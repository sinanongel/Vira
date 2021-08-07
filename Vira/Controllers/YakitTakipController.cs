using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vira.Models;

namespace Vira.Controllers
{
    public class YakitTakipController : Controller
    {
        // GET: YakitTakip
        Context c = new Context();
        public ActionResult Index()
        {
            var liste = c.YakitTakips.OrderByDescending(x=> new { x.YakitAlimTarihi.Year, x.YakitAlimTarihi.Month, x.YakitAlimTarihi.Day }).ToList();
            return View(liste);
        }
        [HttpPost]
        public ActionResult ModalAc()
        {
            List<SelectListItem> aracListe = (from al in c.Araclars.ToList()
                                            select new SelectListItem
                                            {
                                                Text = al.AracPlakasi,
                                                Value = al.AraclarId.ToString()
                                            }).ToList();

            ViewBag.aListe = aracListe;

            return PartialView("YakitTakipEkle");
        }
        [HttpGet]
        public ActionResult YakitTakipEkle()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult YakitTakipEkle(YakitTakip p)
        {
            var sonYakitId = c.YakitTakips
                .Where(x => x.AraclarId == p.AraclarId)
                .OrderByDescending(y => y.YakitTakipId)
                .Select(z => z.YakitTakipId)
                .FirstOrDefault();

            if (sonYakitId != 0)
            {                
                var bKm = c.YakitTakips.Where(x => x.YakitTakipId == sonYakitId).Select(y => y.BaslangicKm).FirstOrDefault();
                var yt = c.YakitTakips.Find(sonYakitId);
                var gidilenKm = p.BaslangicKm - bKm;
                var ortTuketim = yt.YakitAlimMiktari / gidilenKm;
                yt.BitisKm = p.BaslangicKm;
                yt.GidilenKm = gidilenKm;
                yt.OrtalamaTuketim = ortTuketim;
                c.SaveChanges();
            }
            p.OncekiKayitId = sonYakitId;
            c.YakitTakips.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult YakitTakipGetir(int id)
        {
            List<SelectListItem> aracListe = (from al in c.Araclars.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = al.AracPlakasi,
                                                  Value = al.AraclarId.ToString()
                                              }).ToList();

            ViewBag.aListe = aracListe;
            var yakitTakip = c.YakitTakips.Find(id);

            return PartialView("YakitTakipGetir", yakitTakip);
        }
        public ActionResult YakitTakipGuncelle(YakitTakip p)
        {
            var yakitTakip = c.YakitTakips.Find(p.YakitTakipId);
            var oncekiKayitId = c.YakitTakips.Where(x => x.YakitTakipId == p.YakitTakipId).Select(y => y.OncekiKayitId).FirstOrDefault();
            
            if (oncekiKayitId != 0)
            {
                var bKm = c.YakitTakips.Where(x => x.YakitTakipId == oncekiKayitId).Select(y => y.BaslangicKm).FirstOrDefault();
                var yt = c.YakitTakips.Find(oncekiKayitId);
                var gidilenKm = p.BaslangicKm - bKm;
                var ortTuketim = yt.YakitAlimMiktari / gidilenKm;
                yt.BitisKm = p.BaslangicKm;
                yt.GidilenKm = gidilenKm;
                yt.OrtalamaTuketim = ortTuketim;
                c.SaveChanges();
            }

            yakitTakip.AraclarId = p.AraclarId;
            yakitTakip.YakitAlimTarihi = p.YakitAlimTarihi;
            yakitTakip.YakitAlimMiktari = p.YakitAlimMiktari;
            yakitTakip.BaslangicKm = p.BaslangicKm;
            TempData["sozlesmeGuncelle"] = "";
            c.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}