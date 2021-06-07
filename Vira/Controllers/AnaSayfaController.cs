using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Vira.Models;
using Vira.Models.Siniflar;

namespace Vira.Content
{
    public class AnaSayfaController : Controller
    {
        // GET: AnaSayfa
        Context c = new Context();
        [HttpGet]
        public ActionResult Index()
        {
            var yil = DateTime.Now.Year;
            var liste = c.FaturaDetays.Where(x => x.MalHizmetId == 4 && x.Fatura.Yillar.Yil == yil.ToString()).OrderBy(y=>y.Fatura.Ay.AyId).ToList();

            return View(liste);
        }
        public ActionResult VisualizeMiktarResult()
        {
            return Json(MiktarListesi(), JsonRequestBehavior.AllowGet);
        }
        public List<Miktar> MiktarListesi()
        {
            List<Miktar> mkt = new List<Miktar>();
            using (var c = new Context())
            {
                var yil = DateTime.Now.Year;
                mkt = c.FaturaDetays.Where(x => x.MalHizmetId == 4 && x.Fatura.Yillar.Yil == yil.ToString()).OrderBy(y => y.Fatura.Ay.AyId).Select(x => new Miktar
                {
                    Aylar = x.Fatura.Ay.AyAd,
                    Adet = x.FdMiktar
                }).ToList();
            }
            return mkt;
        }
    }
}