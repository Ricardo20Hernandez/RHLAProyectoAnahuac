using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Medio
    {
        public int IdMedio { get; set; }
        public string Titulo { get; set; }
        public int? NumeroPaginas { get; set; }
        public TimeSpan? Duracion { get; set; }
        public int? CantidadDisponible { get; set; }
        public byte[] Imagen { get; set; }
        public List<object> Medios { get; set; }

        //Propiedades de navegación
        public ML.TipoMedio TipoMedio { get; set; }
        public ML.Editorial Editorial { get; set; }
        public ML.Idioma Idioma { get; set; }
        public ML.Autor Autor { get; set; }

    }
}
