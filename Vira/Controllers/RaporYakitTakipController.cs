using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vira.Models;
using Vira.Models.Siniflar;

namespace Vira.Controllers
{
    public class RaporYakitTakipController : Controller
    {
        // GET: RaporYakıtTakip
        Context c = new Context();
        public ActionResult Index()
        {
            List<SelectListItem> aracPlaka = (from ap in c.Araclars.ToList()
                                              orderby ap.AracPlakasi
                                              select new SelectListItem
                                              {
                                                  Text = ap.AracPlakasi,
                                                  Value = ap.AraclarId.ToString()
                                              }).ToList();
            ViewBag.aPlaka = aracPlaka;

            List<SelectListItem> yil = (from t in c.Yillars.ToList()
                                        orderby t.Yil descending
                                        select new SelectListItem
                                        {
                                            Text = t.Yil,
                                            Value = t.YillarId.ToString()
                                        }).ToList();
            ViewBag.yYil = yil;

            return View();
        }
        int yaYil;
        public PartialViewResult RaporListe(int? Yil, int? AracId)
        {
            if (Yil != null && AracId != null)
            {
                yaYil = Convert.ToInt32(c.Yillars.Where(x => x.YillarId == Yil).Select(y => y.Yil).FirstOrDefault());

                var aracPlaka = c.Araclars.Where(x => x.AraclarId == AracId).Select(y => y.AracPlakasi).FirstOrDefault();
                ViewBag.Plaka = aracPlaka;
                ViewBag.Yil = yaYil;
            }

            var liste = c.YakitTakips
                .Where(x => x.AraclarId == AracId && x.YakitAlimTarihi.Year == yaYil)
                .GroupBy(y => new { y.YakitAlimTarihi.Month })
                .Select(z => new
                {
                    Ay = z.Key.Month,
                    KmBilgisi = z.Sum(t => t.GidilenKm),
                    AlimMiktari = z.Sum(m => m.YakitAlimMiktari)
                })
                .ToList();

            List<YakitAnaliz> yaListe = new List<YakitAnaliz>();

            for (int i = 0; i < liste.Count(); i++)
            {
                decimal KmBil = liste[i].KmBilgisi;
                decimal AlMiktari = liste[i].AlimMiktari;
                decimal AyOrtTuketim = 0;
                int AyId = liste[i].Ay;
                if (KmBil != 0) { AyOrtTuketim = AlMiktari / KmBil; } else { AyOrtTuketim = 0; }
                var AyAd = c.Ays.Where(x => x.AyId == AyId).Select(y => y.AyAd).FirstOrDefault();

                yaListe.Add(new YakitAnaliz
                {
                    AyAd = AyAd,
                    KmBilgisi = KmBil,
                    AlimMiktari = AlMiktari,
                    AylikOrtalamaTuketim = Math.Round(AyOrtTuketim, 4)
                });
            }

            return PartialView("RaporListe", yaListe);
        }
    }
}