namespace Zlatara.Models
{
	public class ViewModelRacun
	{
		public Racun? racun { get; set; }
		public IEnumerable<StavkaRacuna> StavkeRacuna { get; set; }
	}
}
