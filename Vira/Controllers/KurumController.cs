using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vira.Models;

namespace Vira.Controllers
{
    public class KurumController : Controller
    {
        // GET: Kurum
        Context c = new Context();
        public ActionResult Index()
        {
            var liste = c.Kurums.ToList();
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

            return PartialView("KurumEkle");
        }
        [HttpGet]
        public ActionResult KurumEkle()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult KurumEkle(Kurum p)
        {
            c.Kurums.Add(p);
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
        public ActionResult KurumGetir(int? id)
        {
            List<SelectListItem> ilListe = (from i in c.Illers.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.Sehir,
                                                Value = i.IllerId.ToString()
                                            }).ToList();

            ViewBag.iList = ilListe;

            var il = c.Kurums.Where(x => x.KurumId == id).FirstOrDefault();
            List<Ilceler> ilceList = c.Ilcelers.Where(i => i.IllerId == il.IllerId).OrderBy(i => i.IlceAdi).ToList();

            List<SelectListItem> ilceListe = (from i in ilceList
                                              select new SelectListItem
                                              {
                                                  Text = i.IlceAdi,
                                                  Value = i.IlcelerId.ToString()
                                              }).ToList();

            ViewBag.ilcList = ilceListe;

            var kurumGet = c.Kurums.Find(id);
            return PartialView("KurumGetir", kurumGet);
        }
        public ActionResult KurumGuncelle(Kurum p)
        {
            var kurum = c.Kurums.Find(p.KurumId);
            kurum.KurumAdi = p.KurumAdi;
            kurum.KurumIlgiliKisi = p.KurumIlgiliKisi;
            kurum.KurumAdres = p.KurumAdres;
            kurum.IllerId = p.IllerId;
            kurum.IlcelerId = p.IlcelerId;
            kurum.KurumTelefon = p.KurumTelefon;
            kurum.KurumGsm = p.KurumGsm;
            kurum.KurumEPosta = p.KurumEPosta;
            c.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}