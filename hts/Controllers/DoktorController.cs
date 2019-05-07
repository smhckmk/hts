using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hts.Controllers
{
    public class DoktorController : Controller
    {
        // GET: Doktor
        public ActionResult DoktorAnasayfa()
        {
            return View();
        }
        public ActionResult Bileklik()
        {
            return View();
        }
    }
}