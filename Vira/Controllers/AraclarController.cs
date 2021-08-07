using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vira.Models;

namespace Vira.Controllers
{
    public class AraclarController : Controller
    {
        // GET: Arac
        Context c = new Context();
        public ActionResult Index()
        {
            var liste = c.Araclars.ToList();
            return View(liste);
        }
        [HttpPost]
        public ActionResult ModalAc()
        {
            List<SelectListItem> aracCinsiListe = (from ac in c.AracCinsis.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = ac.AracCinsiAdi,
                                                   Value = ac.AracCinsiId.ToString()
                                               }).OrderBy(y=>y.Text).ToList();

            List<SelectListItem> calismaSekliListe = new List<SelectListItem>();
            calismaSekliListe.Add(new SelectListItem() { Text = "Demirbaş" });
            calismaSekliListe.Add(new SelectListItem() { Text = "Kiralık" });

            ViewBag.acListe = aracCinsiListe;
            ViewBag.csListe = calismaSekliListe;

            return PartialView("AracEkle");
        }
        [HttpGet]
        public ActionResult AracEkle()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult AracEkle(Araclar p)
        {
            c.Araclars.Add(p);
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult AracGetir(int id)
        {
            List<SelectListItem> aracCinsiListe = (from ac in c.AracCinsis.ToList()
                                                   select new SelectListItem
                                                   {
                                                       Text = ac.AracCinsiAdi,
                                                       Value = ac.AracCinsiId.ToString()
                                                   }).OrderBy(y => y.Text).ToList();

            List<SelectListItem> calismaSekliListe = new List<SelectListItem>();
            calismaSekliListe.Add(new SelectListItem() { Text = "Demirbaş" });
            calismaSekliListe.Add(new SelectListItem() { Text = "Kiralık" });

            ViewBag.acListe = aracCinsiListe;
            ViewBag.csListe = calismaSekliListe;

            var arac = c.Araclars.Find(id);

            return PartialView("AracGetir", arac);
        }
        public ActionResult AracGuncelle(Araclar p)
        {
            var araclar = c.Araclars.Find(p.AraclarId);
            araclar.AracCinsiId = p.AraclarId;
            araclar.AracMarka = p.AracMarka;
            araclar.AracModel = p.AracModel;
            araclar.AracPlakasi = p.AracPlakasi;
            araclar.CalismaSekli = p.CalismaSekli;
            araclar.KullanimYeri = p.KullanimYeri;
            c.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}