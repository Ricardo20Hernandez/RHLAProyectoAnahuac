using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Prestamo
    {

        public int IdPrestamo { get; set; }

        public string FechaSalida { get; set; }


        public string FechaEntregaEsperada { get; set; }

        public ML.Medio Medio { get; set; }

        public string Id { get; set; }

        public List<object> Prestamos { get; set; } 
    }
}
