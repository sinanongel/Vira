using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vira.Models;
using Vira.Models.Siniflar;

namespace Vira.Controllers
{
    public class RaporFaturaController : Controller
    {
        // GET: RaporFatura
        Context c = new Context();
        public ActionResult Index()
        {
            var liste = c.FaturaDetays.ToList();
            return View(liste);
        }

        public DateTime basTarih;
        public DateTime bitTarih;

        public ActionResult ParametreListe(string Detay)
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

            List<SelectListItem> kurum = (from t in c.Kurums.ToList()
                                          orderby t.KurumId descending
                                          select new SelectListItem
                                          {
                                              Text = t.KurumAdi,
                                              Value = t.KurumId.ToString()
                                          }).ToList();
            ViewBag.kListe = kurum;

            List<SelectListItem> malHizmetL = (from t in c.MalHizmets.ToList()
                                               orderby t.MalHizmetId descending
                                               select new SelectListItem
                                               {
                                                   Text = t.MalHizmetAdi,
                                                   Value = t.MalHizmetId.ToString()
                                               }).ToList();
            ViewBag.mhzListe = malHizmetL;
            ViewBag.raporTur = Detay;

            return View();
        }

        public PartialViewResult RaporListeDetayli(FaturaRapor p)
        {
            if (p.BasYil != null && p.BasAy != null && p.BitYil != null && p.BitAy != null && p.KurumId != null && p.MalHizmet != null)
            {
                var baYil = c.Yillars.Where(x => x.YillarId == p.BasYil).Select(y => y.Yil).FirstOrDefault();
                var biYil = c.Yillars.Where(x => x.YillarId == p.BitYil).Select(y => y.Yil).FirstOrDefault();
                basTarih = new DateTime(Convert.ToInt32(baYil), Convert.ToInt32(p.BasAy), 1);
                bitTarih = new DateTime(Convert.ToInt32(biYil), Convert.ToInt32(p.BitAy), 1).AddMonths(1);

                var baAy = c.Ays.Where(x => x.AyId == p.BasAy).Select(y => y.AyAd).FirstOrDefault();
                var biAy = c.Ays.Where(x => x.AyId == p.BitAy).Select(y => y.AyAd).FirstOrDefault();
                ViewBag.Donem = baYil + "/" + baAy + " - " + biYil + "/" + biAy;

                var kurumAdi = c.Kurums.Where(x => x.KurumId == p.KurumId).Select(y => y.KurumAdi).FirstOrDefault();
                ViewBag.KurumAdi = kurumAdi;

                var malHizmet = c.MalHizmets.Where(x => x.MalHizmetId == p.MalHizmet).Select(y => y.MalHizmetAdi).FirstOrDefault();
                ViewBag.MalHizmetAdi = malHizmet;
            }

            var liste = c.FaturaDetays
                .Where(x => x.MalHizmetId == p.MalHizmet && x.Fatura.Kurum.KurumId == p.KurumId && x.Fatura.FaturaTarihi >= basTarih && x.Fatura.FaturaTarihi < bitTarih)
                .OrderBy(y => new { y.Fatura.Yillar.YillarId, y.Fatura.Ay.AyId })
                .ToList();

            return PartialView("RaporListeDetayli", liste);
        }

        public PartialViewResult RaporListeDetaysiz(FaturaRapor p)
        {
            if (p.BasYil != null && p.BasAy != null && p.BitYil != null && p.BitAy != null && p.KurumId != null)
            {
                var baYil = c.Yillars.Where(x => x.YillarId == p.BasYil).Select(y => y.Yil).FirstOrDefault();
                var biYil = c.Yillars.Where(x => x.YillarId == p.BitYil).Select(y => y.Yil).FirstOrDefault();
                basTarih = new DateTime(Convert.ToInt32(baYil), Convert.ToInt32(p.BasAy), 1);
                bitTarih = new DateTime(Convert.ToInt32(biYil), Convert.ToInt32(p.BitAy), 1).AddMonths(1);

                var baAy = c.Ays.Where(x => x.AyId == p.BasAy).Select(y => y.AyAd).FirstOrDefault();
                var biAy = c.Ays.Where(x => x.AyId == p.BitAy).Select(y => y.AyAd).FirstOrDefault();
                ViewBag.Donem = baYil + "/" + baAy + " - " + biYil + "/" + biAy;

                var kurumAdi = c.Kurums.Where(x => x.KurumId == p.KurumId).Select(y => y.KurumAdi).FirstOrDefault();
                ViewBag.KurumAdi = kurumAdi;
            }

            var liste = c.Faturas
                .Where(x => x.KurumId == p.KurumId && x.FaturaTarihi >= basTarih && x.FaturaTarihi < bitTarih)
                .OrderBy(y => new { y.YillarId, y.AyId })
                .ToList();

            return PartialView("RaporListeDetaysiz", liste);
        }

        [HttpPost]
        public JsonResult MalHizmetGetir(int KurumId)
        {
            var fatTipi = c.FaturaDetays.Where(x => x.Fatura.KurumId == KurumId).Select(y => y.Fatura.FaturaTipi).FirstOrDefault();
            List<MalHizmet> malHizmetList = c.MalHizmets.Where(i => i.MalHizmetTuru == fatTipi).OrderBy(i => i.MalHizmetAdi).ToList();

            List<SelectListItem> malHizmetListe = (from i in malHizmetList
                                              select new SelectListItem
                                              {
                                                  Text = i.MalHizmetAdi,
                                                  Value = i.MalHizmetId.ToString()
                                              }).ToList();

            return Json(malHizmetListe, JsonRequestBehavior.AllowGet);
        }
    }
}