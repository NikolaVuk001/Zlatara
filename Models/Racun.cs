using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;


namespace Zlatara.Models;

public partial class Racun
{
    
	public string? RacunId { get; set; }

    public DateOnly Datum { get; set; }

    public double Cena { get; set; }

    public int? Pib { get; set; }

    

    public string UserRadnika { get; set; }
    
	[ValidateNever]
    [ForeignKey("UserRadnika")]
    public IdentityRadnik Radnik { get; set; }

  
}
