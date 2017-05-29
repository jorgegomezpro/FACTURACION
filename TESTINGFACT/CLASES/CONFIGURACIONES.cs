using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TESTINGFACT.CLASES
{
    public class CONFIGURACIONES
    {
        public string Consumidor
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["CONSUMIDOR"];
            }
        }
        public Tuple<string, string, string> CorreoDeAdministracion
        {
            get
            {
                return new Tuple<string, string, string>(
                        System.Configuration.ConfigurationManager.AppSettings["NOMBRE_DE_ADMINISTRACION"],
                        System.Configuration.ConfigurationManager.AppSettings["CORREO_DE_ADMINISTRACION"],
                        System.Configuration.ConfigurationManager.AppSettings["CONTRASENA_DE_CORREO_DE_ADMINISTRACION"]
                    );
            }
        }
        public List<string> CorreosDeSupervision
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["CORREOS_DE_SUPERVISION"].Split(',').ToList();
            }
        }
        public string AsuntoDeFacturacion
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ASUNTO_DE_FACTURACION"];
            }
        }
        public string CadenaDeConexionPrincipal
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ConcexionPrincipal"].ConnectionString;
            }
        }
    }
}