using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zlatara.Models
{
	public class Korpa
	{
		public int Id { get; set; }
		public int ArtikalId { get; set; }
		[ForeignKey("ArtikalId")]		
		[ValidateNever]
		public Artikal Artikal { get; set; }
		public int Count { get; set; }
		[ValidateNever]
		public string RadnikUser { get; set; }
		[ForeignKey("RadnikUser")]
		
		[ValidateNever]
		public IdentityRadnik Radnik { get; set; }
	}
}
