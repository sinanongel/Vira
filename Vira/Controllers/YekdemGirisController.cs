using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vira.Models;

namespace Vira.Controllers
{
    public class YekdemGirisController : Controller
    {
        // GET: YekdemGiris
        Context c = new Context();
        public ActionResult Index()
        {
            var liste = c.Yekdems.OrderBy(x=>x.YekdemId).ToList();
            return View(liste);
        }
        [HttpPost]
        public ActionResult ModalAc()
        {
            List<SelectListItem> donemYil = (from t in c.Yillars.ToList()
                                             orderby t.Yil descending
                                             select new SelectListItem
                                             {
                                                 Text = t.Yil,
                                                 Value = t.YillarId.ToString()
                                             }).ToList();

            List<SelectListItem> donemAy = (from t in c.Ays.ToList()
                                            select new SelectListItem
                                            {
                                                Text = t.AyAd,
                                                Value = t.AyId.ToString()
                                            }).ToList();
            ViewBag.dYil = donemYil;
            ViewBag.dAy = donemAy;

            return PartialView("YekdemEkle");
        }
        [HttpGet]
        public ActionResult YekdemEkle()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult YekdemEkle(Yekdem p)
        {
            var yil = c.Yillars.Where(x => x.YillarId == p.YillarId).Select(y => y.Yil).FirstOrDefault();
            DateTime DonemTarih= new DateTime(Convert.ToInt32(yil), Convert.ToInt32(p.AyId), 1);
            p.DonemTarih = DonemTarih;
            c.Yekdems.Add(p);
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult YekdemGetir(int id)
        {
            List<SelectListItem> donemYil = (from t in c.Yillars.ToList()
                                             orderby t.Yil descending
                                             select new SelectListItem
                                             {
                                                 Text = t.Yil,
                                                 Value = t.YillarId.ToString()
                                             }).ToList();

            List<SelectListItem> donemAy = (from t in c.Ays.ToList()
                                            select new SelectListItem
                                            {
                                                Text = t.AyAd,
                                                Value = t.AyId.ToString()
                                            }).ToList();
            ViewBag.dYil = donemYil;
            ViewBag.dAy = donemAy;

            var yekdemId = c.Yekdems.Find(id);

            return PartialView("YekdemGetir", yekdemId);
        }
        public ActionResult YekdemGuncelle(Yekdem p)
        {
            var yekdem = c.Yekdems.Find(p.YekdemId);
            var yil = c.Yillars.Where(x => x.YillarId == p.YillarId).Select(y => y.Yil).FirstOrDefault();
            DateTime DonemTarih = new DateTime(Convert.ToInt32(yil), Convert.ToInt32(p.AyId), 1);
            yekdem.YillarId = p.YillarId;
            yekdem.AyId = p.AyId;
            yekdem.YekdemTutar = p.YekdemTutar;
            yekdem.BirimFiyatUsd = p.BirimFiyatUsd;
            yekdem.DonemTarih = DonemTarih;
            c.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}