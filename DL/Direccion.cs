using System;
using System.Collections.Generic;

namespace DL;

public partial class Direccion
{
    public int IdDireccion { get; set; }

    public string Calle { get; set; } = null!;

    public string? NumeroInterior { get; set; }

    public string NumeroExterior { get; set; } = null!;

    public int? IdColonia { get; set; }

    public int? IdEditorial { get; set; }

    public virtual Colonium? IdColoniaNavigation { get; set; }

    public virtual Editorial? IdEditorialNavigation { get; set; }
}
