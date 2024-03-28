using Microsoft.AspNetCore.Mvc;
using Zlatara.Data;
using Zlatara.Role;
using Zlatara.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Zlatara.Controllers.Admin
{
	[Authorize(Roles = SD.MenadzerRole)]
	public class OtkupAdminController : Controller
    {
		private readonly ZlataraContext _baza;

        public OtkupAdminController(ZlataraContext baza)
        {
            _baza = baza;
        }

        public IActionResult Index()
        {
            var data = _baza.Predlogs.OrderBy(p => p.Id);
            return View("../Admin/Otkup/Index",data);
        }

        public IActionResult OtkupljeniArtikli()
        {
            var data = _baza.OtkupArtikals;
            return View("../Admin/Otkup/OtkupljeniArtikli",data);
        }

        [HttpPost]
        public IActionResult IzmeniCenu(double cenaPoGramu, int id)
        {
            if(!ModelState.IsValid || cenaPoGramu < 0)
            {
				var data = _baza.Predlogs.OrderBy(p => p.Id);
                TempData["Error"] = "Uneti Podaci Za Cenu Su Neispravni!";
				return View("../Admin/Otkup/Index", data);
			}
            OtkupPredlog? predlog = _baza.Predlogs.FirstOrDefault(p => p.Id == id);
            if(predlog == null)
            {
                return View("NotFound");
            }
            predlog.CenaPoGramu = cenaPoGramu;
            _baza.Predlogs.Update(predlog);
            _baza.SaveChanges();
            TempData["Uspeh"] = "Uspesno Izmenjena Cena Predloga!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult NijeZaOtkup(int id)
        {
            OtkupPredlog? predlog = _baza.Predlogs.FirstOrDefault(p => p.Id == id);
            if(predlog == null)
            {
                return View("NotFound");
            }
            predlog.TrenutnoAktivan = false;
            _baza.Predlogs.Update(predlog);
            _baza.SaveChanges();
            TempData["Uspeh"] = "Uspesno Izmenjen Status";
            return RedirectToAction(nameof(Index));
        }

		public IActionResult ZaOtkup(int id)
		{
			OtkupPredlog? predlog = _baza.Predlogs.FirstOrDefault(p => p.Id == id);
			if (predlog == null)
			{
				return View("NotFound");
			}
			predlog.TrenutnoAktivan = true;
			_baza.Predlogs.Update(predlog);
			_baza.SaveChanges();
			TempData["Uspeh"] = "Uspesno Izmenjen Status";
			return RedirectToAction(nameof(Index));
		}

	}
}
