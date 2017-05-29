using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIMBRADOS
{
    public class VENTA
    {
        public string Codigo { get; set; }
        public float Cantidad { get; set; }
        public string Unidad { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public double Importe { get; set; }
        public double P_IEPS { get; set; }
        public double P_IVA { get; set; }
        public double P_ISH { get; set; }
    }
}
