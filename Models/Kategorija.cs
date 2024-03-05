using System;
using System.Collections.Generic;

namespace Zlatara.Models;

public partial class Kategorija
{
    public int KategorijaId { get; set; }

    public string? Naziv { get; set; }

    public virtual ICollection<Artikal> Artikals { get; set; } = new List<Artikal>();
}
