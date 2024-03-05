using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Zlatara.Models;

public partial class StavkaRacuna
{
    public int RedinBroj { get; set; }

    public int RacunId { get; set; }

    public int ArtikalId { get; set; }

    public int Kolicina { get; set; }

    public double JedCena { get; set; }

    public double UkupnaVred { get; set; }
    [AllowNull]
    public virtual Artikal Artikal { get; set; }
}
