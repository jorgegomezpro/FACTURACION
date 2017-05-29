using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TESTINGFACT
{
    public partial class ALTA_CLIENTE : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Metodos y funciones
        [WebMethod]
        public static bool CrearModificarCliente(string Rfc, string RazonSocial, string Calle, string NumeroExterior, string NumeroInterior, string Colonia,
            string DelegacionMunicipio, string Localidad, string Estado, string Pais, string CodigoPostal, string CorreoElectronico)
        {
            TRANSACCIONES.TRANSACCIONES t = new TRANSACCIONES.TRANSACCIONES();
            bool res = t.CrearModificarCliente(Rfc, RazonSocial, Calle, NumeroExterior, NumeroInterior, Colonia,
                                    DelegacionMunicipio, Localidad, Estado, Pais, CodigoPostal, CorreoElectronico);
            return res;
        }
        [WebMethod]
        public static string ExisteCliente(string Rfc)
        {
            TRANSACCIONES.TRANSACCIONES t = new TRANSACCIONES.TRANSACCIONES();
            bool existe = t.ExisteCliente(Rfc);
            return existe.ToString();
        }
        public object ObenerCliente(string Codigo, string Descripcion)
        {
            TRANSACCIONES.TRANSACCIONES t = new TRANSACCIONES.TRANSACCIONES();
            var res = t.ObenerArticuloServicio(Codigo, Descripcion, 5);
            return res;
        }
        #endregion
    }
}