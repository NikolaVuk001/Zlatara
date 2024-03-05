using System;
using System.Collections.Generic;

namespace Zlatara.Models;

public partial class OtkupljenArtikal
{
    public string Id { get; set; } = null!;

    public string? Materijal { get; set; }

    public double? Gramaza { get; set; }

    public string? Finoca { get; set; }

    public DateOnly? DatumOtkupa { get; set; }

    public string RadnikUser { get; set; }

    public double UkCenaOtkupa { get; set; }

    public virtual Radnik RadnikUserNavigation { get; set; } = null!;
}
