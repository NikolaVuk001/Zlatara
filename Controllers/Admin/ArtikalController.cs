using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using System.Linq;
using Zlatara.Data;
using Zlatara.Models;
using Zlatara.Role;

namespace Zlatara.Controllers.Admin
{
    [BindProperties]
	[Authorize(Roles = SD.MenadzerRole)]    
	public class ArtikalController : Controller
    {
        

        private readonly ZlataraContext _baza;

        public IEnumerable<Artikal>? Artikli { get; set; }
        public static IEnumerable<SelectListItem>? ListaKategorija { get; set; }
		public static IEnumerable<SelectListItem>? ListaBrendova { get; set; }
		

		public ArtikalController(ZlataraContext baza)
        {
            _baza = baza;
			PopuniListe();
		}
        
        // Get: Artikal/
        public IActionResult Index()
        {
            Artikli = _baza.Artikals.Include(n => n.Brend).Include(n => n.Kategorija).ToList();
            return View("../Admin/Artikal/Index", Artikli);
        }

        // Get: Artikal/Dodaj
        public IActionResult Dodaj()
        {
            
			ViewBag.ListaKategorija = ListaKategorija;
			ViewBag.ListaBrendova = ListaBrendova;
			return View("../Admin/Artikal/Dodaj");
        }

        private void PopuniListe()
        {
			ListaKategorija = _baza.Kategorijas.Select(i => new SelectListItem()
			{
				Text = i.Naziv,
				Value = i.KategorijaId.ToString(),
			});

			ListaBrendova = _baza.Brends.Select(i => new SelectListItem()
			{
				Text = i.Naziv,
				Value = i.BrendId.ToString(),
			});

		}
        [HttpPost]
        public async Task<IActionResult> Dodaj(Artikal artikal)
        {
            if (!ModelState.IsValid)
            {
				ViewBag.ListaKategorija = ListaKategorija;
				ViewBag.ListaBrendova = ListaBrendova;
				return View("../Admin/Artikal/Dodaj", artikal);
            }
            if (_baza.Artikals.ToList().Any(a => a.ArtikalId == artikal.ArtikalId))
            {
				TempData["Error"] = "Artikal ID Vec Postoji";
				return View("../Admin/Artikal/Dodaj", artikal);				
			}
            
            await _baza.Artikals.AddAsync(artikal);
            await _baza.SaveChangesAsync();
            TempData["Uspeh"] = "Artikal Uspesno Dodat";
            return RedirectToAction(nameof(Index));
        }


        //Get: Artikal/Info/id
        public IActionResult Info(int id)
        {
            var artikalDetails = _baza.Artikals.Include(n => n.Brend).Include(n => n.Kategorija).FirstOrDefault(i => i.ArtikalId == id);
            

            if (artikalDetails == null)
            {
                return View("NotFound");
            }
            return View("../Admin/Artikal/Info", artikalDetails);
        }


        // Get: Artikal/Izmeni/id
        public IActionResult Izmeni(int id)
        {
			var artikalDetails = _baza.Artikals.Include(n => n.Brend).Include(n => n.Kategorija).FirstOrDefault(i => i.ArtikalId == id);			
			ViewBag.ListaKategorija = ListaKategorija;
			ViewBag.ListaBrendova = ListaBrendova;

			if (artikalDetails == null)
            {
                return View("NotFound");
            }
            return View("../Admin/Artikal/Izmeni", artikalDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Artikal artikal)
        {

            if (!ModelState.IsValid)
            {
				ViewBag.ListaKategorija = ListaKategorija;
				ViewBag.ListaBrendova = ListaBrendova;
				return View("../Admin/Artikal/Izmeni", artikal);
            }

            _baza.Artikals.Update(artikal);
            await _baza.SaveChangesAsync();
            TempData["Uspeh"] = "Artikal Uspesno Izmenjen";
            return RedirectToAction(nameof(Index));

        }



        // Get: Artikal/Delete/id
        public IActionResult Obrisi(int id)
        {
            var artikalDetails = _baza.Artikals.Find(id);

            if (artikalDetails == null)
            {
                return View("NotFound");
            }
            return View("../Admin/Artikal/Obrisi", artikalDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Artikal artikal)
        {
            if (artikal == null)
            {
                return View("NotFound");
            }
            _baza.Artikals.Remove(artikal);
            await _baza.SaveChangesAsync();
            TempData["Uspeh"] = "Artikal Uspesno Obrisan";
            return RedirectToAction(nameof(Index));

        }
    }
}
