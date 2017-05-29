using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TIMBRADOS
{
    public class VALORACION_DE_TIMBRES
    {
        public TIMBRADO TimbrarVenta(List<VENTA> Ventas, int IdNotaDeVenta,
            string Consumidor, string FormaDePago, string MetodoDePago,
            string Rfc, string Nombre,
            string Calle, string NumeroExterior, string NumeroInterior, string Colonia, string Municipio, string Estado, string Pais, string CodigoPostal)
        {
            var conexion = new CONFIGURACIONES.CONEXION();
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@CONSUMIDOR", Consumidor);
            DataTable datatable = conexion.EjecutarConsulta("ST_OBTIENE_FOLIO_DE_TIMBRADO", parametros);
            if (datatable != null && datatable.Rows.Count > 0)
            {
                var configuracion = datatable.AsEnumerable()
                    .Select(x => new
                    {
                        FOLIO = x.Field<int>("FOLIO"),
                        USUARIO_ECRIPTADO = x.Field<string>("USUARIO_ECRIPTADO"),
                        COMENTARIOS = x.Field<string>("COMENTARIOS"),
                        ID = x.Field<int>("ID"),
                        ES_PRODUCTIVO = x.Field<bool>("ES_PRODUCTIVO")
                    }).ToList();
                if (configuracion[0].FOLIO == -1)
                    return new TIMBRADO()
                    {
                        CadenaTimbre = string.Empty,
                        Descripcion = configuracion[0].COMENTARIOS,
                        NumeroError = "0",
                        TipoExcepcion = configuracion[0].COMENTARIOS,
                        Xml = string.Empty
                    };
                string userprofact = DecryptString(configuracion[0].USUARIO_ECRIPTADO, "jorge");
                string xml = CrearArchivoParaTimbrar(Ventas, IdNotaDeVenta, configuracion[0].FOLIO.ToString(), FormaDePago, MetodoDePago, Rfc, Nombre, Calle, NumeroExterior, NumeroInterior, Colonia, Municipio, Estado, Pais, CodigoPostal);
                var res = new TIMBRADO();
                if (configuracion[0].ES_PRODUCTIVO)
                    res = FacturarProductivo(xml, userprofact, configuracion[0].FOLIO.ToString());
                else
                    res = Facturar(xml, userprofact, configuracion[0].FOLIO.ToString());
                if (res.NumeroError == "0")
                {
                    System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                    xmlDoc.LoadXml(res.Xml);
                    parametros = new Dictionary<string, object>();
                    parametros.Add("@ID_TIMBRE_ASIGNADO", configuracion[0].ID);
                    parametros.Add("@UID", xmlDoc.ChildNodes[1].ChildNodes[4].ChildNodes[0].Attributes["UUID"].Value);
                    conexion.EjecutarConsulta("ST_REGISTRAR_TRIMBRE_CONSUMIDO", parametros);
                    res.Folio = configuracion[0].FOLIO;
                    res.RutaDeArchivo = xml;
                    return res;
                }
                else
                    return res;
            }
            else
                return new TIMBRADO()
                {
                    CadenaTimbre = string.Empty,
                    Descripcion = "No se conectó al servicio de administración de timbres",
                    NumeroError = "0",
                    TipoExcepcion = "No se conectó al servicio de administración de timbres",
                    Xml = string.Empty
                };
        }
        private string CrearArchivoParaTimbrar(List<VENTA> Ventas, int IdNotaDeVenta,
            string Folio, string FormaDePago, string MetodoDePago,
            string Rfc, string Nombre,
            string Calle, string NumeroExterior, string NumeroInterior, string Colonia, string Municipio, string Estado, string Pais, string CodigoPostal)
        {
            string Fecha = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            System.IO.FileInfo fi = new System.IO.FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/ARCHIVOS_GENERADOS") + "/PLANTILLAS/comprobanteSinTimbrar.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(fi.FullName);
            XmlNode root = doc.DocumentElement;
            root.Attributes["folio"].Value = Folio;
            root.Attributes["fecha"].Value = Fecha;

            root.Attributes["formaDePago"].Value = FormaDePago;
            root.Attributes["subTotal"].Value = Ventas.Sum(x => x.Importe).ToString();
            root.Attributes["Moneda"].Value = "MXN";
            root.Attributes["total"].Value = Ventas.Select(x => x.Importe + (x.Importe * x.P_IEPS) + (x.Importe * x.P_ISH) + (x.Importe * x.P_IVA)).Sum().ToString();
            root.Attributes["tipoDeComprobante"].Value = "ingreso";
            root.Attributes["metodoDePago"].Value = MetodoDePago;

            root.ChildNodes[1].Attributes["rfc"].Value = Rfc;
            root.ChildNodes[1].Attributes["nombre"].Value = Nombre;

            XmlElement element;
            foreach (var item in Ventas)
            {
                element = doc.CreateElement("cfdi:Concepto", "http://www.sat.gob.mx/cfd/3");
                XmlAttribute attcantidad = doc.CreateAttribute("cantidad"); attcantidad.Value = item.Cantidad.ToString(); element.Attributes.Append(attcantidad);
                XmlAttribute attunidad = doc.CreateAttribute("unidad"); attunidad.Value = item.Unidad.ToString(); element.Attributes.Append(attunidad);
                XmlAttribute attnoIdentificacion = doc.CreateAttribute("noIdentificacion"); attnoIdentificacion.Value = item.Codigo.ToString(); element.Attributes.Append(attnoIdentificacion);
                XmlAttribute attdescripcion = doc.CreateAttribute("descripcion"); attdescripcion.Value = item.Descripcion.ToString(); element.Attributes.Append(attdescripcion);
                XmlAttribute attvalorUnitario = doc.CreateAttribute("valorUnitario"); attvalorUnitario.Value = item.Precio.ToString(); element.Attributes.Append(attvalorUnitario);
                XmlAttribute attimporte = doc.CreateAttribute("importe"); attimporte.Value = item.Importe.ToString(); element.Attributes.Append(attimporte);
                root.ChildNodes[2].AppendChild(element);
            }
            var elementtraslados = doc.CreateElement("cfdi:Impuestos", "http://www.sat.gob.mx/cfd/3");
            root.AppendChild(elementtraslados);
            if (Ventas.Any(x => x.P_IEPS > 0 | x.P_IVA > 0 | x.P_ISH > 0))
            {
                XmlAttribute atttotaltraslados = doc.CreateAttribute("totalImpuestosTrasladados");
                atttotaltraslados.Value = Ventas.Select(x => x.P_IEPS + x.P_ISH + x.P_IVA).Sum().ToString();
                elementtraslados.Attributes.Append(atttotaltraslados);
                var elementitemtraslados = doc.CreateElement("cfdi:Traslados", "http://www.sat.gob.mx/cfd/3");
                elementtraslados.AppendChild(elementitemtraslados);
                foreach (var item in Ventas.Where(x => x.P_IEPS > 0).Select(x => new { x.P_IEPS, Total = x.Importe * x.P_IEPS }).Distinct())
                {
                    var elemenieps = doc.CreateElement("cfdi:Traslado", "http://www.sat.gob.mx/cfd/3");
                    XmlAttribute attriepsimpuesto = doc.CreateAttribute("impuesto"); attriepsimpuesto.Value = "IEPS"; elemenieps.Attributes.Append(attriepsimpuesto);
                    XmlAttribute attriepstasa = doc.CreateAttribute("tasa"); attriepstasa.Value = (item.P_IEPS * 100).ToString(); elemenieps.Attributes.Append(attriepstasa);
                    XmlAttribute attriepsimporte = doc.CreateAttribute("importe"); attriepsimporte.Value = item.Total.ToString(); elemenieps.Attributes.Append(attriepsimporte);
                    elementitemtraslados.AppendChild(elemenieps);
                }
                foreach (var item in Ventas.Where(x => x.P_IVA > 0).Select(x => new { x.P_IVA, Total = x.Importe * x.P_IVA }).Distinct())
                {
                    var elemenieps = doc.CreateElement("cfdi", "Traslado");
                    XmlAttribute attriepsimpuesto = doc.CreateAttribute("impuesto"); attriepsimpuesto.Value = "IVA"; elemenieps.Attributes.Append(attriepsimpuesto);
                    XmlAttribute attriepstasa = doc.CreateAttribute("tasa"); attriepstasa.Value = (item.P_IVA * 100).ToString(); elemenieps.Attributes.Append(attriepstasa);
                    XmlAttribute attriepsimporte = doc.CreateAttribute("importe"); attriepsimporte.Value = item.Total.ToString(); elemenieps.Attributes.Append(attriepsimporte);
                    elementitemtraslados.AppendChild(elemenieps);
                }
                foreach (var item in Ventas.Where(x => x.P_ISH > 0).Select(x => new { x.P_ISH, Total = x.Importe * x.P_ISH }).Distinct())
                {
                    var elemenieps = doc.CreateElement("cfdi", "Traslado");
                    XmlAttribute attriepsimpuesto = doc.CreateAttribute("impuesto"); attriepsimpuesto.Value = "ISH"; elemenieps.Attributes.Append(attriepsimpuesto);
                    XmlAttribute attriepstasa = doc.CreateAttribute("tasa"); attriepstasa.Value = (item.P_ISH * 100).ToString(); elemenieps.Attributes.Append(attriepstasa);
                    XmlAttribute attriepsimporte = doc.CreateAttribute("importe"); attriepsimporte.Value = item.Total.ToString(); elemenieps.Attributes.Append(attriepsimporte);
                    elementitemtraslados.AppendChild(elemenieps);
                }
            }
            doc.Save(System.Web.HttpContext.Current.Server.MapPath("~/ARCHIVOS_GENERADOS") + "/" + Fecha.Replace(":", "_") + ".xml");
            return System.Web.HttpContext.Current.Server.MapPath("~/ARCHIVOS_GENERADOS") + "/" + Fecha.Replace(":", "_") + ".xml";// idfactura);
        }
        private TIMBRADO Facturar(string ArchivoXML, string UsuarioProfact, string Folio)
        {
            //Ubicación del servicio de pruebas 
            //http://www.timbracfdipruebas.mx/ServicioIntegracionPruebas/Timbrado.asmx 
            //Parámetro usuarioIntegrador 
            //string usuarioIntegrador = "mvpNUXmQfK8=";
            //Parámetro comprobante a timbrar en base64 
            string comprobanteBase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(ArchivoXML));
            ServiceReferencePruebas.TimbradoSoapClient soapClient = new ServiceReferencePruebas.TimbradoSoapClient();
            ServiceReferencePruebas.ArrayOfAnyType resultados = soapClient.TimbraCFDI(UsuarioProfact, comprobanteBase64, Folio);
            //Tipo de excepcion 
            TIMBRADO tbr = new TIMBRADO()
            {
                TipoExcepcion = resultados[0].ToString(),
                //Numero de error 
                NumeroError = resultados[1].ToString(),
                //Descripcion del resultado 
                Descripcion = resultados[2].ToString(),
                //Xml timbrado 
                Xml = resultados[3].ToString(),
                //Codigo bidimensional 
                CodigoBidimensional = (byte[])(resultados[4]),
                //Cadena timbre 
                CadenaTimbre = resultados[5].ToString(),
            };
            return tbr;
        }
        private TIMBRADO FacturarProductivo(string ArchivoXML, string UsuarioProfact, string Folio)
        {
            //Ubicación del servicio de pruebas 
            //http://www.timbracfdipruebas.mx/ServicioIntegracionPruebas/Timbrado.asmx 
            //Parámetro usuarioIntegrador 
            //string usuarioIntegrador = "mvpNUXmQfK8=";
            //Parámetro comprobante a timbrar en base64 
            string comprobanteBase64 = Convert.ToBase64String(System.IO.File.ReadAllBytes(ArchivoXML));
            ServiceReferenceProductivo.TimbradoSoapClient soapClient = new ServiceReferenceProductivo.TimbradoSoapClient();
            ServiceReferenceProductivo.ArrayOfAnyType resultados = soapClient.TimbraCFDI(UsuarioProfact, comprobanteBase64, Folio);
            //Tipo de excepcion 
            TIMBRADO tbr = new TIMBRADO()
            {
                TipoExcepcion = resultados[0].ToString(),
                //Numero de error 
                NumeroError = resultados[1].ToString(),
                //Descripcion del resultado 
                Descripcion = resultados[2].ToString(),
                //Xml timbrado 
                Xml = resultados[3].ToString(),
                //Codigo bidimensional 
                CodigoBidimensional = (byte[])(resultados[4]),
                //Cadena timbre 
                CadenaTimbre = resultados[5].ToString()
            };
            return tbr;
        }

        #region Metodos
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        private const string initVector = "pemgail9uzpgzl88";
        // This constant is used to determine the keysize of the encryption algorithm.
        private const int keysize = 256;
        //Encrypt
        public string EncryptString(string plainText, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }
        //Decrypt
        public string DecryptString(string cipherText, string passPhrase)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
        #endregion
    }
}
