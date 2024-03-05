using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Zlatara.Models;

public partial class Radnik
{
	[Required(ErrorMessage = "Ovo Polje Je Obavezno!")]
	[RegularExpression(@"[0-9]{4}",
		ErrorMessage = "User Mora Biti 4 Broja. Primer: 1234")]
	public string User { get; set; }
	[Required(ErrorMessage = "Ovo Polje Je Obavezno!")]
	[RegularExpression(@"[0-9]{4}",
		ErrorMessage = "Password Mora Biti 4 Broja. Primer: 1234")]
	public string Password { get; set; }
	[Required(ErrorMessage = "Ovo Polje Je Obavezno!")]
	public string Ime { get; set; } = null!;
	[Required(ErrorMessage = "Ovo Polje Je Obavezno!")]
	public string Prezime { get; set; } = null!;
	
	public virtual ICollection<OtkupljenArtikal> OtkupljenArtikals { get; set; } = new List<OtkupljenArtikal>();
    
    public virtual ICollection<Racun> Racuns { get; set; } = new List<Racun>();
	
	public virtual ICollection<StorniranRacun> StorniranRacuns { get; set; } = new List<StorniranRacun>();
}
