using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zlatara.Data;
using Zlatara.Models;
using Zlatara.Role;

namespace Zlatara.Controllers.Admin
{
	[Authorize(Roles = SD.MenadzerRole)]
	public class RadnikController : Controller
	{
		private readonly ZlataraContext _baza;


		public RadnikController(ZlataraContext baza)
		{
			_baza = baza;
		}


		// Get: Radnik/
		public IActionResult Index()
		{
			var Radnici = _baza.Users.OrderBy(r => r.UserName);
			foreach (var r in Radnici)
			{

			}
			return View("../Admin/Radnik/Index", Radnici);
		}

		//{//// Get: Radnik/Izmeni/id
		//public IActionResult Izmeni(string id)
		//{
		//	var radnik = _baza.Radniks.Find(id);

		//	if (radnik == null)
		//	{
		//		return View("../Admin/Radnik/NotFound");
		//	}
		//	return View("../Admin/Radnik/Izmeni", radnik);
		//}


		//[HttpPost]
		//public async Task<IActionResult> Izmeni(string id, [Bind("User,Password,Ime,Prezime")] Radnik radnik)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		return View("../Admin/Radnik/Izmeni", radnik);
		//	}

		//	_baza.Update(radnik);
		//	await _baza.SaveChangesAsync();
		//	TempData["Uspeh"] = "Radnik Je Uspesno Izmenjen";
		//	return RedirectToAction(nameof(Index));

		//}}
		

		// Get: Radnik/Dodaj
		public IActionResult Dodaj()
		{
			return View("/Areas/Identity/Pages/Account/Register.cshtml");
		}
		//[HttpPost]
		//public async Task<IActionResult> Dodaj([Bind("User,Password,Ime,Prezime")] Radnik radnik)
		//{
			
			

		//	if (!ModelState.IsValid)
		//	{
		//		return View("../Admin/Radnik/Dodaj", radnik);
		//	}

		//	if (_baza.Radniks.ToList().Any(a => a.User == radnik.User))
		//	{
		//		TempData["Error"] = "User Vec Postoji";
		//		return View("../Admin/Radnik/Dodaj", radnik);

		//	}
		//	await _baza.Radniks.AddAsync(radnik);
		//	await _baza.SaveChangesAsync();
		//	TempData["Uspeh"] = "Radnik Je Uspesno Dodat";
		//	return RedirectToAction(nameof(Index));
		//}


		// Get: Radnik/Obrisi/Id
		public IActionResult Obrisi(string id)
		{
			var radnik = _baza.Users.Where(r => r.Id == id).FirstOrDefault();

			if (radnik == null)
			{
				return View("NotFound");
			}

			_baza.Users.Remove(radnik);
			_baza.SaveChanges();
			TempData["Uspeh"] = "Radnik Je Uspesno Obrisan";
			return RedirectToAction(nameof(Index));
		}		
	}
}