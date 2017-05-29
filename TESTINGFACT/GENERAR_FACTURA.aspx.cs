using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TESTINGFACT.CLASES;
using System.Data;
using System.Collections;
using TIMBRADOS;

namespace TESTINGFACT
{
    public partial class GENERAR_FACTURA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static object ValidadProducto(string Codigo)
        {
            TRANSACCIONES.TRANSACCIONES t = new TRANSACCIONES.TRANSACCIONES();
            object res = t.ObenerArticuloServicio(Codigo);
            return res;
        }
        [WebMethod]
        public static object GenerarVenta(List<VENTA> Ventas, string Rfc, string FormaDePago, string DatosDePago)
        {
            TRANSACCIONES.TRANSACCIONES t = new TRANSACCIONES.TRANSACCIONES();
            var res = t.CrearNotaDeVenta(Ventas, Rfc, FormaDePago, DatosDePago);
            return res;
        }
        [WebMethod]
        public static object ObtenerCliente(string Rfc)
        {
            TRANSACCIONES.TRANSACCIONES t = new TRANSACCIONES.TRANSACCIONES();
            var res = t.ObenerCliente(Rfc);
            return res;
        }
        [WebMethod]
        public static IList BuscarProducto(string Descripcion)
        {
            TRANSACCIONES.TRANSACCIONES t = new TRANSACCIONES.TRANSACCIONES();
            var res = t.ObenerArticuloServicio(string.Empty, Descripcion, 5);
            return res;
        }
        [WebMethod]
        public static IList BuscarCliente(string Nombre)
        {
            TRANSACCIONES.TRANSACCIONES t = new TRANSACCIONES.TRANSACCIONES();
            var res = t.ObenerCliente(string.Empty, Nombre, 5);
            return res;
        }
    }
}