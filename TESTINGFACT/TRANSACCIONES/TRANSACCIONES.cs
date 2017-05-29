using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TESTINGFACT.CLASES;
using TESTINGFACT.REPORTES;
using TIMBRADOS;

namespace TESTINGFACT.TRANSACCIONES
{
    public class TRANSACCIONES
    {
        public double IVA = 0.16;
        public double ISH = 0.02;

        public bool CrearModificarArticuloServicio(string Codigo, string Clase, string Descripcion, string Unidad, double PrecioSinIVA, bool GeneraIVA,
            float PorcentajeIEPS, bool GeneraISH, int StockMinimo, int StockMaximo, string Anaquel)
        {
            CONSULTAS sql = new CONSULTAS();
            CONEXION C = new CONEXION();
            bool existe = (C.EjecutarEscalar(sql.EXISTE_ARTICULO_SERVICIO(Codigo)).ToString() == "1");
            string transaccion = string.Empty;
            if (!existe)
                transaccion = sql.AGREGAR_ARTICULO_SERVICIO(Codigo, Clase, Descripcion, Unidad, PrecioSinIVA, GeneraIVA,
                    (PorcentajeIEPS / 100), GeneraISH, StockMinimo, StockMaximo, Anaquel);
            else
                transaccion = sql.MODIFICAR_ARTICULO_SERVICIO(Codigo, Clase, Descripcion, Unidad, PrecioSinIVA, GeneraIVA,
                    (PorcentajeIEPS / 100), GeneraISH, StockMinimo, StockMaximo, Anaquel);
            var res = C.EjecutarSentencia(transaccion);
            if (res.HasValue && res.Value > 0)
                return true;
            return false;
        }
        public bool ExisteArticuloServicio(string Codigo)
        {
            CONSULTAS sql = new CONSULTAS();
            CONEXION C = new CONEXION();
            bool existe = (C.EjecutarEscalar(sql.EXISTE_ARTICULO_SERVICIO(Codigo)).ToString() == "1");
            return existe;
        }
        public IList ObenerArticuloServicio(string Codigo, string Descripcion, int Elementos)
        {
            DataTable dt = null;
            CONSULTAS sql = new CONSULTAS();
            CONEXION C = new CONEXION();
            string sentencia = string.Empty;
            if (string.IsNullOrEmpty(Codigo))
                sentencia = sql.OBTENER_ARTICULO_SERVICIO_POR_DESCRIPCION(Descripcion, 5);
            else
                sentencia = sql.OBTENER_ARTICULO_SERVICIO_POR_CODIGO(Codigo);
            dt = C.EjecutarConsulta(sentencia);
            return dt.AsEnumerable()
                .Select(x => new
                {
                    CODIGO = x.Field<string>("CODIGO"),
                    DESCRIPCION = x.Field<string>("DESCRIPCION"),
                    PRECIO_SIN_IVA = x.Field<decimal>("PRECIO_SIN_IVA"),
                    UNIDAD = x.Field<string>("UNIDAD")
                }).ToList();
        }
        public object ObenerArticuloServicio(string Codigo)
        {
            DataTable dt = null;
            CONSULTAS sql = new CONSULTAS();
            CONEXION C = new CONEXION();
            string sentencia = sql.OBTENER_ARTICULO_SERVICIO_POR_CODIGO(Codigo);
            dt = C.EjecutarConsulta(sentencia);
            var list = dt.AsEnumerable()
                .Select(x => new
                {
                    CODIGO = x.Field<string>("CODIGO"),
                    DESCRIPCION = x.Field<string>("DESCRIPCION"),
                    PRECIO_SIN_IVA = x.Field<decimal>("PRECIO_SIN_IVA"),
                    UNIDAD = x.Field<string>("UNIDAD"),
                    P_IVA = (x.Field<bool>("GENERA_IVA") ? IVA : 0),
                    P_ISH = (x.Field<bool>("GENERA_ISH") ? ISH : 0),
                    P_IEPS = x.Field<double>("P_IEPS")
                }).ToList();
            if (list.Count > 0)
                return list[0];
            else
                return null;
        }

        public bool CrearModificarCliente(string Rfc, string RazonSocial, string Calle, string NumeroExterior, string NumeroInterior, string Colonia,
            string DelegacionMunicipio, string Localidad, string Estado, string Pais, string CodigoPostal, string CorreoElectronico)
        {
            CONSULTAS sql = new CONSULTAS();
            CONEXION C = new CONEXION();
            bool existe = (C.EjecutarEscalar(sql.EXISTE_CLIENTE(Rfc)).ToString() == "1");
            string transaccion = string.Empty;
            if (!existe)
                transaccion = sql.AGREGAR_CLIENTE(Rfc, RazonSocial, Calle, NumeroExterior, NumeroInterior, Colonia, DelegacionMunicipio,
                    Localidad, Estado, Pais, CodigoPostal, CorreoElectronico);
            else
                transaccion = sql.MODIFICAR_CLIENTE(Rfc, RazonSocial, Calle, NumeroExterior, NumeroInterior, Colonia, DelegacionMunicipio,
                    Localidad, Estado, Pais, CodigoPostal, CorreoElectronico);
            var res = C.EjecutarSentencia(transaccion);
            if (res.HasValue && res.Value > 0)
                return true;
            return false;
        }
        public bool ExisteCliente(string Rfc)
        {
            CONSULTAS sql = new CONSULTAS();
            CONEXION C = new CONEXION();
            bool existe = (C.EjecutarEscalar(sql.EXISTE_CLIENTE(Rfc)).ToString() == "1");
            return existe;
        }
        public IList ObenerCliente(string Rfc, string RazonSocial, int Elementos)
        {
            DataTable dt = null;
            CONSULTAS sql = new CONSULTAS();
            CONEXION C = new CONEXION();
            string sentencia = string.Empty;
            if (string.IsNullOrEmpty(Rfc))
                sentencia = sql.OBTENER_CLIENTE_POR_RAZON_SOCIAL(RazonSocial, Elementos);
            else
                sentencia = sql.OBTENER_CLIENTE_POR_RFC(Rfc);
            dt = C.EjecutarConsulta(sentencia);
            return dt.AsEnumerable()
                .Select(x => new
                {
                    RFC = x.Field<string>("RFC"),
                    RAZON_SOCIAL = x.Field<string>("RAZON_SOCIAL"),
                    CALLE = x.Field<string>("CALLE"),
                    NUMERO_INTERIOR = x.Field<string>("NUMERO_INTERIOR"),
                    NUMERO_EXTERIOR = x.Field<string>("NUMERO_EXTERIOR"),
                    COLONIA = x.Field<string>("COLONIA"),
                    DELEGACION_MUNICIPIO = x.Field<string>("DELEGACION_MUNICIPIO"),
                    LOCALIDAD = x.Field<string>("LOCALIDAD"),
                    ESTADO = x.Field<string>("ESTADO"),
                    PAIS = x.Field<string>("PAIS"),
                    CODIGO_POSTAL = x.Field<string>("CODIGO_POSTAL"),
                    CORREO_ELECTRONICO = x.Field<string>("CORREO_ELECTRONICO")
                }).ToList();
        }
        public object ObenerCliente(string Rfc)
        {
            DataTable dt = null;
            CONSULTAS sql = new CONSULTAS();
            CONEXION C = new CONEXION();
            string sentencia = string.Empty;
            sentencia = sql.OBTENER_CLIENTE_POR_RFC(Rfc);
            dt = C.EjecutarConsulta(sentencia);
            var res = dt.AsEnumerable()
                .Select(x => new
                {
                    RFC = x.Field<string>("RFC"),
                    RAZON_SOCIAL = x.Field<string>("RAZON_SOCIAL"),
                    CALLE = x.Field<string>("CALLE"),
                    NUMERO_INTERIOR = x.Field<string>("NUMERO_INTERIOR"),
                    NUMERO_EXTERIOR = x.Field<string>("NUMERO_EXTERIOR"),
                    COLONIA = x.Field<string>("COLONIA"),
                    DELEGACION_MUNICIPIO = x.Field<string>("DELEGACION_MUNICIPIO"),
                    LOCALIDAD = x.Field<string>("LOCALIDAD"),
                    ESTADO = x.Field<string>("ESTADO"),
                    PAIS = x.Field<string>("PAIS"),
                    CODIGO_POSTAL = x.Field<string>("CODIGO_POSTAL"),
                    CORREO_ELECTRONICO = x.Field<string>("CORREO_ELECTRONICO")
                }).ToList();
            if (res.Count > 0)
                return res[0];
            else
                return null;
        }

        public object CrearNotaDeVenta(List<VENTA> Ventas, string Rfc, string FormaDePago, string DatosDePago)
        {
            CONFIGURACIONES configuracion = new CONFIGURACIONES();
            CONSULTAS sql = new CONSULTAS();
            CONEXION C = new CONEXION();
            string sentencia = string.Empty;
            int idnotadeventa = int.Parse(C.EjecutarEscalar(sql.AGREGAR_NOTA_DE_VENTA()).ToString());
            TIMBRADO timbradoactual = null;
            foreach (var venta in Ventas)
                C.EjecutarSentencia(sql.AGREGAR_VENTAS(idnotadeventa, venta.Codigo, venta.Cantidad, venta.Unidad, venta.Descripcion, venta.Precio, venta.P_IEPS, venta.P_IVA, venta.P_ISH));
            if (!string.IsNullOrEmpty(Rfc))
            {
                sentencia = sql.OBTENER_CLIENTE_POR_RFC(Rfc);
                DataTable dt = C.EjecutarConsulta(sentencia);
                var res = dt.AsEnumerable()
                    .Select(x => new
                    {
                        RFC = x.Field<string>("RFC"),
                        RAZON_SOCIAL = x.Field<string>("RAZON_SOCIAL"),
                        CALLE = x.Field<string>("CALLE"),
                        NUMERO_INTERIOR = x.Field<string>("NUMERO_INTERIOR"),
                        NUMERO_EXTERIOR = x.Field<string>("NUMERO_EXTERIOR"),
                        COLONIA = x.Field<string>("COLONIA"),
                        DELEGACION_MUNICIPIO = x.Field<string>("DELEGACION_MUNICIPIO"),
                        LOCALIDAD = x.Field<string>("LOCALIDAD"),
                        ESTADO = x.Field<string>("ESTADO"),
                        PAIS = x.Field<string>("PAIS"),
                        CODIGO_POSTAL = x.Field<string>("CODIGO_POSTAL"),
                        CORREO_ELECTRONICO = x.Field<string>("CORREO_ELECTRONICO")
                    }).ToList();
                if (res.Count > 0)
                {
                    VALORACION_DE_TIMBRES t = new VALORACION_DE_TIMBRES();
                    Random mirandom = new Random();
                    timbradoactual = t.TimbrarVenta(Ventas, idnotadeventa, configuracion.Consumidor, "Pago en una sola exhibicion", FormaDePago, res[0].RFC, res[0].RAZON_SOCIAL, res[0].CALLE, res[0].NUMERO_EXTERIOR,
                        res[0].NUMERO_INTERIOR, res[0].COLONIA, res[0].DELEGACION_MUNICIPIO, res[0].ESTADO, res[0].PAIS, res[0].CODIGO_POSTAL);
                    int idfactura = (int)(C.EjecutarEscalar(sql.AGREGAR_FACTURA("", FormaDePago, Ventas.Select(x => (double)x.Importe).Sum(), idnotadeventa, Rfc,
                                        timbradoactual.Folio.ToString(), res[0].RAZON_SOCIAL, "MXN", Ventas.Select(x => x.Importe + (x.Importe * x.P_IEPS) + (x.Importe * x.P_ISH) + (x.Importe * x.P_IVA)).Sum(),
                                        FormaDePago)));
                    //Nullable<int> creados = C.EjecutarSentencia(sql.AGREGAR_FACTURACION(IdNotaDeVenta, idfactura));
                    C.EjecutarSentencia(sql.ACTUALIZAR_fACTURA(idfactura, timbradoactual.Descripcion));
                    GENERACION_DE_REPORTES gr = new GENERACION_DE_REPORTES();
                    System.IO.FileInfo fi = new System.IO.FileInfo(timbradoactual.RutaDeArchivo);
                    System.IO.File.WriteAllText(fi.FullName.Replace(fi.Extension, "_SAT.xml"), timbradoactual.Xml);
                    System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                    xmlDoc.LoadXml(timbradoactual.Xml);
                    gr.GenerarFactura(fi.Name.Replace(fi.Extension, string.Empty), Ventas, res[0].RFC, res[0].RAZON_SOCIAL,
                        string.Format("CALLE {0}, # EXTERIOR {1}, #INTERIOR {2}, COLONIA {3}", res[0].CALLE, res[0].NUMERO_EXTERIOR, res[0].NUMERO_INTERIOR, res[0].COLONIA),
                        res[0].DELEGACION_MUNICIPIO, res[0].CODIGO_POSTAL, xmlDoc.ChildNodes[1].Attributes["folio"].Value,
                        xmlDoc.ChildNodes[1].ChildNodes[4].ChildNodes[0].Attributes["UUID"].Value, xmlDoc.ChildNodes[1].ChildNodes[4].ChildNodes[0].Attributes["FechaTimbrado"].Value,
                        xmlDoc.ChildNodes[1].Attributes["sello"].Value, xmlDoc.ChildNodes[1].ChildNodes[4].ChildNodes[0].Attributes["selloCFD"].Value,
                        xmlDoc.ChildNodes[1].ChildNodes[4].ChildNodes[0].Attributes["noCertificadoSAT"].Value, "CODIGO RQ");
                    if (!string.IsNullOrEmpty(res[0].CORREO_ELECTRONICO))
                    {
                        NOTIFIACIONES nt = new NOTIFIACIONES();
                        nt.NotificacionPorCorreo(res[0].CORREO_ELECTRONICO, new List<string>() {
                            fi.FullName.Replace(fi.Extension, "_SAT.xml"),
                            fi.FullName.Replace(fi.Extension, "_SAT.pdf")
                        });
                    }
                }
            }
            return new
            {
                ID_NOTA_DE_VENTA = idnotadeventa,
                ERROR = (timbradoactual == null ? false : (timbradoactual.NumeroError == "0" ? false : true)),
                RESULTADO = (timbradoactual == null ? "" : timbradoactual.Descripcion)
            };
        }
    }
}