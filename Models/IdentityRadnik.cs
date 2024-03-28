using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Zlatara.Models
{
    public class IdentityRadnik : IdentityUser
    {       

        [Required(ErrorMessage = "Ovo Polje Je Obavezno!")]
        public string Ime { get; set; } = null!;
        [Required(ErrorMessage = "Ovo Polje Je Obavezno!")]
        public string Prezime { get; set; } = null!;
        [AllowNull]
        public string Pozicija { get; set; }
    }
}

