using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using TESTINGFACT.CLASES;

namespace TESTINGFACT.TRANSACCIONES
{
    public class TIMBRES
    {
        public Tuple<string, string, int> CrearArchivoParaTimbrar(List<VENTA> Ventas, int IdNotaDeVenta,
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
            CONSULTAS sql = new CONSULTAS();
            CONEXION C = new CONEXION();
            string sentencia = string.Empty;
            int idfactura = (int)(C.EjecutarEscalar(sql.AGREGAR_FACTURA("", FormaDePago, Ventas.Select(x => (double)x.Importe).Sum(), IdNotaDeVenta, Rfc,
                                Folio, Nombre, "MXN", Ventas.Select(x => x.Importe + (x.Importe * x.P_IEPS) + (x.Importe * x.P_ISH) + (x.Importe * x.P_IVA)).Sum(),
                                MetodoDePago)));
            Nullable<int> creados = C.EjecutarSentencia(sql.AGREGAR_FACTURACION(IdNotaDeVenta, idfactura));
            return new Tuple<string, string, int>(
                (System.Web.HttpContext.Current.Server.MapPath("~/ARCHIVOS_GENERADOS") + "/" + Fecha.Replace(":", "_") + ".xml"),
                Folio, idfactura);
        }
        public TIMBRADO Facturar(string ArchivoXML, string UsuarioProfact, string Folio)
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
        public TIMBRADO FacturarProductivo(string ArchivoXML, string UsuarioProfact, string Folio)
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
    }
}