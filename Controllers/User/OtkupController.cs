using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.AspNetCore.Authorization;
using Zlatara.Data;
using Zlatara.Models;
using Humanizer;

namespace Zlatara.Controllers.User
{
    [BindProperties]
    [Authorize]    
    public class OtkupController : Controller
    {
		private readonly ZlataraContext _baza;

        public OtkupController(ZlataraContext baza)
        {
            _baza = baza;
        }
        public IActionResult Index()
        {
			ViewBag.PredCena = 0;
			return View("../Radnik/Otkup/Index");
        }


        [HttpPost]        
        public IActionResult PredloziCenu(OtkupArtikal artikalZaOtkup)
        {
            OtkupPredlog? predlog = _baza.Predlogs.Where(p => p.Naziv == artikalZaOtkup.Materijal && p.Finoca == artikalZaOtkup.Finoca && p.Ostecenje == artikalZaOtkup.Ostecenje).FirstOrDefault();       
            if ( predlog == null || predlog.TrenutnoAktivan == false) 
            {
                TempData["Error"] = "Trenutno Ne Otkupljujemo Ovaj Metal";
				ViewBag.PredCena = 0;
				return View("../Radnik/Otkup/Index");
			}
            double predCena = artikalZaOtkup.Gramaza * predlog.CenaPoGramu;
            ViewBag.PredCena = predCena;
            return View("../Radnik/Otkup/Index");
        }

        [HttpPost]
        public IActionResult ZavrsiOtkup(OtkupArtikal artikalZaOtkup)
        {
            Random rnd = new Random();
            artikalZaOtkup.DatumOtkupa = DateTime.Now;
            artikalZaOtkup.RadnikUser = User.Identity.Name;
			artikalZaOtkup.Id = rnd.NextInt64(0,1000).ToString() + artikalZaOtkup.DatumOtkupa.ToString() + artikalZaOtkup.RadnikUser;
			_baza.OtkupArtikals.Add(artikalZaOtkup);
            _baza.SaveChanges();
            TempData["Uspeh"] = "Uspesno Otkupljen Artikal";
            return RedirectToAction("Index", "Prodaja");
        }
    }
}
