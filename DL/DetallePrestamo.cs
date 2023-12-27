using System;
using System.Collections.Generic;

namespace DL;

public partial class DetallePrestamo
{
    public int IdDetallePrestamo { get; set; }

    public bool Entregado { get; set; }

    public DateTime? FechaEntregaReal { get; set; }

    public int? IdPrestamo { get; set; }

    public virtual Prestamo? IdPrestamoNavigation { get; set; }
}
