using System;
using System.Collections.Generic;

namespace Zlatara.Models;

public partial class StorniranRacun
{
    public int RacunId { get; set; }

    public int MenadzerUser { get; set; }

    public DateOnly Datum { get; set; }

    public DateOnly DatumStorniranja { get; set; }

    public double Cena { get; set; }

    public int? Pib { get; set; }

    public string RadnikUser { get; set; }

    public virtual Menadzer MenadzerUserNavigation { get; set; } = null!;

    public virtual Radnik RadnikUserNavigation { get; set; } = null!;
}
