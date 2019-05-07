

using hts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hts.Controllers
{
    public class HomeController : Controller
    {
        private htsContext context = new htsContext();
        // GET: Home
        public ActionResult Anasayfa()
        {
            
            return View(context.Doktorlar.ToList());
        }
    }
}