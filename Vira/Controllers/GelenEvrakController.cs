using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vira.Models;

namespace Vira.Controllers
{
    public class GelenEvrakController : Controller
    {
        // GET: GelenEvrak
        Context c = new Context();
        public ActionResult Index()
        {
            var liste = c.GelenEvraks.OrderByDescending(x => x.GelenEvrakId).ToList();
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

            return PartialView("GelenEvrakEkle");
        }
        [HttpGet]
        public ActionResult GelenEvrakEkle()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult GelenEvrakEkle(GelenEvrak p)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                if (dosyaAdi != "")
                {
                    string yol = "D://Dosya/GelenEvrak/" + dosyaAdi;
                    Request.Files[0].SaveAs(yol);
                    p.GelenEvrakdosya = dosyaAdi;
                }
            }
            c.GelenEvraks.Add(p);
            TempData["Ekle"] = "";
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult AltBirimListe(int KurumId)
        {
            List<AltBirim> altBirimList = c.AltBirims.Where(i => i.KurumId == KurumId).OrderBy(i => i.AltBirimAdi).ToList();

            List<SelectListItem> altBirimListe = (from ab in altBirimList
                                                  select new SelectListItem
                                                  {
                                                      Text = ab.AltBirimAdi,
                                                      Value = ab.AltBirimId.ToString()
                                                  }).ToList();

            return Json(altBirimListe, JsonRequestBehavior.AllowGet);
        }
        public FileResult DosyaIndir(int id)
        {
            string dosya = c.GelenEvraks.Where(x => x.GelenEvrakId == id).Select(y => y.GelenEvrakdosya).FirstOrDefault();

            string yol = "D://Dosya/GelenEvrak/" + dosya;
            byte[] bytes = System.IO.File.ReadAllBytes(yol);
            return File(bytes, "Application/octet-stream", dosya);
        }
        public ActionResult DosyaEki(int id)
        {
            ViewBag.gelEvrakId = id;
            return PartialView();
        }
        [HttpPost]
        public ActionResult EkEkle(int GelenEvrakId, EvrakEk[] Ekler)
        {
            int sayac = 0;
            int ekAdet = 0;
            foreach (var ek in Ekler)
            {
                EvrakEk ee = new EvrakEk();
                ee.EvrakEkAdi = ek.EvrakEkAdi;
                ee.GelenEvrakId = GelenEvrakId;
                c.EvrakEks.Add(ee);
                sayac++;
            }
            var ge = c.GelenEvraks.Find(GelenEvrakId);
            ekAdet = Convert.ToInt32(c.GelenEvraks.Where(x => x.GelenEvrakId == GelenEvrakId).Select(y => y.EkAdet).FirstOrDefault());
            ge.EkAdet = sayac + ekAdet;
            c.SaveChanges();

            return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ModalEkListe(int id)
        {
            var liste = c.EvrakEks.OrderBy(f => f.EvrakEkId).Where(x => x.GelenEvrakId == id).ToList();

            return PartialView("EkListele", liste);
        }
        public ActionResult EkListele()
        {
            return PartialView("EkListele");
        }
        public ActionResult EkSil(int id)
        {
            int ekAdet = 0;
            var gelEvrakId = c.EvrakEks.Where(x => x.EvrakEkId == id).Select(y => y.GelenEvrakId).FirstOrDefault();
            var gelEvrak = c.GelenEvraks.Find(gelEvrakId);
            ekAdet = Convert.ToInt32(c.GelenEvraks.Where(x => x.GelenEvrakId == gelEvrakId).Select(y => y.EkAdet).FirstOrDefault());
            gelEvrak.EkAdet = ekAdet - 1;
            var ek = c.EvrakEks.Find(id);
            c.EvrakEks.Remove(ek);
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult EkGuncelle(EvrakEk Ekler)
        {

            return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);
        }
        public ActionResult GelenEvrakGetir(int id)
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

            var kurum = c.GelenEvraks.Where(x => x.GelenEvrakId == id).FirstOrDefault();
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

            var gelenEvrakGet = c.GelenEvraks.Find(id);
            ViewBag.dosya = c.GelenEvraks.Where(x => x.GelenEvrakId == id).Select(y => y.GelenEvrakdosya).FirstOrDefault();

            return PartialView("GelenEvrakGetir", gelenEvrakGet);
        }
        public ActionResult GelenEvrakGuncelle(GelenEvrak p)
        {
            var gelEvrak = c.GelenEvraks.Find(p.GelenEvrakId);
            if (Request.Files.Count > 0)
            {
                if (!ModelState.IsValid)
                {
                    return View("GelenEvrakGuncelle", p);
                }
                else if (gelEvrak.GelenEvrakdosya != null && p.GelenEvrakdosya != null && gelEvrak.GelenEvrakdosya != p.GelenEvrakdosya) // dosya varsa ve yeni dosya ile değiştirilecekse
                {
                    var dosyaSayi = c.GelenEvraks.Where(x => x.GelenEvrakdosya == gelEvrak.GelenEvrakdosya).Count();
                    if (dosyaSayi == 1)
                    {
                        System.IO.File.Delete("D://Dosya/GelenEvrak/" + gelEvrak.GelenEvrakdosya); //önceki siliniyor
                    }                    
                    string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                    string yol = "D://Dosya/GelenEvrak/" + dosyaAdi;
                    Request.Files[0].SaveAs(yol);
                    p.GelenEvrakdosya = dosyaAdi;
                    gelEvrak.GelenEvrakdosya = p.GelenEvrakdosya;
                }
                else if (gelEvrak.GelenEvrakdosya == null && p.GelenEvrakdosya != null) // var olan kayıta ilk dosya ekleme
                {
                    string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                    string yol = "D://Dosya/GelenEvrak/" + dosyaAdi;
                    Request.Files[0].SaveAs(yol);
                    p.GelenEvrakdosya = dosyaAdi;
                    gelEvrak.GelenEvrakdosya = p.GelenEvrakdosya;
                }
            }
            gelEvrak.FirmaId = p.FirmaId;
            gelEvrak.GelenEvrakSayi = p.GelenEvrakSayi;
            gelEvrak.GelenEvrakTarih = p.GelenEvrakTarih;
            gelEvrak.TebligatTarihi = p.TebligatTarihi;
            gelEvrak.KurumId = p.KurumId;
            gelEvrak.AltBirimId = p.AltBirimId;
            gelEvrak.KonuBasligiId = p.KonuBasligiId;
            gelEvrak.GelenEvrakKonu = p.GelenEvrakKonu;
            gelEvrak.IlgiEvrak = p.IlgiEvrak;
            TempData["Guncelle"] = "";
            c.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}