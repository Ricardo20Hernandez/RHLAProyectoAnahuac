using System;
using System.Collections.Generic;

namespace DL;

public partial class TipoMedio
{
    public int IdTipoMedio { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Medio> Medios { get; set; } = new List<Medio>();
}
