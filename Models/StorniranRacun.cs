using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zlatara.Models;

public class StorniranRacun
{
	public string RacunId { get; set; }

	public string MenadzerUser { get; set; }

	public IdentityRadnik Radnik { get; set; }

	public DateOnly Datum { get; set; }

	public DateOnly DatumStorniranja { get; set; }

	public double Cena { get; set; }

	public int? Pib { get; set; }

	public string RadnikUser { get; set; }

	



}
