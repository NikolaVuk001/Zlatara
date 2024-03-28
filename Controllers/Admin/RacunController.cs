using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zlatara.Models;
using Zlatara.Data;
using Microsoft.AspNetCore.Authorization;
using Zlatara.Role;

namespace Zlatara3.Controllers
{
    [Authorize(Roles = SD.MenadzerRole)]
    public class RacunController : Controller
	{
		private readonly ZlataraContext _baza;

		public RacunController(ZlataraContext baza)
		{
			_baza = baza;
		}
		
		public IActionResult Index()
		{
			//var data =  _baza.Racuns.Include(r => r.Radnik);
			var data = _baza.Racuns.Include(r => r.Radnik);
			return View("../Admin/Racun/Index",data);
		}
		// Get: Racun/StornoRacuni
        public IActionResult StornoRacuni()
        {
            var data = _baza.StorniranRacuns.Include(s => s.Radnik);
            return View("../Admin/Racun/StornoRacuni", data);
        }

        // Get: Racun/Delete/Id
        public IActionResult Storno(string id)
		{

            ViewModelRacun racunInfo = new ViewModelRacun();
			racunInfo.racun = _baza.Racuns.Include(r => r.Radnik).FirstOrDefault(r => r.RacunId == id);
			

            racunInfo.StavkeRacuna = _baza.StavkaRacunas
                .Include(a => a.Artikal)
                .Where(s => s.RacunId == id)
                .ToList();

            if (racunInfo == null)
            {
                return View("NotFound");
            }
            return View("../Admin/Racun/Storno", racunInfo);

            
		}

		[HttpPost, ActionName("Storno")]
		
		public async Task<IActionResult> StornoConf(string id)
		{
			var racun = _baza.Racuns.FirstOrDefault(r => r.RacunId == id);

			if (racun == null)
			{
				return View("../Admin/Racun/NotFound");
			}

			

			StorniranRacun stornoRacun = new StorniranRacun();
			stornoRacun.RacunId = racun.RacunId;
			stornoRacun.MenadzerUser = _baza.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault().Id;
            stornoRacun.Datum = racun.Datum;
			stornoRacun.DatumStorniranja = DateOnly.FromDateTime(DateTime.Now); ;
			stornoRacun.Cena = racun.Cena;
			stornoRacun.Pib = racun.Pib;
			stornoRacun.RadnikUser = racun.UserRadnika;
			stornoRacun.Radnik = _baza.Users.FirstOrDefault(r => r.Id == stornoRacun.MenadzerUser);

			StavkaRacuna[] stavkeRacuna = _baza.StavkaRacunas.Where(p => p.RacunId == id).ToArray();

			_baza.Racuns.Remove(racun);
			_baza.StavkaRacunas.RemoveRange(stavkeRacuna);
			await _baza.StorniranRacuns.AddAsync(stornoRacun);
            _baza.SaveChanges();
            TempData["Uspeh"] = "Racun Je Uspesno Storiran";
			return RedirectToAction(nameof(Index));

		}


		//Get: Racun/Info/id
		public IActionResult Info(string id)
		{
			ViewModelRacun racunInfo = new ViewModelRacun();
			racunInfo.racun = _baza.Racuns.Include(r => r.Radnik).FirstOrDefault(r => r.RacunId == id);



			racunInfo.StavkeRacuna = _baza.StavkaRacunas
				.Include(a => a.Artikal)
				.Where(s => s.RacunId == id)
				.ToList();



			if (racunInfo == null)
			{
				return View("NotFound");
			}
			return View("../Admin/Racun/Info", racunInfo);
		}

		//POST: Racun/PronadjiRacun/id
		public IActionResult PronadjiRacun(string id)
		{
			var data = _baza.Racuns.Include(r => r.Radnik).Where(r => r.RacunId.Contains(id));

			return View("../Admin/Racun/Index", data);

		}

    }
}
