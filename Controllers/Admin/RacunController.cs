using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zlatara.Models;
using Zlatara.Data;

namespace Zlatara3.Controllers
{
	public class RacunController : Controller
	{
		private readonly ZlataraContext _baza;

		public RacunController(ZlataraContext baza)
		{
			_baza = baza;
		}
		


		public IActionResult Index()
		{
			var data =  _baza.Racuns;
			return View("../Admin/Racun/Index",data);
		}
		// Get: Racun/StornoRacuni
        public IActionResult StornoRacuni()
        {
            var data = _baza.StorniranRacuns;
            return View("../Admin/Racun/StornoRacuni", data);
        }

        // Get: Racun/Delete/Id
        public IActionResult Storno(int id)
		{
			var racun = _baza.Racuns.Find(id);

			if (racun == null)
			{
				return View("../Admin/Racun/NotFound");
			}
			return View("../Admin/Racun/Storno",racun);
		}

		[HttpPost, ActionName("Storno")]
		
		public async Task<IActionResult> StornoConf(int id)
		{
			var racun = _baza.Racuns.Find(id);

			if (racun == null)
			{
				return View("../Admin/Racun/NotFound");
			}

			

			StorniranRacun stornoRacun = new StorniranRacun();
			stornoRacun.RacunId = racun.RacunId;
			stornoRacun.MenadzerUser = 1000;
			stornoRacun.Datum = racun.Datum;
			stornoRacun.DatumStorniranja = DateOnly.FromDateTime(DateTime.Now); ;
			stornoRacun.Cena = racun.Cena;
			stornoRacun.Pib = racun.Pib;
			stornoRacun.RadnikUser = racun.RadnikUser;

			StavkaRacuna[] stavkeRacuna = _baza.StavkaRacunas.Where(p => p.RacunId == id).ToArray();
			
			_baza.Racuns.Remove(racun);
			_baza.StavkaRacunas.RemoveRange(stavkeRacuna);
			await _baza.StorniranRacuns.AddAsync(stornoRacun);
			await _baza.SaveChangesAsync();
			TempData["Uspeh"] = "Racun Je Uspesno Storiran";
			return RedirectToAction(nameof(Index));

		}


		//Get: Racun/Info/id
		public IActionResult Info(int id)
		{
			ViewModelRacun racunInfo = new ViewModelRacun();
			racunInfo.Racuni = _baza.Racuns.Where(r => r.RacunId == id).ToList();
			racunInfo.StavkeRacuna = _baza.StavkaRacunas
				.Include(a => a.Artikal)
				.Where(s => s.RacunId == id)
				.ToList();
			


			if (racunInfo == null)
			{
				return View("../Admin/Racun/NotFound");
			}
			return View("../Admin/Racun/Info", racunInfo);
		}



	}
}
