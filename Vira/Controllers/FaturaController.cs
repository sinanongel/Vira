using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vira.Models;

namespace Vira.Controllers
{
    public class FaturaController : Controller
    {
        // GET: Fatura
        Context c = new Context();
        public ActionResult Index()
        {
            var liste = c.Faturas.ToList();
            return View(liste);
        }
        [HttpPost]
        public ActionResult ModalAc()
        {
            List<SelectListItem> kurumListe = (from t in c.Kurums.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = t.KurumAdi + " - " + t.KurumUnvani,
                                                   Value = t.KurumId.ToString()
                                               }).ToList();

            List<SelectListItem> tipListe = new List<SelectListItem>();
            tipListe.Add(new SelectListItem() { Text = "Alış" });
            tipListe.Add(new SelectListItem() { Text = "Satış" });

            List<SelectListItem> turListe = new List<SelectListItem>();
            turListe.Add(new SelectListItem() { Text = "Çekiş" });
            turListe.Add(new SelectListItem() { Text = "Veriş" });
            turListe.Add(new SelectListItem() { Text = "Ceza" });

            ViewBag.kList = kurumListe;
            ViewBag.tiListe = tipListe;
            ViewBag.tuListe = turListe;

            return PartialView("FaturaEkle");
        }
        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult FaturaEkle(Fatura p)
        {
            c.Faturas.Add(p);
            c.SaveChanges();

            var faturaId = c.Faturas.Max(x => x.FaturaId);
            return RedirectToAction("FaturaDetayIndex", new { id = faturaId });
        }
        public ActionResult FaturaGetir(int id)
        {
            var fatGet = c.Faturas.Find(id);

            List<SelectListItem> kurumListe = (from t in c.Kurums.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = t.KurumAdi + " - " + t.KurumUnvani,
                                                   Value = t.KurumId.ToString()
                                               }).ToList();

            List<SelectListItem> tipListe = new List<SelectListItem>();
            tipListe.Add(new SelectListItem() { Text = "Alış" });
            tipListe.Add(new SelectListItem() { Text = "Satış" });

            List<SelectListItem> turListe = new List<SelectListItem>();
            turListe.Add(new SelectListItem() { Text = "Çekiş" });
            turListe.Add(new SelectListItem() { Text = "Veriş" });
            turListe.Add(new SelectListItem() { Text = "Ceza" });

            ViewBag.kList = kurumListe;
            ViewBag.tiListe = tipListe;
            ViewBag.tuListe = turListe;

            return PartialView("FaturaGetir", fatGet);
        }
        public ActionResult FaturaGuncelle(Fatura p)
        {
            var fat = c.Faturas.Find(p.FaturaId);
            fat.FaturaNo = p.FaturaNo;
            fat.FaturaTipi = p.FaturaTipi;
            fat.FaturaTuru = p.FaturaTuru;
            fat.FaturaTarihi = p.FaturaTarihi;
            fat.KurumId = p.KurumId;
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult FaturaDetayIndex(int id)
        {
            var liste = c.FaturaDetays.Where(x => x.FaturaId == id).ToList();
            var fatSira = c.Faturas.Where(x => x.FaturaId == id).Select(y => y.FaturaNo).FirstOrDefault();
            var fatTipi = c.Faturas.Where(x => x.FaturaId == id).Select(y => y.FaturaTipi).FirstOrDefault();
            var kurAd = c.Faturas.Where(x => x.FaturaId == id).Select(y => y.Kurum.KurumAdi).FirstOrDefault();
            var kurUnvan = c.Faturas.Where(x => x.FaturaId == id).Select(y => y.Kurum.KurumUnvani).FirstOrDefault();
            var fatTarihi = c.Faturas.Where(x => x.FaturaId == id).Select(y => y.FaturaTarihi).FirstOrDefault();
            var fatToplam = c.Faturas.Where(x => x.FaturaId == id).Select(y => y.FaturaToplami).FirstOrDefault();
            var fatKdvToplam = c.Faturas.Where(x => x.FaturaId == id).Select(y => y.FaturaKdvToplami).FirstOrDefault();
            var fatOdenecekTut = c.Faturas.Where(x => x.FaturaId == id).Select(y => y.FaturaOdenecekTutar).FirstOrDefault();

            ViewBag.FSira = fatSira;
            ViewBag.FTipi = fatTipi;
            ViewBag.KurumAd = kurAd;
            ViewBag.KurumUnvan = kurUnvan;
            ViewBag.FaturaTarihi = fatTarihi.ToString("d");
            ViewBag.FaturaId = id;
            ViewBag.FaturaToplam = fatToplam;
            ViewBag.FaturaKdvToplam = fatKdvToplam;
            ViewBag.FaturaOdenecektutar = fatOdenecekTut;

            return View(liste);
        }
        [HttpPost]
        public ActionResult ModalFaturaDetayAc(int id)
        {
            List<SelectListItem> biListe = (from b in c.Birims.OrderBy(b => b.BirimId).ToList()
                                            select new SelectListItem
                                            {
                                                Text = b.BirimAdi,
                                                Value = b.BirimId.ToString()
                                            }).ToList();

            List<SelectListItem> kdvListe = (from k in c.Kdvs.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = k.KdvOrani,
                                                 Value = k.KdvId.ToString()
                                             }).ToList();

            List<SelectListItem> dovizListe = (from d in c.Dovizs.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = d.DovizCinsi,
                                                   Value = d.DovizId.ToString()
                                               }).ToList();

            List<SelectListItem> mhListe = (from m in c.MalHizmets
                                            select new SelectListItem
                                            {
                                                Text = m.MalHizmetAdi,
                                                Value = m.MalHizmetId.ToString()
                                            }).ToList();

            ViewBag.bList = biListe;
            ViewBag.kList = kdvListe;
            ViewBag.dList = dovizListe;
            ViewBag.mhList = mhListe;
            ViewBag.faturaId = id;

            return PartialView("FaturaDetayEkle");
        }
        [HttpGet]
        public ActionResult FaturaDetayEkle()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult FaturaDetayEkle(FaturaDetay p)
        {
            var KdvOran = c.Kdvs.Where(x => x.KdvId == p.KdvId).Select(y => y.KdvOrani).FirstOrDefault();
            var FdBirimFiyatTl = Math.Round((p.FdBirimFiyat * p.FdKur), 8);
            var FdTutar = Math.Round((FdBirimFiyatTl * p.FdMiktar), 2);
            var KdvTutar = Math.Round((FdTutar * Convert.ToInt32(KdvOran) / 100), 2);

            var faturaId = p.FaturaId;
            p.FdBirimFiyatTl = FdBirimFiyatTl;
            p.KdvTutar = KdvTutar;
            p.FdTutar = FdTutar;
            c.FaturaDetays.Add(p);
            c.SaveChanges();

            var FdTutarToplam = c.FaturaDetays.Where(x => x.FaturaId == faturaId).Sum(x => x.FdTutar);
            var FdKdvTutarToplam = c.FaturaDetays.Where(x => x.FaturaId == faturaId).Sum(x => x.KdvTutar);
            var FatToplam = FdTutarToplam + FdKdvTutarToplam;

            var f = c.Faturas.Find(faturaId);
            f.FaturaToplami = FdTutarToplam;
            f.FaturaKdvToplami = FdKdvTutarToplam;
            f.FaturaOdenecekTutar = FatToplam;
            c.SaveChanges();
            return RedirectToAction("FaturaDetayIndex", new { id = faturaId });
        }
        public ActionResult FaturaDetayGetir(int? id)
        {
            List<SelectListItem> mhizListe = (from mh in c.MalHizmets.OrderBy(m => m.MalHizmetAdi).ToList()
                                              select new SelectListItem
                                              {
                                                  Text = mh.MalHizmetAdi,
                                                  Value = mh.MalHizmetId.ToString()
                                              }).ToList();

            List<SelectListItem> biListe = (from b in c.Birims.OrderBy(b => b.BirimId).ToList()
                                            select new SelectListItem
                                            {
                                                Text = b.BirimAdi,
                                                Value = b.BirimId.ToString()
                                            }).ToList();

            List<SelectListItem> kdvListe = (from k in c.Kdvs.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = k.KdvOrani,
                                                 Value = k.KdvId.ToString()
                                             }).ToList();

            List<SelectListItem> dovizListe = (from d in c.Dovizs.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = d.DovizCinsi,
                                                   Value = d.DovizId.ToString()
                                               }).ToList();

            ViewBag.mhList = mhizListe;
            ViewBag.bList = biListe;
            ViewBag.kList = kdvListe;
            ViewBag.dList = dovizListe;

            var faturaId = c.FaturaDetays.Where(f => f.FaturaDetayId == id).Select(x => x.FaturaId).FirstOrDefault();
            ViewBag.faturaId = faturaId;

            var fdGet = c.FaturaDetays.Find(id);

            return PartialView("FaturaDetayGetir", fdGet);
        }
        public ActionResult FaturaDetayGuncelle(FaturaDetay p)
        {
            var KdvOran = c.Kdvs.Where(x => x.KdvId == p.KdvId).Select(y => y.KdvOrani).FirstOrDefault();
            var FdBirimFiyatTl = Math.Round((p.FdBirimFiyat * p.FdKur), 8);
            var FdTutar = Math.Round((FdBirimFiyatTl * p.FdMiktar), 2);
            var KdvTutar = Math.Round((FdTutar * Convert.ToInt32(KdvOran) / 100), 2);

            var fd = c.FaturaDetays.Find(p.FaturaDetayId);
            fd.MalHizmetId = p.MalHizmetId;
            fd.FdMiktar = p.FdMiktar;
            fd.BirimId = p.BirimId;
            fd.FdBirimFiyat = p.FdBirimFiyat;
            fd.DovizId = p.DovizId;
            fd.FdKur = p.FdKur;
            fd.FdBirimFiyatTl = FdBirimFiyatTl;
            fd.KdvId = p.KdvId;
            fd.KdvTutar = KdvTutar;
            fd.FdTutar = FdTutar;
            c.SaveChanges();

            var FdTutarToplam = c.FaturaDetays.Where(x => x.FaturaId == fd.FaturaId).Sum(x => x.FdTutar);
            var FdKdvTutarToplam = c.FaturaDetays.Where(x => x.FaturaId == fd.FaturaId).Sum(x => x.KdvTutar);
            var FatToplam = FdTutarToplam + FdKdvTutarToplam;

            var f = c.Faturas.Find(fd.FaturaId);
            f.FaturaToplami = FdTutarToplam;
            f.FaturaKdvToplami = FdKdvTutarToplam;
            f.FaturaOdenecekTutar = FatToplam;
            c.SaveChanges();

            var faturaId = p.FaturaId;

            return RedirectToAction("FaturaDetayIndex", new { id = faturaId });
        }
        [HttpPost]
        public ActionResult ModalDosya(int id)
        {
            var FatId = c.Faturas.Where(i => i.FaturaId == id).Select(f => f.FaturaId).FirstOrDefault();

            List<SelectListItem> dtListe = (from d in c.DosyaTurus.OrderBy(d => d.DosyaTuruId).ToList()
                                            select new SelectListItem
                                            {
                                                Text = d.DosyaTuruAd,
                                                Value = d.DosyaTuruId.ToString()
                                            }).ToList();

            ViewBag.dListe = dtListe;
            ViewBag.fId = FatId;

            return PartialView("DosyaYukle");
        }
        [HttpGet]
        public ActionResult DosyaYukle()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult DosyaYukle(Dosya p)
        {
            var KurumAd = c.Faturas.Where(x => x.FaturaId == p.FaturaId).Select(y => y.Kurum.KurumAdi).FirstOrDefault();
            var FaturaTuru = c.Faturas.Where(x => x.FaturaId == p.FaturaId).Select(y => y.FaturaTuru).FirstOrDefault();
            DateTime FaturaTarihi = c.Faturas.Where(x => x.FaturaId == p.FaturaId).Select(y => y.FaturaTarihi).FirstOrDefault();
            string dosyaAdi;

            var ay = FaturaTarihi.ToString("MM");
            var yil = FaturaTarihi.ToString("yyyy");
;
            if (Request.Files.Count > 0)
            {
                //string dosyaAdi = Guid.NewGuid().ToString("D");
                //string dosyaAdi = dosyaAd.Replace('-', '_');
                //string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                if(FaturaTuru is null)
                {
                    dosyaAdi = KurumAd + '_' + yil + '_' + ay;
                }
                else
                {
                    dosyaAdi = KurumAd + '_' + FaturaTuru.ToUpper() + '_' + yil + '_' + ay;
                }
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/App_Data/Dosya/" + dosyaAdi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                p.DosyaYolu = "/App_Data/Dosya/";
                p.DosyaAdi = dosyaAdi + uzanti;
            }
            c.Dosyas.Add(p);
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult DosyaListe(int id)
        {
            List<Dosya> dosyaList = c.Dosyas.Where(i => i.FaturaId == id).OrderBy(i => i.DosyaYolu).ToList();

            List<SelectListItem> dListe = (from i in dosyaList
                                           select new SelectListItem
                                           {
                                               Text = i.DosyaYolu,
                                               Value = i.DosyaId.ToString()
                                           }).ToList();

            return Json(dListe, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ModalDosyaListe(int id)
        {
            var liste = c.Dosyas.OrderBy(f => f.DosyaId).Where(x => x.FaturaId == id).ToList();

            return PartialView("DosyaListele", liste);
        }
        public ActionResult DosyaListele()
        {
            return PartialView("DosyaListele");
        }
        public FileResult DosyaAc(string dosya)
        {
            Response.AppendHeader("Content-Disposition", "inline; filename" + dosya + ";");

            string yol = AppDomain.CurrentDomain.BaseDirectory + "App_Data/Dosya/";
            return File(yol + dosya, System.Net.Mime.MediaTypeNames.Application.Pdf, dosya);
        }
    }
}