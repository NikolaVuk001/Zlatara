using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Zlatara.Models
{
    public class OtkupPredlog
    {
        [Key]
        public int Id { get; set; }
        public string Naziv { get; set; }
        [AllowNull]
        public string? Finoca { get; set; }
        public int Ostecenje { get; set; }
        [Range(0,int.MaxValue,ErrorMessage = "Cena Mora Biti Veca Od 0")]
        [RegularExpression(@"^\d+$",ErrorMessage = "Cena Mora Biti Broj")]
        public double CenaPoGramu { get; set; }
        public bool TrenutnoAktivan { get; set; }
    }
}
