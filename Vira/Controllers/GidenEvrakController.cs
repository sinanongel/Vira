using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vira.Models;

namespace Vira.Controllers
{
    public class GidenEvrakController : Controller
    {
        // GET: GidenEvrak
        Context c = new Context();
        public ActionResult Index()
        {
            var liste = c.GidenEvraks.OrderByDescending(x => x.GidenEvrakId).ToList();
            return View(liste);
        }
        public ActionResult ModalAc()
        {
            List<SelectListItem> firmaListe = (from f in c.Firmas.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = f.FirmaAdi,
                                                   Value = f.FirmaId.ToString()
                                               }).ToList();
            ViewBag.fListe = firmaListe;

            List<SelectListItem> kurumListe = (from k in c.Kurums.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = k.KurumUnvani,
                                                   Value = k.KurumId.ToString()
                                               }).ToList();
            ViewBag.kListe = kurumListe;

            List<SelectListItem> baslikListe = (from b in c.KonuBasligis.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = b.BaslikAdi,
                                                    Value = b.KonuBasligiId.ToString()
                                                }).ToList();
            ViewBag.bListe = baslikListe;

            return PartialView("GidenEvrakEkle");
        }
        [HttpGet]
        public ActionResult GidenEvrakEkle()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult GidenEvrakEkle(GidenEvrak p)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                if (dosyaAdi != "")
                {
                    string yol = "D://Dosya/GidenEvrak/" + dosyaAdi;
                    Request.Files[0].SaveAs(yol);
                    p.GidenEvrakdosya = dosyaAdi;
                }
            }
            c.GidenEvraks.Add(p);
            TempData["Ekle"] = "";
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult DosyaEki(int id)
        {
            ViewBag.gidEvrakId = id;
            return PartialView();
        }
        [HttpPost]
        public ActionResult EkEkle(int GidenEvrakId, EvrakEk[] Ekler)
        {
            int sayac = 0;
            int ekAdet = 0;
            foreach (var ek in Ekler)
            {
                EvrakEk ee = new EvrakEk();
                ee.EvrakEkAdi = ek.EvrakEkAdi;
                ee.GidenEvrakId = GidenEvrakId;
                c.EvrakEks.Add(ee);
                sayac++;
            }
            var ge = c.GidenEvraks.Find(GidenEvrakId);
            ekAdet = Convert.ToInt32(c.GidenEvraks.Where(x => x.GidenEvrakId == GidenEvrakId).Select(y => y.EkAdet).FirstOrDefault());
            ge.EkAdet = sayac + ekAdet;
            c.SaveChanges();

            return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ModalEkListe(int id)
        {
            var liste = c.EvrakEks.OrderBy(f => f.EvrakEkId).Where(x => x.GidenEvrakId == id).ToList();

            return PartialView("EkListele", liste);
        }
        public ActionResult EkListele()
        {
            return PartialView("EkListele");
        }
        public ActionResult EkSil(int id)
        {
            int ekAdet = 0;
            var gidEvrakId = c.EvrakEks.Where(x => x.EvrakEkId == id).Select(y => y.GidenEvrakId).FirstOrDefault();
            var gidEvrak = c.GidenEvraks.Find(gidEvrakId);
            ekAdet = Convert.ToInt32(c.GidenEvraks.Where(x => x.GidenEvrakId == gidEvrakId).Select(y => y.EkAdet).FirstOrDefault());
            gidEvrak.EkAdet = ekAdet - 1;
            var ek = c.EvrakEks.Find(id);
            c.EvrakEks.Remove(ek);
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult GidenEvrakGetir(int id)
        {
            List<SelectListItem> firmaListe = (from f in c.Firmas.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = f.FirmaAdi,
                                                   Value = f.FirmaId.ToString()
                                               }).ToList();
            ViewBag.fListe = firmaListe;

            List<SelectListItem> kurumListe = (from k in c.Kurums.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = k.KurumUnvani,
                                                   Value = k.KurumId.ToString()
                                               }).ToList();
            ViewBag.kListe = kurumListe;
            var kurum = c.GidenEvraks.Where(x => x.GidenEvrakId == id).FirstOrDefault();
            List<AltBirim> altBirimList = c.AltBirims.Where(x => x.KurumId == kurum.KurumId).OrderBy(y => y.AltBirimAdi).ToList();

            List<SelectListItem> altBirimListe = (from b in altBirimList
                                                  select new SelectListItem
                                                  {
                                                      Text = b.AltBirimAdi,
                                                      Value = b.AltBirimId.ToString()
                                                  }).ToList();
            ViewBag.abListe = altBirimListe;


            List<SelectListItem> baslikListe = (from b in c.KonuBasligis.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = b.BaslikAdi,
                                                    Value = b.KonuBasligiId.ToString()
                                                }).ToList();
            ViewBag.bListe = baslikListe;

            var gidenEvrakGet = c.GidenEvraks.Find(id);
            ViewBag.dosya = c.GidenEvraks.Where(x => x.GidenEvrakId == id).Select(y => y.GidenEvrakdosya).FirstOrDefault();

            return PartialView("GidenEvrakGetir", gidenEvrakGet);
        }
        public ActionResult GidenEvrakGuncelle(GidenEvrak p)
        {
            var gidEvrak = c.GidenEvraks.Find(p.GidenEvrakId);
            if (Request.Files.Count > 0)
            {
                if (!ModelState.IsValid)
                {
                    return View("GidenEvrakGuncelle", p);
                }
                else if (gidEvrak.GidenEvrakdosya != null && p.GidenEvrakdosya != null && gidEvrak.GidenEvrakdosya != p.GidenEvrakdosya) // dosya varsa ve yeni dosya ile değiştirilecekse
                {
                    var dosyaSayi = c.GidenEvraks.Where(x => x.GidenEvrakdosya == gidEvrak.GidenEvrakdosya).Count();
                    if (dosyaSayi == 1)
                    {
                        System.IO.File.Delete("D://Dosya/GidenEvrak/" + gidEvrak.GidenEvrakdosya); //önceki siliniyor
                    }
                    string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                    string yol = "D://Dosya/GidenEvrak/" + dosyaAdi;
                    Request.Files[0].SaveAs(yol);
                    p.GidenEvrakdosya = dosyaAdi;
                    gidEvrak.GidenEvrakdosya = p.GidenEvrakdosya;
                }
                else if (gidEvrak.GidenEvrakdosya == null && p.GidenEvrakdosya != null) // var olan kayıta ilk dosya ekleme
                {
                    string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                    string yol = "D://Dosya/GidenEvrak/" + dosyaAdi;
                    Request.Files[0].SaveAs(yol);
                    p.GidenEvrakdosya = dosyaAdi;
                    gidEvrak.GidenEvrakdosya = p.GidenEvrakdosya;
                }
            }
            gidEvrak.FirmaId = p.FirmaId;
            gidEvrak.GidenEvrakSayi = p.GidenEvrakSayi;
            gidEvrak.GidenEvrakTarih = p.GidenEvrakTarih;
            gidEvrak.IslemTarihi = p.IslemTarihi;
            gidEvrak.KurumId = p.KurumId;
            gidEvrak.AltBirimId = p.AltBirimId;
            gidEvrak.KonuBasligiId = p.KonuBasligiId;
            gidEvrak.GidenEvrakKonu = p.GidenEvrakKonu;
            gidEvrak.IlgiEvrak = p.IlgiEvrak;
            TempData["Guncelle"] = "";
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public FileResult DosyaIndir(int id)
        {
            string dosya = c.GidenEvraks.Where(x => x.GidenEvrakId == id).Select(y => y.GidenEvrakdosya).FirstOrDefault();

            string yol = "D://Dosya/GidenEvrak/" + dosya;
            byte[] bytes = System.IO.File.ReadAllBytes(yol);
            return File(bytes, "Application/octet-stream", dosya);
        }
    }
}