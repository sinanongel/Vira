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

        public ActionResult ParametreListe(string Detay, FaturaRapor p)
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

            List<Fatura> kurumList = c.Faturas.GroupBy(x => x.KurumId).Select(y => y.FirstOrDefault()).ToList();

            List<SelectListItem> kurum = (from t in kurumList
                                          orderby t.Kurum.KurumUnvani ascending
                                          select new SelectListItem
                                          {
                                              Text = t.Kurum.KurumUnvani,
                                              Value = t.KurumId.ToString()
                                          }).ToList();            
            ViewBag.kListe = kurum; 
            
            var fatTipi = c.FaturaDetays.Where(x => x.Fatura.KurumId == p.KurumId).Select(y => y.Fatura.FaturaTipi).FirstOrDefault();
            var malHizmetId = c.FaturaDetays.Where(x => x.Fatura.KurumId == p.KurumId).Select(y => y.MalHizmetId).FirstOrDefault();
            var malHizmetGrup = c.MalHizmets.Where(x => x.MalHizmetId == malHizmetId).Select(y => y.MalHizmetGrupId).FirstOrDefault();

            List<MalHizmet> malHizmetList = c.MalHizmets.Where(i => i.MalHizmetTuru == fatTipi).OrderBy(i => i.MalHizmetAdi).ToList();

            List<SelectListItem> malHizmetListe = (from i in malHizmetList
                                                   where i.MalHizmetGrupId == Convert.ToInt32(malHizmetGrup)
                                                   select new SelectListItem
                                                   {
                                                       Text = i.MalHizmetAdi,
                                                       Value = i.MalHizmetId.ToString()
                                                   }).ToList();
            
            ViewBag.mhzListe = malHizmetListe;
            
            if(Detay is null)
            {
                Detay = "2";
            }
            ViewBag.raporTur = Detay;

            return View();
        }

        public PartialViewResult RaporListeDetayli(FaturaRapor p)
        {
            //if (p.BasYil != null && p.BasAy != null && p.BitYil != null && p.BitAy != null && p.KurumId != null && p.MalHizmet != null)
            //{
            //    var baYil = c.Yillars.Where(x => x.YillarId == p.BasYil).Select(y => y.Yil).FirstOrDefault();
            //    var biYil = c.Yillars.Where(x => x.YillarId == p.BitYil).Select(y => y.Yil).FirstOrDefault();
            //    basTarih = new DateTime(Convert.ToInt32(baYil), Convert.ToInt32(p.BasAy), 1);
            //    bitTarih = new DateTime(Convert.ToInt32(biYil), Convert.ToInt32(p.BitAy), 1).AddMonths(1);

            //    var baAy = c.Ays.Where(x => x.AyId == p.BasAy).Select(y => y.AyAd).FirstOrDefault();
            //    var biAy = c.Ays.Where(x => x.AyId == p.BitAy).Select(y => y.AyAd).FirstOrDefault();
            //    ViewBag.Donem = baYil + "/" + baAy + " - " + biYil + "/" + biAy;

            //    var kurumAdi = c.Kurums.Where(x => x.KurumId == p.KurumId).Select(y => y.KurumAdi).FirstOrDefault();
            //    ViewBag.KurumAdi = kurumAdi;

            //    var malHizmet = c.MalHizmets.Where(x => x.MalHizmetId == p.MalHizmet).Select(y => y.MalHizmetAdi).FirstOrDefault();
            //    ViewBag.MalHizmetAdi = malHizmet;
            //}

            if (p.BasTarih != null && p.BitTarih != null && p.KurumId != null && p.MalHizmet != null)
            {
                var kurumAdi = c.Kurums.Where(x => x.KurumId == p.KurumId).Select(y => y.KurumAdi).FirstOrDefault();
                var malHizmet = c.MalHizmets.Where(x => x.MalHizmetId == p.MalHizmet).Select(y => y.MalHizmetAdi).FirstOrDefault();

                ViewBag.KurumAdi = kurumAdi;
                ViewBag.MalHizmetAdi = malHizmet;
                ViewBag.BasTarih = p.BasTarih.ToString("d");
                ViewBag.BitTarih = p.BitTarih.ToString("d");
                ViewBag.Donem = p.BasTarih + " - " + p.BitTarih;
            }

            var liste = c.FaturaDetays
                .Where(x => x.Fatura.FaturaTarihi >= p.BasTarih && x.Fatura.FaturaTarihi <= p.BitTarih &&  x.MalHizmetId == p.MalHizmet && x.Fatura.Kurum.KurumId == p.KurumId)
                .ToList();

            //var liste = c.faturadetays
            //    .where(x => x.malhizmetıd == p.malhizmet && x.fatura.kurum.kurumıd == p.kurumıd && x.fatura.faturatarihi >= bastarih && x.fatura.faturatarihi < bittarih)
            //    .orderby(y => new { y.fatura.yillar.yillarıd, y.fatura.ay.ayıd })
            //    .tolist();

            var count = liste.Count();
            ViewBag.Count = count;

            return PartialView("RaporListeDetayli", liste);
        }

        public PartialViewResult RaporListeDetaysiz(FaturaRapor p)
        {
            //if (p.BasYil != null && p.BasAy != null && p.BitYil != null && p.BitAy != null && p.KurumId != null)
            //{
            //    var baYil = c.Yillars.Where(x => x.YillarId == p.BasYil).Select(y => y.Yil).FirstOrDefault();
            //    var biYil = c.Yillars.Where(x => x.YillarId == p.BitYil).Select(y => y.Yil).FirstOrDefault();
            //    basTarih = new DateTime(Convert.ToInt32(baYil), Convert.ToInt32(p.BasAy), 1);
            //    bitTarih = new DateTime(Convert.ToInt32(biYil), Convert.ToInt32(p.BitAy), 1).AddMonths(1);

            //    var baAy = c.Ays.Where(x => x.AyId == p.BasAy).Select(y => y.AyAd).FirstOrDefault();
            //    var biAy = c.Ays.Where(x => x.AyId == p.BitAy).Select(y => y.AyAd).FirstOrDefault();
            //    ViewBag.Donem = baYil + "/" + baAy + " - " + biYil + "/" + biAy;

            //    var kurumAdi = c.Kurums.Where(x => x.KurumId == p.KurumId).Select(y => y.KurumAdi).FirstOrDefault();
            //    ViewBag.KurumAdi = kurumAdi;
            //}
            if (p.BasTarih != null && p.BitTarih != null && p.KurumId != null && p.MalHizmet != null)
            {
                var kurumAdi = c.Kurums.Where(x => x.KurumId == p.KurumId).Select(y => y.KurumAdi).FirstOrDefault();
                var malHizmet = c.MalHizmets.Where(x => x.MalHizmetId == p.MalHizmet).Select(y => y.MalHizmetAdi).FirstOrDefault();

                ViewBag.KurumAdi = kurumAdi;
                ViewBag.MalHizmetAdi = malHizmet;
                ViewBag.BasTarih = p.BasTarih.ToString("d");
                ViewBag.BitTarih = p.BitTarih.ToString("d");
                ViewBag.Donem = p.BasTarih + " - " + p.BitTarih;
            }

            //var liste = c.Faturas
            //    .Where(x => x.KurumId == p.KurumId && x.FaturaTarihi >= basTarih && x.FaturaTarihi < bitTarih)
            //    .OrderBy(y => new { y.YillarId, y.AyId })
            //    .ToList();

            var liste = c.Faturas
                .Where(x => x.KurumId == p.KurumId && x.FaturaTarihi >= p.BasTarih && x.FaturaTarihi < p.BitTarih)
                .OrderBy(y => new { y.YillarId, y.AyId })
                .ToList();

            return PartialView("RaporListeDetaysiz", liste);
        }

        [HttpPost]
        public JsonResult MalHizmetGetir(int KurumId)
        {
            var fatTipi = c.FaturaDetays.Where(x => x.Fatura.KurumId == KurumId).Select(y => y.Fatura.FaturaTipi).FirstOrDefault();
            var malHizmetId = c.FaturaDetays.Where(x => x.Fatura.KurumId == KurumId).Select(y => y.MalHizmetId).FirstOrDefault();
            var malHizmetGrup = c.MalHizmets.Where(x => x.MalHizmetId == malHizmetId).Select(y => y.MalHizmetGrupId).FirstOrDefault();

            List<MalHizmet> malHizmetList = c.MalHizmets.Where(i => i.MalHizmetTuru == fatTipi).OrderBy(i => i.MalHizmetAdi).ToList();

            List<SelectListItem> malHizmetListe = (from i in malHizmetList
                                                   where i.MalHizmetGrupId == Convert.ToInt32(malHizmetGrup)
                                                   select new SelectListItem
                                                   {
                                                       Text = i.MalHizmetAdi,
                                                       Value = i.MalHizmetId.ToString()
                                                   }).ToList();

            return Json(malHizmetListe, JsonRequestBehavior.AllowGet);
        }
    }
}