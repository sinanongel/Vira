using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vira.Models;

namespace Vira.Controllers
{
    public class SozlesmeController : Controller
    {
        // GET: Sozlesme
        Context c = new Context();
        public ActionResult Index()
        {
            var liste = c.Sozlesmes.ToList();
            return View(liste);
        }
        public ActionResult ModalAc()
        {
            List<SelectListItem> isverenKurumListe = (from k in c.Kurums.ToList()
                                                      select new SelectListItem
                                                      {
                                                          Text = k.KurumAdi,
                                                          Value = k.KurumId.ToString()
                                                      }).ToList();
            ViewBag.iKrmListe = isverenKurumListe;

            List<SelectListItem> yukleniciKurumListe = (from k in c.Kurums.ToList()
                                                        select new SelectListItem
                                                        {
                                                            Text = k.KurumAdi,
                                                            Value = k.KurumId.ToString()
                                                        }).ToList();
            ViewBag.yKrmListe = yukleniciKurumListe;

            return PartialView("SozlesmeEkle");
        }
        [HttpGet]
        public ActionResult SozlesmeEkle()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult SozlesmeEkle(Sozlesme p)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                if (dosyaAdi != "")
                {
                    string yol = "D://Dosya/Sozlesmeler/" + dosyaAdi;
                    Request.Files[0].SaveAs(yol);
                    //string yol = "~/App_Data/Dosya/" + dosyaAdi;
                    //Request.Files[0].SaveAs(Server.MapPath(yol));
                    p.Dosya = dosyaAdi;
                }
            }
            TempData["sozlesmeEkle"] = "";
            c.Sozlesmes.Add(p);
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult SozlesmeGetir(int id)
        {
            List<SelectListItem> isverenKurumListe = (from k in c.Kurums.ToList()
                                                      select new SelectListItem
                                                      {
                                                          Text = k.KurumAdi,
                                                          Value = k.KurumId.ToString()
                                                      }).ToList();
            ViewBag.iKrmListe = isverenKurumListe;

            List<SelectListItem> yukleniciKurumListe = (from k in c.Kurums.ToList()
                                                        select new SelectListItem
                                                        {
                                                            Text = k.KurumAdi,
                                                            Value = k.KurumId.ToString()
                                                        }).ToList();
            ViewBag.yKrmListe = yukleniciKurumListe;
            var dosyaAdi = c.Sozlesmes.Where(x => x.SozlesmeId == id).Select(y => y.Dosya).FirstOrDefault();
            ViewBag.dosya = dosyaAdi;
            var sozlesme = c.Sozlesmes.Find(id);

            return PartialView("SozlesmeGetir", sozlesme);
        }
        public ActionResult SozlesmeGuncelle(Sozlesme p)
        {
            var sozlesme = c.Sozlesmes.Find(p.SozlesmeId);

            if (Request.Files.Count > 0)
            {
                if (!ModelState.IsValid)
                {
                    return View("SozlesmeGuncelle", p);
                }
                else if (sozlesme.Dosya != null && p.Dosya != null && sozlesme.Dosya != p.Dosya) // dosya varsa ve yeni dosya ile değiştirilecekse
                {
                    var dosyaSayi = c.Sozlesmes.Where(x => x.Dosya == sozlesme.Dosya).Count();
                    if (dosyaSayi == 1)
                    {
                        System.IO.File.Delete("D://Dosya/Sozlesmeler/" + sozlesme.Dosya); //önceki siliniyor
                    }                        
                    string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                    string yol = "D://Dosya/Sozlesmeler/" + dosyaAdi;
                    Request.Files[0].SaveAs(yol);
                    p.Dosya = dosyaAdi;
                    sozlesme.Dosya = p.Dosya;
                }
                else if (sozlesme.Dosya == null && p.Dosya != null) // var olan kayıta ilk dosya ekleme
                {
                    string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                    string yol = "D://Dosya/Sozlesmeler/" + dosyaAdi;
                    Request.Files[0].SaveAs(yol);
                    p.Dosya = dosyaAdi;
                    sozlesme.Dosya = p.Dosya;
                }
            }
            sozlesme.IsverenKurumId = p.IsverenKurumId;
            sozlesme.YukleniciKurumId = p.YukleniciKurumId;
            sozlesme.SozlesmeKonusu = p.SozlesmeKonusu;
            sozlesme.SozlesmeTarihi = p.SozlesmeTarihi;
            sozlesme.SozlesmeBedeli = p.SozlesmeBedeli;
            sozlesme.SozlesmeSuresi = p.SozlesmeSuresi;
            TempData["sozlesmeGuncelle"] = "";
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public FileResult DosyaIndir(int id)
        {
            string dosya = c.Sozlesmes.Where(x => x.SozlesmeId == id).Select(y => y.Dosya).FirstOrDefault();

            //dosya = dosya.Replace(",", "");

            //string yol = Server.MapPath("~/App_Data/Dosya/") + dosya;
            string yol = "D://Dosya/Sozlesmeler/" + dosya;
            byte[] bytes = System.IO.File.ReadAllBytes(yol);
            return File(bytes, "Application/octet-stream", dosya);
        }
    }
}