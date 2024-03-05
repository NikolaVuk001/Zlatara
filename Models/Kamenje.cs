using System;
using System.Collections.Generic;

namespace Zlatara.Models;

public partial class Kamenje
{
    public int KamenId { get; set; }

    public string? VrstaKamena { get; set; }

    public double Cistoca { get; set; }

    public string Boja { get; set; } = null!;

    public double Karataz { get; set; }

    public double Kolcina { get; set; }

    public virtual ICollection<Artikal> Artikals { get; set; } = new List<Artikal>();
}
