using hts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hts.Controllers
{
    public class DoktorController : Controller
    {
        private htsContext context = new htsContext();
        // GET: Doktor
        public ActionResult DoktorAnasayfa()
        {
            return View();
        }
        public ActionResult Bileklik()
        {
            return View(context.Bileklikler.ToList());
        }
    }
}