using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIMBRADOS
{
    public class TIMBRADO
    {
        public string TipoExcepcion { get; set; }
        public string NumeroError { get; set; }
        public string Descripcion { get; set; }
        public string Xml { get; set; }
        public byte[] CodigoBidimensional { get; set; }
        public string CadenaTimbre { get; set; }
        public int Folio { get; set; }
        public string RutaDeArchivo { get; set; }
    }
}
