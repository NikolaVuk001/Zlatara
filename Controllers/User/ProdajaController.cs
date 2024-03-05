using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zlatara.Data;
using Zlatara.Models;

namespace Zlatara.Controllers.User
{
	public class ProdajaController : Controller
	{

		private readonly ZlataraContext _baza;

		public IEnumerable<Artikal> Artikli { get; set; }
		public IEnumerable<SelectListItem> ListaKategorija { get; set; }
		public IEnumerable<SelectListItem> ListaBrendova { get; set; }


		public ProdajaController(ZlataraContext baza)
        {
			_baza = baza;
			PopuniListe();
			
        }
        public IActionResult Index()
		{
			Artikli = _baza.Artikals.Include(a => a.Kategorija).Include(a => a.Brend).Where(a => a.KolicinaNaStanju > 0).ToList();
			ViewBag.ListaKategorija = ListaKategorija;
			ViewBag.ListaBrendova = ListaBrendova;
			return View("../Radnik/Index", Artikli);
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


		[HttpPost("Filter")]
		public IActionResult Filter(int id, string naziv, string materijal, double cena, int kategorija, double gramaza, int brend)
		{
			Artikli = _baza.Artikals.Include(a => a.Kategorija).Include(a => a.Brend).ToList();
			if (id != 0)
			{
				Artikli = Artikli.Where(n => n.ArtikalId == id);
			}
			if (!string.IsNullOrEmpty(naziv))
			{
				Artikli = Artikli.Where(n => n.NazivArtikla.ToLower().Contains(naziv.ToLower()));
			}
			if (!string.IsNullOrEmpty(materijal))
			{
				Artikli = Artikli.Where(n => n.Materijal.ToLower().Contains(materijal.ToLower()));
			}
			if (cena != 0)
			{
				Artikli = Artikli.Where(n => n.Cena == cena);
			}
			if (kategorija != 0)
			{
				Artikli = Artikli.Where(n => n.KategorijaId == kategorija);
			}
			if (gramaza != 0)
			{
				Artikli = Artikli.Where(n => n.Gramaza == gramaza);
			}
			if (brend != 0)
			{
				Artikli = Artikli.Where(n => n.BrendId == brend);
			}

			ViewBag.ListaKategorija = ListaKategorija;
			ViewBag.ListaBrendova = ListaBrendova;
			return View("../Radnik/Index", Artikli);

		}

	}
}

