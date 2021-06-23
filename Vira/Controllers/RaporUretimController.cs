using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vira.Models;

namespace Vira.Controllers
{
    public class RaporUretimController : Controller
    {
        // GET: RaporUretim
        Context c = new Context();
        public ActionResult Index()
        {
            List<SelectListItem> donemYil = (from t in c.Yillars.ToList()
                                             orderby t.YillarId descending
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

        public DateTime basTarih;
        public DateTime bitTarih;
        public PartialViewResult RaporListe(int? BasYil, int? BasAy, int? BitYil, int? BitAy)
        {
            
            if (BasYil != null && BasAy != null && BitYil != null && BitAy != null)
            {
                var baYil = c.Yillars.Where(x => x.YillarId == BasYil).Select(y => y.Yil).FirstOrDefault();
                var biYil = c.Yillars.Where(x => x.YillarId == BitYil).Select(y => y.Yil).FirstOrDefault();
                basTarih = new DateTime(Convert.ToInt32(baYil), Convert.ToInt32(BasAy), 1);
                bitTarih = new DateTime(Convert.ToInt32(biYil), Convert.ToInt32(BitAy), 1).AddMonths(1);
            }

            var liste = c.FaturaDetays
                .Where(x => x.MalHizmetId == 9 && x.Fatura.FaturaTuru == "Veriş" && x.Fatura.Kurum.KurumId == 2 && x.Fatura.FaturaTarihi >= basTarih && x.Fatura.FaturaTarihi < bitTarih)
                .OrderBy(y => new { y.Fatura.Yillar.YillarId, y.Fatura.Ay.AyId })
                .ToList();

            return PartialView("RaporListe", liste);
        }
    }
}