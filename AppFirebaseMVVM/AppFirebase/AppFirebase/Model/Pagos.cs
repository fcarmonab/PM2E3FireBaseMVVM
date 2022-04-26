using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AppFirebase.Model
{
    public class Pagos
    {
        public string Descripcion { get; set; }
        public double Monto { get; set; }
        public DateTime Fecha { get; set; }
        public byte[] Photo_Recibo { get; set; }
        
    }
}
