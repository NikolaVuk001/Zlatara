using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Zlatara.Data;
using Zlatara.Models;

namespace Zlatara.Controllers.User
{
    [Authorize]
    public class ProdajaController : Controller
    {

        private readonly ZlataraContext _baza;

        public IEnumerable<Artikal> Artikli { get; set; }
        public IEnumerable<SelectListItem> ListaKategorija { get; set; }
        public IEnumerable<SelectListItem> ListaBrendova { get; set; }

        [BindProperty]
        public Korpa korpa { get; set; }
        public IEnumerable<Korpa> UserKorpa { get; set; }
        public static string? UserID { get; set; }


        public ProdajaController(ZlataraContext baza)
        {
            _baza = baza;
            PopuniListe();
            PopuniArtikle();
        }

        

        private void PopuniUserKorpa()
        {
            UserID = _baza.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault().Id;
            UserKorpa = _baza.Korpas.Include(k => k.Artikal).Where(k => k.RadnikUser == UserID).ToList();
        }

        private void PopuniArtikle()
        {
            Artikli = _baza.Artikals.Include(a => a.Kategorija).Include(a => a.Brend).Where(a => a.KolicinaNaStanju > 0).ToList();
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

        public IActionResult Index()
        {
            ViewBag.ListaKategorija = ListaKategorija;
            ViewBag.ListaBrendova = ListaBrendova;
            PopuniUserKorpa();
            ViewData["Korpa"] = UserKorpa;
            return View("../Radnik/Prodaja/Index", Artikli);
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
            PopuniUserKorpa();
            ViewData["Korpa"] = UserKorpa;
            return View("../Radnik/Prodaja/Index", Artikli);
        }


        
        public IActionResult DodajUKorpu(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            Artikal artikal = _baza.Artikals.FirstOrDefault(a => a.ArtikalId == id);
			artikal.KolicinaNaStanju -= 1;
			_baza.Artikals.Update(artikal);

			// Ako Je Artikal Vec U Korpi Povecava Se Kolicina Za Jedan
			if (_baza.Korpas.Where(k => k.RadnikUser == claim.Value && k.ArtikalId == artikal.ArtikalId).FirstOrDefault() != null)
            {
                korpa = _baza.Korpas.Where(k => k.RadnikUser == claim.Value && k.ArtikalId == artikal.ArtikalId).FirstOrDefault();
                korpa.Count += 1;
                _baza.Update(korpa);
                _baza.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            //Ako Artikal Nije U Korpi Dodaje Se Nova Instanca Korpe
            korpa = new Korpa();
            if (_baza.Artikals.FirstOrDefault(a => a.ArtikalId == id).KolicinaNaStanju >= 0)
            {                
                korpa.ArtikalId = id;
                korpa.Artikal = _baza.Artikals.FirstOrDefault(a => a.ArtikalId == id);
                korpa.Count = 1;
                korpa.RadnikUser = claim.Value;

                
                _baza.Korpas.Add(korpa);
                
                _baza.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            //Ako Artikal Nije Trenutno Na Stanju
            else
            {
                TempData["Error"] = "Artikal Nije Trenutno Na Stanju";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Prodaja/UkloniIzKorpe/Id
        public IActionResult UkloniIzKorpe(int id)
        {
            int _id = id;
            string _radnikUser = UserID;
            Korpa artikalZaBrisanje = _baza.Korpas.Where(k => k.ArtikalId == id && k.RadnikUser == UserID).FirstOrDefault();
            Artikal artikal = _baza.Artikals.FirstOrDefault(a => a.ArtikalId == id);
            if (artikalZaBrisanje != null)
            {
                artikal.KolicinaNaStanju += artikalZaBrisanje.Count;
                _baza.Korpas.Remove(artikalZaBrisanje);
                _baza.SaveChanges();
                return RedirectToAction(nameof(Index), Artikli);
            }
            TempData["Error"] = "Greska Pri Uklanjanju Artikla Iz Korpe";
			return RedirectToAction(nameof(Index), Artikli);
        }

        //GET: Prodaja/Placanje
        public IActionResult Placanje()
        {
			PopuniUserKorpa();
            if(!UserKorpa.IsNullOrEmpty())
            {
				return View("../Radnik/Prodaja/Placanje", UserKorpa);
			}
			TempData["Error"] = "Korpa Je Prazana!";
			return RedirectToAction(nameof(Index), Artikli);

		}


        public IActionResult ZavrsiRacun()
        {
            double ukRacun = 0;
            int brStavki = 0;
			PopuniUserKorpa();
			foreach (var item in UserKorpa)
            {
                ukRacun += item.Artikal.Cena * item.Count;
                brStavki++;
                
            }
            if(ukRacun != 0 && brStavki != 0)
            {
				Racun racun = new Racun();
				racun.Datum = DateOnly.FromDateTime(DateTime.Now);
                racun.Cena = ukRacun;
                racun.UserRadnika = UserID;
                racun.RacunId = racun.UserRadnika + (DateTime.Now).ToString("dd-MM-yyyy HH:mm:ss");

                _baza.Racuns.Add(racun);             
				_baza.SaveChanges();

				List<StavkaRacuna> stavke = new List<StavkaRacuna>();
				List<Artikal> artikli = new List<Artikal>();
				int redBr = 0;

				var id = racun.RacunId;

				foreach (var item in UserKorpa)
                {
                    redBr++;
					stavke.Add(new StavkaRacuna()
                    {
                        RedinBroj = redBr,
                        RacunId = racun.RacunId,
                        Racun = racun,
                        ArtikalId = item.ArtikalId,
						Artikal = _baza.Artikals.FirstOrDefault(a => a.ArtikalId == item.ArtikalId),
                        Kolicina = item.Count,
                        JedCena = item.Artikal.Cena,
                        UkupnaVred = item.Count * item.Artikal.Cena
					});
                    artikli.Add(_baza.Artikals.FirstOrDefault(a => a.ArtikalId == item.ArtikalId));
                    artikli[redBr - 1].KolicinaNaStanju = artikli[redBr - 1].KolicinaNaStanju - item.Count;


				}
                
                _baza.StavkaRacunas.AddRange(stavke);
                _baza.Artikals.UpdateRange(artikli);
                _baza.RemoveRange(UserKorpa);
                _baza.SaveChanges();
                

                return RedirectToAction(nameof(Index), Artikli);
			}
            TempData["Error"] = "Korpa Je Prazana!";
            return RedirectToAction(nameof(Index), Artikli);
            
		}
    }
}

