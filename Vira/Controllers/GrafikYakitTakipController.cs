using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vira.Models;
using Vira.Models.Siniflar;

namespace Vira.Controllers
{
    public class GrafikYakitTakipController : Controller
    {
        // GET: GrafikYakitTakip
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
        public ActionResult VisualizeOrtalamaTuketimResult(int Yil, int AracId)
        {
            return Json(OrtalamaTuketim(Yil, AracId), JsonRequestBehavior.AllowGet);
        }
        public List<YakitAnaliz> OrtalamaTuketim(int Yil, int AracId)
        {
            List<YakitAnaliz> ortTuketim = new List<YakitAnaliz>();
            using (var c = new Context())
            {
                int yaYil;
                decimal topYilOrtTuketim = 0;
                yaYil = Convert.ToInt32(c.Yillars.Where(x => x.YillarId == Yil).Select(y => y.Yil).FirstOrDefault());

                var ayListe = c.YakitTakips
                    .Where(x => x.AraclarId == AracId && x.YakitAlimTarihi.Year == yaYil)
                    .GroupBy(y => new { y.YakitAlimTarihi.Month })
                    .Select(z => new
                    {
                        Ay = z.Key.Month,
                        KmBilgisi = z.Sum(t => t.GidilenKm),
                        AlimMiktari = z.Sum(m => m.YakitAlimMiktari)
                    })
                    .ToList();

                var yilListe = c.YakitTakips
                    .Where(x => x.AraclarId == AracId && x.YakitAlimTarihi.Year == yaYil)
                    .GroupBy(y => new { y.YakitAlimTarihi.Year })
                    .Select(z => new
                    {
                        Yil = z.Key.Year,
                        KmBilgisi = z.Sum(t => t.GidilenKm),
                        AlimMiktari = z.Sum(m => m.YakitAlimMiktari)
                    })
                    .FirstOrDefault();

                topYilOrtTuketim = yilListe.AlimMiktari / yilListe.KmBilgisi;

                for (int i = 0; i < ayListe.Count(); i++)
                {
                    decimal KmBil = ayListe[i].KmBilgisi;
                    decimal AlMiktari = ayListe[i].AlimMiktari;
                    decimal AyOrtTuketim = 0;
                    int AyId = ayListe[i].Ay;
                    if (KmBil != 0) { AyOrtTuketim = AlMiktari / KmBil; } else { AyOrtTuketim = 0; }
                    var AyAd = c.Ays.Where(x => x.AyId == AyId).Select(y => y.AyAd).FirstOrDefault();

                    ortTuketim.Add(new YakitAnaliz
                    {
                        AyAd = AyAd,
                        KmBilgisi = KmBil,
                        AlimMiktari = AlMiktari,
                        AylikOrtalamaTuketim = Math.Round(AyOrtTuketim, 4),
                        YillikOrtalamaTuketim = Math.Round(topYilOrtTuketim, 4)
                    });
                }
            }
            return ortTuketim;
        }
    }
}