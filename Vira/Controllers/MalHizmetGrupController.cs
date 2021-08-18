using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vira.Models;

namespace Vira.Controllers
{
    public class MalHizmetGrupController : Controller
    {
        // GET: MalHizmetGrup
        Context c = new Context();
        public ActionResult Index()
        {
            var liste = c.MalHizmetGrups.ToList();
            return View(liste);
        }
        [HttpPost]
        public ActionResult ModalAc()
        {
            return PartialView("MalHizmetGrupEkle");
        }
        [HttpGet]
        public ActionResult MalHizmetGrupEkle()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult MalHizmetGrupEkle(MalHizmetGrup p)
        {
            c.MalHizmetGrups.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MalHizmetGrupGetir(int id)
        {
            var malHizmetGrup = c.MalHizmetGrups.Find(id);

            return PartialView("MalHizmetGrupGetir", malHizmetGrup);
        }
        public ActionResult MalHizmetGrupGuncelle(MalHizmetGrup p)
        {
            var malHizmetGrup = c.MalHizmetGrups.Find(p.MalHizmetGrupId);
            malHizmetGrup.MalHizmetGrupAdi = p.MalHizmetGrupAdi;
            c.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}