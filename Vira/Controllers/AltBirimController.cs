using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vira.Models;

namespace Vira.Controllers
{
    public class AltBirimController : Controller
    {
        // GET: AltBirim
        Context c = new Context();
        public ActionResult Index()
        {
            var liste = c.AltBirims.ToList();
            return View(liste);
        }
        [HttpPost]
        public ActionResult ModalAc()
        {
            List<SelectListItem> kurumListe = (from k in c.Kurums.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = k.KurumUnvani,
                                                   Value = k.KurumId.ToString()
                                               }).ToList();
            ViewBag.kListe = kurumListe;

            return PartialView("AltBirimEkle");
        }
        [HttpGet]
        public ActionResult AltBirimEkle()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult AltBirimEkle(AltBirim p)
        {
            c.AltBirims.Add(p);
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult AltBirimGetir(int id)
        {
            List<SelectListItem> kurumListe = (from k in c.Kurums.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = k.KurumUnvani,
                                                   Value = k.KurumId.ToString()
                                               }).ToList();
            ViewBag.kListe = kurumListe;
            var aBrm = c.AltBirims.Find(id);

            return PartialView("AltBirimGetir", aBrm);
        }
        public ActionResult AltBirimGuncelle(AltBirim p)
        {
            var abrm = c.AltBirims.Find(p.AltBirimId);
            abrm.KurumId = p.KurumId;
            abrm.AltBirimAdi = p.AltBirimAdi;
            c.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}