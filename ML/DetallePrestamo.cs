using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class DetallePrestamo
    {

        public int IdDetallePrestamo { get; set; }


        public bool Entregado { get; set; }

        public string FechaEntregaReal { get; set; }


      

        public ML.Prestamo Prestamo { get; set; }

        public List<object> DetallePrestamos { get; set; }


    }
}
