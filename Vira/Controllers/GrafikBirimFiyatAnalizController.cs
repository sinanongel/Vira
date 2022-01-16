using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vira.Models;
using Vira.Models.Siniflar;

namespace Vira.Controllers
{
    public class GrafikBirimFiyatAnalizController : Controller
    {
        // GET: Grafik
        Context c = new Context();
        public ActionResult Index()
        {
            List<SelectListItem> malHizGrup = (from m in c.MalHizmetGrups.ToList()
                                              orderby m.MalHizmetGrupAdi
                                              select new SelectListItem
                                              {
                                                  Text = m.MalHizmetGrupAdi,
                                                  Value = m.MalHizmetGrupId.ToString()
                                              }).ToList();
            ViewBag.MalHizGrup = malHizGrup;

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
        [HttpPost]
        public JsonResult MalHizmetGetir(int MalHizGrupId)
        {
            var malHizmet = c.MalHizmetGrups.Where(x => x.MalHizmetGrupId == MalHizGrupId).Select(y => y.MalHizmetGrupId).FirstOrDefault();

            List<MalHizmet> malHizmetList = c.MalHizmets.Where(i => i.MalHizmetGrupId == MalHizGrupId).OrderBy(i => i.MalHizmetAdi).ToList();

            List<SelectListItem> malHizmetListe = (from i in malHizmetList
                                                   select new SelectListItem
                                                   {
                                                       Text = i.MalHizmetAdi,
                                                       Value = i.MalHizmetId.ToString()
                                                   }).ToList();

            return Json(malHizmetListe, JsonRequestBehavior.AllowGet);
        }
        public ActionResult VisualizeOrtalamaBirimFiyatResult(int Yil, int MalHizId)
        {
            return Json(OrtalamaBirimFiyat(Yil, MalHizId), JsonRequestBehavior.AllowGet);
        }
        public List<BirimFiyatAnaliz> OrtalamaBirimFiyat(int Yil, int MalHizId)
        {
            List<BirimFiyatAnaliz> ortBirimFiyat = new List<BirimFiyatAnaliz>();
            using (var c = new Context())
            {
                int yaYil;
                decimal topYilOrtBirimFiyat = 0;
                yaYil = Convert.ToInt32(c.Yillars.Where(x => x.YillarId == Yil).Select(y => y.Yil).FirstOrDefault());

                var bfListe = c.FaturaDetays
                    .Where(x => x.MalHizmetId == MalHizId && x.Fatura.FaturaTarihi.Year == yaYil)
                    .Select(z => new
                    {
                        FaturaTarihi = z.Fatura.FaturaTarihi,
                        BirimFiyat = z.FdBirimFiyatTl
                    })
                    .OrderBy(z => z.FaturaTarihi).ToList();

                var yilListe = c.FaturaDetays
                    .Where(x => x.MalHizmetId == MalHizId && x.Fatura.FaturaTarihi.Year == yaYil)
                    .GroupBy(y => new { y.Fatura.FaturaTarihi.Year })
                    .Select(z => new
                    {
                        Yil = z.Key.Year,
                        BirimFiyat = z.Sum(t => t.FdBirimFiyatTl)
                    })
                    .FirstOrDefault();

                topYilOrtBirimFiyat = yilListe.BirimFiyat / bfListe.Count;

                for (int i = 0; i < bfListe.Count(); i++)
                {
                    decimal BirimFiyat = bfListe[i].BirimFiyat;
                    //decimal AyOrtBirimFiyat = 0;
                    string FaturaTarihi = bfListe[i].FaturaTarihi.ToString("d");
                    //var adet = c.FaturaDetays.Where(x => x.Fatura.AyId == AyId && x.MalHizmetId == MalHizId).Count();
                    //if (BirimFiyat != 0) { AyOrtBirimFiyat = BirimFiyat / adet; } else { AyOrtBirimFiyat = 0; }
                    //var AyAd = c.Ays.Where(x => x.AyId == AyId).Select(y => y.AyAd).FirstOrDefault();

                    ortBirimFiyat.Add(new BirimFiyatAnaliz
                    {
                        FaturaTarihi = FaturaTarihi,
                        BirimFiyat = BirimFiyat,
                        YillikOrtalamaBirimFiyat = Math.Round(topYilOrtBirimFiyat, 2)
                    });
                }
            }
            return ortBirimFiyat;
        }
    }
}