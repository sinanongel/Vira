using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vira.Models;

namespace Vira.Controllers
{
    public class RaporFaturaController : Controller
    {
        // GET: RaporFatura
        Context c = new Context();
        public ActionResult Index()
        {
            var liste = c.FaturaDetays.ToList();
            return View(liste);
        }
    }
}