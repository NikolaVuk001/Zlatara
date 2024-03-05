using System;
using System.Collections.Generic;

namespace Zlatara.Models;

public partial class Brend
{
    public int BrendId { get; set; }

    public string Naziv { get; set; } = null!;

    public virtual ICollection<Artikal> Artikals { get; set; } = new List<Artikal>();
}
