using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vira.Models;

namespace Vira.Controllers
{
    public class FirmaController : Controller
    {
        // GET: Firma
        Context c = new Context();
        public ActionResult Index()
        {
            var liste = c.Firmas.ToList();
            return View(liste);
        }
        [HttpPost]
        public ActionResult ModalAc()
        {
            List<SelectListItem> ilListe = (from t in c.Illers.ToList()
                                            select new SelectListItem
                                            {
                                                Text = t.Sehir,
                                                Value = t.IllerId.ToString()
                                            }).ToList();

            ViewBag.iList = ilListe;

            return PartialView("FirmaEkle");
        }
        [HttpGet]
        public ActionResult FirmaEkle()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult FirmaEkle(Firma p)
        {
            c.Firmas.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult IlceListe(int IlId)
        {
            List<Ilceler> ilceList = c.Ilcelers.Where(i => i.IllerId == IlId).OrderBy(i => i.IlceAdi).ToList();

            List<SelectListItem> ilceListe = (from i in ilceList
                                              select new SelectListItem
                                              {
                                                  Text = i.IlceAdi,
                                                  Value = i.IlcelerId.ToString()
                                              }).ToList();

            return Json(ilceListe, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FirmaGetir(int? id)
        {
            List<SelectListItem> ilListe = (from i in c.Illers.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.Sehir,
                                                Value = i.IllerId.ToString()
                                            }).ToList();

            ViewBag.iList = ilListe;

            var il = c.Firmas.Where(x => x.FirmaId == id).FirstOrDefault();
            List<Ilceler> ilceList = c.Ilcelers.Where(i => i.IllerId == il.IllerId).OrderBy(i => i.IlceAdi).ToList();

            List<SelectListItem> ilceListe = (from i in ilceList
                                              select new SelectListItem
                                              {
                                                  Text = i.IlceAdi,
                                                  Value = i.IlcelerId.ToString()
                                              }).ToList();

            ViewBag.ilcList = ilceListe;

            var firmaGet = c.Firmas.Find(id);
            return PartialView("FirmaGetir", firmaGet);
        }
        public ActionResult FirmaGuncelle(Firma p)
        {
            var firma = c.Firmas.Find(p.FirmaId);
            firma.FirmaAdi = p.FirmaAdi;
            firma.FirmaUnvani = p.FirmaUnvani;
            firma.FirmaAdresi = p.FirmaAdresi;
            firma.IllerId = p.IllerId;
            firma.IlcelerId = p.IlcelerId;
            firma.FirmaTelefon = p.FirmaTelefon;
            c.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}