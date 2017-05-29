using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TESTINGFACT.REPORTES
{
    public class PLANTILLA_DE_REPORTE
    {
        public enum ReportePersonalizado
        {
            Factura
        }
        public string ObtenerPlantilla(ReportePersonalizado tipoDeReport)
        {
            string res = string.Empty;
            switch (tipoDeReport)
            {
                case ReportePersonalizado.Factura:
                    System.IO.FileInfo fi = new System.IO.FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/ARCHIVOS_GENERADOS") + "/PLANTILLAS/FACTURA_GENERAL.html");
                    res = fi.FullName;
                    break;
            }
            return res;
        }
    }
}