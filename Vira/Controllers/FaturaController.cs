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

            List<SelectListItem> donemYil = (from t in c.Yillars.ToList()
                                             orderby t.Yil descending
                                             select new SelectListItem
                                             {
                                                 Text = t.Yil,
                                                 Value = t.YillarId.ToString()
                                             }).ToList();

            List<SelectListItem> donemAy = (from t in c.Ays.ToList()
                                            select new SelectListItem
                                            {
                                                Text = t.AyAd,
                                                Value = t.AyId.ToString()
                                            }).ToList();

            List<SelectListItem> tipListe = new List<SelectListItem>();
            tipListe.Add(new SelectListItem() { Text = "Alış" });
            tipListe.Add(new SelectListItem() { Text = "Satış" });
            tipListe.Add(new SelectListItem() { Text = "İade" });

            List<SelectListItem> turListe = new List<SelectListItem>();
            turListe.Add(new SelectListItem() { Text = "Çekiş" });
            turListe.Add(new SelectListItem() { Text = "Veriş" });
            turListe.Add(new SelectListItem() { Text = "Ceza" });

            ViewBag.kList = kurumListe;
            ViewBag.tiListe = tipListe;
            ViewBag.tuListe = turListe;
            ViewBag.dYil = donemYil;
            ViewBag.dAy = donemAy;

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
            var ay = p.FaturaTarihi.ToString("MM");
            var yil = p.FaturaTarihi.ToString("yyyy");
            var KurumAd = c.Kurums.Where(x => x.KurumId == p.KurumId).Select(y => y.KurumAdi).FirstOrDefault();

            if (Request.Files.Count > 0)
            {
                string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                if (dosyaAdi != "")
                {
                    if (p.FaturaTuru is null)
                    {
                        dosyaAdi = KurumAd + '_' + yil + '_' + ay;
                    }
                    else
                    {
                        dosyaAdi = KurumAd + '_' + p.FaturaTuru.ToUpper() + '_' + yil + '_' + ay;
                    }
                    string uzanti = Path.GetExtension(Request.Files[0].FileName);
                    //Request.Files[0].SaveAs(Server.MapPath(yol));
                    string yol = "D://Dosya/Faturalar/" + dosyaAdi + uzanti;
                    Request.Files[0].SaveAs(yol);
                    p.Dosya = dosyaAdi + uzanti;
                }
            }
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

            List<SelectListItem> donemYil = (from t in c.Yillars.ToList()
                                             orderby t.YillarId descending
                                             select new SelectListItem
                                             {
                                                 Text = t.Yil,
                                                 Value = t.YillarId.ToString()
                                             }).ToList();

            List<SelectListItem> donemAy = (from t in c.Ays.ToList()
                                            select new SelectListItem
                                            {
                                                Text = t.AyAd,
                                                Value = t.AyId.ToString()
                                            }).ToList();

            List<SelectListItem> tipListe = new List<SelectListItem>();
            tipListe.Add(new SelectListItem() { Text = "Alış" });
            tipListe.Add(new SelectListItem() { Text = "Satış" });
            tipListe.Add(new SelectListItem() { Text = "İade" });

            List<SelectListItem> turListe = new List<SelectListItem>();
            turListe.Add(new SelectListItem() { Text = "Çekiş" });
            turListe.Add(new SelectListItem() { Text = "Veriş" });
            turListe.Add(new SelectListItem() { Text = "Ceza" });

            var dosyaAdi = c.Faturas.Where(x => x.FaturaId == id).Select(y => y.Dosya).FirstOrDefault();

            ViewBag.kList = kurumListe;
            ViewBag.tiListe = tipListe;
            ViewBag.tuListe = turListe;
            ViewBag.dYil = donemYil;
            ViewBag.dAy = donemAy;
            ViewBag.dosya = dosyaAdi;

            return PartialView("FaturaGetir", fatGet);
        }
        public ActionResult FaturaGuncelle(Fatura p)
        {
            var fatura = c.Faturas.Find(p.FaturaId);

            var ay = p.FaturaTarihi.ToString("MM");
            var yil = p.FaturaTarihi.ToString("yyyy");

            if (Request.Files.Count > 0)
            {
                var KurumAd = c.Faturas.Where(x => x.FaturaId == p.FaturaId).Select(y => y.Kurum.KurumAdi).FirstOrDefault();

                if (!ModelState.IsValid)
                {
                    return View("FaturaGuncelle", p);
                }
                else if (fatura.Dosya != null && p.Dosya != null && fatura.Dosya != p.Dosya) // dosya varsa ve yeni dosya ile değiştirilecekse
                {
                    var dosyaSayi = c.Sozlesmes.Where(x => x.Dosya == fatura.Dosya).Count();
                    string dosyaAdi = "";

                    if (dosyaSayi == 1)
                    {
                        System.IO.File.Delete("D://Dosya/Faturalar/" + fatura.Dosya); //önceki siliniyor
                    }
                    if (p.FaturaTuru is null)
                    {
                        dosyaAdi = KurumAd + '_' + yil + '_' + ay;
                    }
                    else
                    {
                        dosyaAdi = KurumAd + '_' + p.FaturaTuru.ToUpper() + '_' + yil + '_' + ay;
                    }
                    string uzanti = Path.GetExtension(Request.Files[0].FileName);
                    string yol = "D://Dosya/Faturalar/" + dosyaAdi + uzanti;
                    Request.Files[0].SaveAs(yol);
                    p.Dosya = dosyaAdi + uzanti;
                    fatura.Dosya = p.Dosya;
                }
                else if (fatura.Dosya == null && p.Dosya != null) // var olan kayıta ilk dosya ekleme
                {
                    string dosyaAdi = "";
                    if (p.FaturaTuru is null)
                    {
                        dosyaAdi = KurumAd + '_' + yil + '_' + ay;
                    }
                    else
                    {
                        dosyaAdi = KurumAd + '_' + p.FaturaTuru.ToUpper() + '_' + yil + '_' + ay;
                    }
                    string uzanti = Path.GetExtension(Request.Files[0].FileName);
                    string yol = "D://Dosya/Faturalar/" + dosyaAdi + uzanti;
                    Request.Files[0].SaveAs(yol);
                    p.Dosya = dosyaAdi + uzanti;
                    fatura.Dosya = p.Dosya;
                }
            }
            fatura.FaturaNo = p.FaturaNo;
            fatura.FaturaTipi = p.FaturaTipi;
            fatura.FaturaTuru = p.FaturaTuru;
            fatura.FaturaTarihi = p.FaturaTarihi;
            fatura.KurumId = p.KurumId;
            fatura.YillarId = p.YillarId;
            fatura.AyId = p.AyId;
            fatura.Dosya = p.Dosya;
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
            var fatTipi = c.Faturas.Where(x => x.FaturaId == id).Select(y => y.FaturaTipi).FirstOrDefault();

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

            List<SelectListItem> mhgListe = (from m in c.MalHizmetGrups
                                             select new SelectListItem
                                             {
                                                 Text = m.MalHizmetGrupAdi,
                                                 Value = m.MalHizmetGrupId.ToString()
                                             }).ToList();

            ViewBag.bList = biListe;
            ViewBag.kList = kdvListe;
            ViewBag.dList = dovizListe;
            ViewBag.mhgList = mhgListe;
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
            decimal FdBirimFiyatTl = 0;
            decimal FdTutar = 0;
            decimal KdvTutar = 0;
            decimal FdBirimFiyat = 0;
            var faturaId = p.FaturaId;            

            if (p.FdTutar == 0)
            {
                FdBirimFiyatTl = Math.Round((p.FdBirimFiyat * p.FdKur), 8);
                FdTutar = Math.Round((FdBirimFiyatTl * p.FdMiktar), 2);
                p.FdTutar = FdTutar;
            }
            else if (p.FdBirimFiyat == 0)
            {
                FdBirimFiyatTl = Math.Round((p.FdTutar / p.FdMiktar), 8);
                FdBirimFiyat = Math.Round((FdBirimFiyatTl / p.FdKur), 8);
                p.FdBirimFiyat = FdBirimFiyat;
            }
            KdvTutar = Math.Round((p.FdTutar * Convert.ToInt32(KdvOran) / 100), 2);

            p.FdBirimFiyatTl = FdBirimFiyatTl;
            p.KdvTutar = KdvTutar;
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
            var fatTipi = c.FaturaDetays.Where(x => x.FaturaDetayId == id).Select(y => y.Fatura.FaturaTipi).FirstOrDefault();

            List<SelectListItem> mhgListe = (from m in c.MalHizmetGrups
                                             select new SelectListItem
                                             {
                                                 Text = m.MalHizmetGrupAdi,
                                                 Value = m.MalHizmetGrupId.ToString()
                                             }).ToList();

            var malHizmet = c.FaturaDetays.Where(x => x.FaturaDetayId == id).FirstOrDefault();
            var malHizmetGrup = c.MalHizmets.Where(x => x.MalHizmetId == malHizmet.MalHizmetId).FirstOrDefault();

            List<SelectListItem> mhizListe = (from mh in c.MalHizmets.OrderBy(m => m.MalHizmetAdi).ToList()
                                              where mh.MalHizmetTuru == fatTipi && mh.MalHizmetGrupId == malHizmetGrup.MalHizmetGrupId
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

            ViewBag.mhgList = mhgListe;
            ViewBag.mhList = mhizListe;
            ViewBag.bList = biListe;
            ViewBag.kList = kdvListe;
            ViewBag.dList = dovizListe;

            var faturaId = c.FaturaDetays.Where(f => f.FaturaDetayId == id).Select(x => x.FaturaId).FirstOrDefault();
            ViewBag.faturaId = faturaId;

            var fdGet = c.FaturaDetays.Find(id);

            return PartialView("FaturaDetayGetir", fdGet);
        }
        public ActionResult FaturaDetayGuncelle(FaturaDetay p, int hesaplama)
        {
            var KdvOran = c.Kdvs.Where(x => x.KdvId == p.KdvId).Select(y => y.KdvOrani).FirstOrDefault();
            decimal FdBirimFiyatTl = 0;
            decimal FdTutar = 0;
            decimal KdvTutar = 0;
            decimal FdBirimFiyat = 0;
            var fd = c.FaturaDetays.Find(p.FaturaDetayId);

            if (hesaplama == 2)
            {
                FdBirimFiyatTl = Math.Round((p.FdBirimFiyat * p.FdKur), 8);
                FdTutar = Math.Round((FdBirimFiyatTl * p.FdMiktar), 2);
                fd.FdTutar = FdTutar;
            }
            else if (hesaplama == 3)
            {
                FdBirimFiyatTl = Math.Round((p.FdTutar / p.FdMiktar), 8);
                FdBirimFiyat = Math.Round((FdBirimFiyatTl / p.FdKur), 2);
                fd.FdBirimFiyat = FdBirimFiyat;
                FdTutar = p.FdTutar;
                fd.FdTutar = p.FdTutar;
            }
            KdvTutar = Math.Round((FdTutar * Convert.ToInt32(KdvOran) / 100), 2);

            fd.MalHizmetId = p.MalHizmetId;
            fd.FdMiktar = p.FdMiktar;
            fd.BirimId = p.BirimId;
            fd.DovizId = p.DovizId;
            fd.FdKur = p.FdKur;
            fd.FdBirimFiyatTl = FdBirimFiyatTl;
            fd.KdvId = p.KdvId;
            fd.KdvTutar = KdvTutar;
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
        public FileResult DosyaAc(int id)
        {
            string dosya = c.Faturas.Where(x => x.FaturaId == id).Select(y => y.Dosya).FirstOrDefault();

            string yol = "D://Dosya/Faturalar/" + dosya;
            byte[] bytes = System.IO.File.ReadAllBytes(yol);
            return File(bytes, "Application/octet-stream", dosya);
        }
        [HttpPost]
        public JsonResult BirimGetir(int MalHizmetId)
        {
            var birimId = c.MalHizmets.Where(x => x.MalHizmetId == MalHizmetId).Select(y => y.Birim.BirimId).FirstOrDefault();
            var birimAd = c.MalHizmets.Where(x => x.MalHizmetId == MalHizmetId).Select(y => y.Birim.BirimAdi).FirstOrDefault();

            var birim = new { birimId, birimAd };

            return Json(birim, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult MalHizmetGetir(int MalHizmetGrupId, int FaturaId)
        {
            var fatTipi = c.Faturas.Where(x => x.FaturaId == FaturaId).Select(y => y.FaturaTipi).FirstOrDefault();
            List<MalHizmet> malHizmetListe = c.MalHizmets.Where(i => i.MalHizmetGrupId == MalHizmetGrupId).OrderBy(i => i.MalHizmetAdi).ToList();

            List<SelectListItem> mhListe = (from m in malHizmetListe
                                            where m.MalHizmetTuru == fatTipi
                                            select new SelectListItem
                                            {
                                                Text = m.MalHizmetAdi,
                                                Value = m.MalHizmetId.ToString()
                                            }).ToList();

            return Json(mhListe, JsonRequestBehavior.AllowGet);
        }
    }
}