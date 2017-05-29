using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using SelectPdf;
using TESTINGFACT.CLASES;
using TIMBRADOS;

namespace TESTINGFACT.REPORTES
{
    public class GENERACION_DE_REPORTES
    {
        public string GenerarFactura(string NOMBREDEGUARDADO, List<VENTA> VENTASGENERADAS, string RFC, string RAZONSOCIAL, string DOMICILIO, string CIUDADDELEGACIONMUNICIPIO,
            string CODIGOPOSTAL, string FOLIOFISCAL, string SERIEDECERTIFICADOSAT, string FECHADECERTIFICAION, string SELLODIGITALEMISOR,
            string SELLODIGITALSAT, string CADENAORIGINALCOMPLEMENTO, string CODIGORQ)
        {
            // read parameters from the webpage
            PLANTILLA_DE_REPORTE pr = new PLANTILLA_DE_REPORTE();
            string url = pr.ObtenerPlantilla(PLANTILLA_DE_REPORTE.ReportePersonalizado.Factura);
            System.IO.FileInfo fi = new System.IO.FileInfo(url);
            string Desgloce = "",
                formatodefila = "<tr><td style='font-size:10px;text-align:center;'>{0}</td>" +
                                "<td style='font-size:10px;text-align:center;'>{1}</td>" +
                                "<td style='font-size:10px;text-align:center;'>{2}</td>" +
                                "<td style='font-size:10px;text-align:center;'>{3}</td>" +
                                "<td style='font-size:10px;text-align:center;'>{4}</td>" +
                                "<td style='font-size:10px;text-align:center;'>{5}</td> " +
                                "</tr>";
            foreach (var item in VENTASGENERADAS)
            {
                Desgloce += string.Format(formatodefila, item.Codigo, Math.Round(item.Cantidad, 2), item.Unidad,
                    item.Descripcion, Math.Round(item.Precio), Math.Round(item.Importe));
            }
            string lines = System.IO.File.ReadAllText(fi.FullName);
            string htmlString = lines.Replace("@RFC", RFC)
                .Replace("@DESGLOCE", Desgloce)
                .Replace("@FOLIOFISCAL", FOLIOFISCAL)
                .Replace("@NUMERODECRTIFICADOSAT", SERIEDECERTIFICADOSAT)
                .Replace("@FECHADECRTIFICACIÓN", FECHADECERTIFICAION)
                .Replace("@RAZONSOCIAL", RAZONSOCIAL)
                .Replace("@DOMICILIOFISCAL", DOMICILIO)
                .Replace("@CIUDAD_DELEGACIÓN_MUNICIPIO", CIUDADDELEGACIONMUNICIPIO)
                .Replace("@CP", CODIGOPOSTAL)
                .Replace("@SUBTOTAL", Math.Round(VENTASGENERADAS.Sum(x => x.Importe), 2).ToString())
                .Replace("@IVA", Math.Round(VENTASGENERADAS.Select(x => x.Importe * x.P_IVA).Sum(), 2).ToString())
                .Replace("@IVA", Math.Round(VENTASGENERADAS.Select(x => x.Importe * x.P_ISH).Sum(), 2).ToString())
                .Replace("@IEPS", Math.Round(VENTASGENERADAS.Select(x => x.Importe * x.P_IEPS).Sum(), 2).ToString())
                .Replace("@TOTAL", Math.Round(VENTASGENERADAS.Select(x => (x.Importe * x.P_ISH) + (x.Importe * x.P_IVA) + (x.Importe * x.P_IEPS) + x.Importe).Sum(), 2).ToString())
                .Replace("@SELLODIGITALEMISOR", DistribuircadenasLargas(SELLODIGITALEMISOR, 80))
                .Replace("@SELLODIGITALSAT", DistribuircadenasLargas(SELLODIGITALSAT, 80))
                .Replace("@CADENAORIGINALCOMPLEMENTO", DistribuircadenasLargas(CADENAORIGINALCOMPLEMENTO, 80));
            string pdf_page_size = "Letter";
            PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize), pdf_page_size, true);

            string pdf_orientation = "Portrait";//Landscape
            PdfPageOrientation pdfOrientation = (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation), pdf_orientation, true);
            int webPageWidth = 1024;
            int webPageHeight = 0;
            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();
            // set converter options
            converter.Options.PdfPageSize = pageSize;
            converter.Options.PdfPageOrientation = pdfOrientation;
            converter.Options.WebPageWidth = webPageWidth;
            converter.Options.WebPageHeight = webPageHeight;
            // create a new pdf document converting an url
            //PdfDocument doc = converter.ConvertUrl(url);
            PdfDocument doc = converter.ConvertHtmlString(htmlString);

            // save pdf document
            //(System.Web.HttpContext.Current.Server.MapPath("~/ARCHIVOS_GENERADOS") + "/" + Fecha.Replace(":", "_") + ".xml"),
            string rutadeguardado = (System.Web.HttpContext.Current.Server.MapPath("~/ARCHIVOS_GENERADOS") + "/" + NOMBREDEGUARDADO + "_SAT.pdf");
            doc.Save(rutadeguardado);

            //doc.Save(Response, false, "Sample.pdf");
            // close pdf document
            doc.Close();
            return rutadeguardado;
        }
        private string DistribuircadenasLargas(string Texto, int Tamaño)
        {
            int posicion = 0;
            string res = string.Empty;
            string parte = "";
            while (Texto.Length > posicion)
            {
                if (Texto.Length >= (posicion + Tamaño))
                    parte = Texto.Substring(posicion, Tamaño);
                else
                    parte = Texto.Substring(posicion, (Texto.Length - posicion));
                res += ("<div>" + parte + "</div>");
                posicion += Tamaño;
            }
            return res;
        }
    }
}