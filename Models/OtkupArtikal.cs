using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Zlatara.Models
{
    public class OtkupArtikal
    {
        [Key]
        public string Id { get; set; }
        public string Materijal { get; set; }
        public double Gramaza { get; set; }
        public string? Finoca { get; set; }        
        public int Ostecenje { get; set; }
        public string RadnikUser { get; set; }
        public DateTime DatumOtkupa { get; set; }
        public double ProdajnaCena { get; set; }
    }
}
