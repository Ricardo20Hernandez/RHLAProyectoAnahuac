using System;
using System.Collections.Generic;

namespace DL;

public partial class Medio
{
    public int IdMedio { get; set; }

    public string Titulo { get; set; } = null!;

    public int? NumeroPaginas { get; set; }

    public TimeSpan? Duracion { get; set; }

    public int CantidadDisponible { get; set; }

    public byte[]? Imagen { get; set; }

    public int? IdTipoMedio { get; set; }

    public int? IdEditorial { get; set; }

    public int? IdIdioma { get; set; }

    public int? IdAutor { get; set; }

    public virtual Autor? IdAutorNavigation { get; set; }

    public virtual Editorial? IdEditorialNavigation { get; set; }

    public virtual Idioma? IdIdiomaNavigation { get; set; }

    public virtual TipoMedio? IdTipoMedioNavigation { get; set; }

    public virtual ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
}
