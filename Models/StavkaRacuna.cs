using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Zlatara.Models;

public partial class StavkaRacuna
{
	[Key]
	public int RedinBroj { get; set; }

	[Key]
    public string RacunId { get; set; }
	[ForeignKey("RacunId")]
	[ValidateNever]
	public Racun Racun { get; set; }

	public int ArtikalId { get; set; }
	[ForeignKey("ArtikalId")]
	[ValidateNever]
	
	public Artikal Artikal { get; set; }

	public int Kolicina { get; set; }

    public double JedCena { get; set; }

    public double UkupnaVred { get; set; }
    
    
}
