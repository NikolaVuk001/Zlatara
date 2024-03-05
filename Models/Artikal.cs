using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zlatara.Models;

public partial class Artikal
{    
	[Display(Name = "Artikal ID")]
	[Required(ErrorMessage = "Ovo Polje Je Obavezno!")]
	[Key]
	public int? ArtikalId { get; set; }


	[Display(Name = "Naziv")]
	[Required(ErrorMessage = "Ovo Polje Je Obavezno!")]
	public string NazivArtikla { get; set; } = null!;


	[Display(Name = "Materijal")]
	[Required(ErrorMessage = "Ovo Polje Je Obavezno!")]	
	public string Materijal { get; set; } = null!;


	[Display(Name = "Cena")]
	[Required(ErrorMessage = "Ovo Polje Je Obavezno!")]
	public double? Cena { get; set; }


	[Display(Name = "Kolicna")]
	[Required(ErrorMessage = "Ovo Polje Je Obavezno!")]
	public int? KolicinaNaStanju { get; set; }


	[Display(Name = "Kategorija ID")]
	[Required(ErrorMessage = "Ovo Polje Je Obavezno!")]
	public int? KategorijaId { get; set; }


	[Display(Name = "Opis")]
	public string? Opis { get; set; }


	[Display(Name = "Gramaza")]
	[Required(ErrorMessage = "Ovo Polje Je Obavezno!")]
	public double? Gramaza { get; set; }


	[Display(Name = "Slika")]
	public string? Slika { get; set; }


	[Display(Name = "Brend ID")]
	public int? BrendId { get; set; }


	[Display(Name = "Kamen ID")]
	public int? KamenId { get; set; }



	public virtual Brend? Brend { get; set; }

	public virtual Kamenje? Kamen { get; set; }

	public virtual Kategorija? Kategorija { get; set; }

	public virtual ICollection<StavkaRacuna> StavkaRacunas { get; set; } = new List<StavkaRacuna>();
}
