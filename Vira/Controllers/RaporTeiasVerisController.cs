using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vira.Models;
using Vira.Models.Siniflar;

namespace Vira.Controllers
{
    public class RaporTeiasVerisController : Controller
    {
        // GET: RaporTeiasCekis
        Context c = new Context();
        public ActionResult Index()
        {
            var liste = c.FaturaDetays.Where(x => x.Fatura.KurumId == 2 & x.Fatura.FaturaTuru == "Veriş")
                .GroupBy(y => new { y.Fatura.FaturaTarihi, y.Fatura.Yillar.Yil, y.Fatura.Ay.AyAd, y.Fatura.AyId })
                .Select(g => new TeiasVeris
                {
                    Yil = g.Key.Yil,
                    AyId = g.Key.AyId,
                    Donem = g.Key.Yil + "/" + g.Key.AyAd,
                    FaturaTarihi = g.Key.FaturaTarihi,
                    UretimMiktari = g.FirstOrDefault(e => e.MalHizmetId == 9).FdMiktar,
                    SistemKullanimBedeliS = g.FirstOrDefault(e => e.MalHizmetId == 8).FdTutar,
                    SistemKullanimBedeliD = g.FirstOrDefault(e => e.MalHizmetId == 9).FdTutar,
                    SistemIsletimBedeli = g.FirstOrDefault(e => e.MalHizmetId == 7).FdTutar,
                    IletimEkUcreti = g.FirstOrDefault(e => e.MalHizmetId == 10).FdTutar,
                    Toplam = g.FirstOrDefault().Fatura.FaturaToplami
                }).OrderByDescending(z => new { z.Yil, z.AyId }).ToList();

            return View(liste);
        }
    }
}