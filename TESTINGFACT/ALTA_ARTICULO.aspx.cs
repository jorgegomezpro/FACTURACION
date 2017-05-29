using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TESTINGFACT
{
    public partial class ALTA_ARTICULO : System.Web.UI.Page
    {
        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region Metodos y funciones
        [WebMethod]
        public static bool CrearModificarArticuloServicio(string Codigo, string Clase, string Descripcion, string Unidad, double PrecioSinIVA, bool GeneraIVA,
            float PorcentajeIEPS, bool GeneraISH, int StockMinimo, int StockMaximo, string Anaquel)
        {
            TRANSACCIONES.TRANSACCIONES t = new TRANSACCIONES.TRANSACCIONES();
            bool res = t.CrearModificarArticuloServicio(Codigo, Clase, Descripcion, Unidad, PrecioSinIVA, GeneraIVA,
                                            PorcentajeIEPS, GeneraISH, StockMinimo, StockMaximo, Anaquel);
            return res;
        }
        [WebMethod]
        public static string ExisteArticuloServicio(string Codigo)
        {
            TRANSACCIONES.TRANSACCIONES t = new TRANSACCIONES.TRANSACCIONES();
            bool existe = t.ExisteArticuloServicio(Codigo);
            return existe.ToString();
        }
        public object ObenerArticuloServicio(string Codigo, string Descripcion)
        {
            TRANSACCIONES.TRANSACCIONES t = new TRANSACCIONES.TRANSACCIONES();
            var res = t.ObenerArticuloServicio(Codigo, Descripcion, 5);
            return res;
        }
        #endregion
    }
}