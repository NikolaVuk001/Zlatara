using System;
using System.Collections.Generic;

namespace Zlatara.Models;

public partial class Menadzer
{
    public int User { get; set; }

    public int Password { get; set; }

    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;

    public virtual ICollection<StorniranRacun> StorniranRacuns { get; set; } = new List<StorniranRacun>();
}
