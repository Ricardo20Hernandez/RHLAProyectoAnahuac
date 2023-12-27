using System;
using System.Collections.Generic;

namespace DL;

public partial class Prestamo
{
    public int IdPrestamo { get; set; }

    public DateTime FechaSalida { get; set; }

    public DateTime FechaEntregaEsperada { get; set; }

    public int? IdMedio { get; set; }

    public string? Id { get; set; }

    public virtual ICollection<DetallePrestamo> DetallePrestamos { get; set; } = new List<DetallePrestamo>();

    public virtual Medio? IdMedioNavigation { get; set; }

    public virtual AspNetUser? IdNavigation { get; set; }
}
