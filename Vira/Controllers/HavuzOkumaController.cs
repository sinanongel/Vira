using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vira.Models;

namespace Vira.Controllers
{
    public class HavuzOkumaController : Controller
    {
        // GET: HavuzOkuma
        Context c = new Context();
        public ActionResult Index()
        {
            //var liste = c.HavuzOkumas.ToList();
            return View();
        }
    }
}