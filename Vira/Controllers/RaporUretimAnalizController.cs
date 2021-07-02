using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vira.Models;
using Vira.Models.Siniflar;

namespace Vira.Controllers
{
    public class RaporUretimAnalizController : Controller
    {
        // GET: RaporUretimAnaliz
        Context c = new Context();
        public ActionResult Index()
        {
            List<SelectListItem> donemYil = (from t in c.Yillars.ToList()
                                             orderby t.Yil descending
                                             select new SelectListItem
                                             {
                                                 Text = t.Yil,
                                                 Value = t.YillarId.ToString()
                                             }).ToList();
            ViewBag.dYil = donemYil;

            List<SelectListItem> donemAy = (from t in c.Ays.ToList()
                                            orderby t.AyId
                                            select new SelectListItem
                                            {
                                                Text = t.AyAd,
                                                Value = t.AyId.ToString()
                                            }).ToList();
            ViewBag.dAy = donemAy;

            return View();
        }

        public PartialViewResult RaporListe(UretimAnaliz p)
        {
            return PartialView("RaporListe", UretimAnalizRaporu(p));
        }
        public ActionResult RaporYillarListe(UretimAnaliz p)
        {
            var yekdemListe = c.Yekdems
               .GroupBy(y => new { y.Yillar.Yil })
               .OrderBy(y => new { y.Key.Yil })
               .Select(y => new
               {
                   Yil = y.Key.Yil,
                   YekdemTutar = y.Sum(z => z.YekdemTutar)
               })
               .ToList();

            var bFiyatUsd = c.Yekdems.Select(y => y.BirimFiyatUsd).FirstOrDefault();

            ViewBag.BirimFiyatUsd = bFiyatUsd;

            List<UretimAnaliz> uaListe = new List<UretimAnaliz>();

            for (int i = 0; i < yekdemListe.Count(); i++)
            {
                string yil = yekdemListe[i].Yil;
                decimal aylıkUretim = 0;
                decimal yekdemTutariTl = 0;
                decimal yekdemTutariUsd = 0;
                decimal ortalamaAylikUsdTl = 0;
                decimal faturaToplamV = 0;
                decimal faturaToplamE = 0;
                decimal faturaFarkıTl = 0;
                decimal faturaFarkıUsd = 0;
                decimal karZarar = 0;

                var yilId = c.Yekdems.Where(x => x.Yillar.Yil == yil).Select(y => y.YillarId).FirstOrDefault();
                p.Yil = yilId;

                var uretimRapor = UretimAnalizRaporu(p);
                for (int j = 0; j < uretimRapor.Count(); j++)
                {
                    aylıkUretim = aylıkUretim + uretimRapor[j].AylikUretim;
                    yekdemTutariTl = yekdemTutariTl + uretimRapor[j].YekdemTutari;
                    yekdemTutariUsd = yekdemTutariUsd + uretimRapor[j].YekdemTutariUsd;
                    ortalamaAylikUsdTl = uretimRapor[j].OrtalamaAylikUsdTl;
                    faturaToplamV = faturaToplamV + uretimRapor[j].ViraFaturaDegeri;
                    faturaToplamE = faturaToplamE + uretimRapor[j].EpiaşFaturaDegeri;
                    faturaFarkıTl = faturaFarkıTl + uretimRapor[j].FaturaFarkıTl;
                    faturaFarkıUsd = faturaFarkıUsd + uretimRapor[j].FaturaFarkıUsd;
                    karZarar = karZarar + uretimRapor[j].KarZarar;
                }
                if (yekdemTutariUsd == 0) { ortalamaAylikUsdTl = 0; } else { ortalamaAylikUsdTl = Math.Round((yekdemTutariTl / yekdemTutariUsd), 5); }

                uaListe.Add(new UretimAnaliz
                {
                    Yil = Convert.ToInt32(yil),
                    AylikUretim = aylıkUretim,
                    YekdemTutari = yekdemTutariTl,
                    YekdemTutariUsd = yekdemTutariUsd,
                    OrtalamaAylikUsdTl = ortalamaAylikUsdTl,
                    ViraFaturaDegeri = faturaToplamV,
                    EpiaşFaturaDegeri = faturaToplamE,
                    FaturaFarkıTl = faturaFarkıTl,
                    FaturaFarkıUsd = faturaFarkıUsd,
                    KarZarar = faturaFarkıUsd - yekdemTutariUsd

                });
            }
            return View(uaListe);
        }
        public List<UretimAnaliz> UretimAnalizRaporu(UretimAnaliz p)
        {
            List<UretimAnaliz> UaListe = new List<UretimAnaliz>();

            var yekdemListe = c.Yekdems
               .Where(x => x.YillarId == p.Yil)
               .OrderBy(y => new { y.Yillar.Yil, y.AyId })
               .Select(y => new
               {
                   Yil = y.Yillar.Yil,
                   AyAd = y.Ay.AyAd,
                   YekdemTutar = y.YekdemTutar,
                   BirimFiyatUsd = y.BirimFiyatUsd
               })
               .ToList();

            var bFiyatUsd = c.Yekdems.Where(x => x.YillarId == p.Yil).Select(y => y.BirimFiyatUsd).FirstOrDefault();

            ViewBag.BirimFiyatUsd = bFiyatUsd;

            for (int i = 0; i < yekdemListe.Count(); i++)
            {
                string ayAd = yekdemListe[i].AyAd;
                string yil = yekdemListe[i].Yil;
                decimal ortalamaAylikUsdTl = 0;
                decimal faturaFarkıUsd = 0;
                decimal birimFiyatUsd = 0;
                decimal kumulatif = 0;

                var viraIade = c.FaturaDetays.Where(x => x.Fatura.Yillar.Yil == yil && x.Fatura.Ay.AyAd == ayAd && x.Fatura.KurumId == 3 && x.Fatura.FaturaTipi == "İade")
                    .Select(y => y.FdMiktar).FirstOrDefault();

                var teiasMiktar = c.FaturaDetays.Where(x => x.MalHizmetId == 9 && x.Fatura.Yillar.Yil == yil && x.Fatura.Ay.AyAd == ayAd && x.Fatura.FaturaTuru == "Veriş")
                    .Select(y => y.FdMiktar).FirstOrDefault();

                var viraFatura = c.Faturas.Where(x => x.Yillar.Yil == yil && x.Ay.AyAd == ayAd && x.KurumId == 3 && x.FaturaTipi != "İade")
                    .Select(y => y.FaturaToplami).FirstOrDefault();

                var epiaşFatura = c.Faturas.Where(x => x.Yillar.Yil == yil && x.Ay.AyAd == ayAd && x.KurumId == 1).Select(y => y.FaturaToplami).FirstOrDefault();

                birimFiyatUsd = yekdemListe[i].BirimFiyatUsd;
                decimal yekdemTutariTl = yekdemListe[i].YekdemTutar;
                decimal yekdemTutariUsd = birimFiyatUsd * teiasMiktar;
                decimal faturaFarkıTl = viraFatura - epiaşFatura;

                if (yekdemTutariUsd == 0)
                {
                    ortalamaAylikUsdTl = 0;
                    faturaFarkıUsd = 0;
                }
                else
                {
                    ortalamaAylikUsdTl = Math.Round((yekdemTutariTl / yekdemTutariUsd), 5);
                    faturaFarkıUsd = Math.Round((faturaFarkıTl / ortalamaAylikUsdTl), 2);
                }

                if (yekdemListe[i].AyAd == "Ocak") { kumulatif = teiasMiktar; } else { kumulatif = teiasMiktar + UaListe[i - 1].Kumulatif; }

                UaListe.Add(new UretimAnaliz
                {
                    Donem = ayAd + "/" + yil,
                    AylikUretim = teiasMiktar,
                    Kumulatif = kumulatif,
                    YekdemTutari = yekdemTutariTl,
                    YekdemTutariUsd = yekdemTutariUsd,
                    OrtalamaAylikUsdTl = ortalamaAylikUsdTl,
                    ViraFaturaDegeri = viraFatura - viraIade,
                    EpiaşFaturaDegeri = epiaşFatura,
                    FaturaFarkıTl = faturaFarkıTl,
                    FaturaFarkıUsd = faturaFarkıUsd,
                    KarZarar = faturaFarkıUsd - yekdemTutariUsd

                });

                birimFiyatUsd = 0;
                yekdemTutariTl = 0;
                yekdemTutariUsd = 0;
                ortalamaAylikUsdTl = 0;
                faturaFarkıTl = 0;
                faturaFarkıUsd = 0;
                kumulatif = 0;
            }

            return UaListe;
        }
    }
}