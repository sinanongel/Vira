using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Vira.Models;

namespace Vira.Controllers
{
    [AllowAnonymous] //Tüm kullanıcıların login sayfasına ulaşabilmesi için
    public class LoginController : Controller
    {
        Context c = new Context();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Kullanici p)
        {
            var kullanici = c.Kullanicis.FirstOrDefault(x => x.KullaniciAdi == p.KullaniciAdi && x.Sifre == p.Sifre);
            if (kullanici != null)
            {
                FormsAuthentication.SetAuthCookie(kullanici.KullaniciAdi, false);
                return RedirectToAction("Index", "AnaSayfa");
            }
            else
            {
                return View();
            }
        }
    }
}