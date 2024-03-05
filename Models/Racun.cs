using System;
using System.Collections.Generic;

namespace Zlatara.Models;

public partial class Racun
{
    public int RacunId { get; set; }

    public string RadnikUser { get; set; }

    public DateOnly Datum { get; set; }

    public double Cena { get; set; }

    public int? Pib { get; set; }

    public virtual Radnik RadnikUserNavigation { get; set; } = null!;
}
