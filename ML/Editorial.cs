using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Editorial
    {
        public int IdEditorial { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public List<object> Editoriales { get; set; }
        public ML.Direccion Direccion { get; set; } //Propiedad de navegación
    }
}
