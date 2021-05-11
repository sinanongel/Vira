using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vira.Models;

namespace Vira.Controllers
{
    public class MalHizmetController : Controller
    {
        // GET: MlHizmet
        Context c = new Context();
        public ActionResult Index()
        {
            var liste = c.MalHizmets.ToList();
            return View(liste);
        }
        [HttpPost]
        public ActionResult ModalAc()
        {
            List<SelectListItem> birimListe = (from b in c.Birims.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = b.BirimAdi,
                                                   Value = b.BirimId.ToString()
                                               }).ToList();

            List<SelectListItem> turListe = new List<SelectListItem>();
            turListe.Add(new SelectListItem() { Text = "Alış" });
            turListe.Add(new SelectListItem() { Text = "Satış" });

            ViewBag.brmListe = birimListe;
            ViewBag.tListe = turListe;

            return PartialView("MalHizmetEkle");
        }
        [HttpGet]
        public ActionResult MalHizmetEkle()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult MalHizmetEkle(MalHizmet p)
        {
            c.MalHizmets.Add(p);
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult MalHizmetGetir(int id)
        {
            List<SelectListItem> birimListe = (from b in c.Birims.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = b.BirimAdi,
                                                   Value = b.BirimId.ToString()
                                               }).ToList();

            List<SelectListItem> turListe = new List<SelectListItem>();
            turListe.Add(new SelectListItem() { Text = "Alış" });
            turListe.Add(new SelectListItem() { Text = "Satış" });

            ViewBag.brmListe = birimListe;
            ViewBag.tListe = turListe;
            var mlHiz = c.MalHizmets.Find(id);

            return PartialView("MalHizmetGetir", mlHiz);
        }
        public ActionResult MalHizmetGuncelle(MalHizmet p)
        {
            var mlhmt = c.MalHizmets.Find(p.MalHizmetId);
            mlhmt.StokKod = p.StokKod;
            mlhmt.MalHizmetAdi = p.MalHizmetAdi;
            mlhmt.MalHizmetTuru = p.MalHizmetTuru;
            mlhmt.BirimId = p.BirimId;
            c.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}