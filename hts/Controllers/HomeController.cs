

using hts.Entity;
using hts.Models;
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
            var sorgu = context.Kurumlar
                .Select(i => new HomeAnasayfa()
                {

                    misyon = i.misyon.Length > 50 ? i.misyon.Substring(0, 47) + "..." : i.misyon,
                    hizmetler = i.hizmetler.Length > 50 ? i.hizmetler.Substring(0, 47) + "..." : i.hizmetler,
                    iletisim = i.iletisim.Length > 50 ? i.iletisim.Substring(0, 47) + "..." : i.iletisim,
                    subeler = i.subeler.Length > 50 ? i.subeler.Substring(0, 47) + "..." : i.subeler


                }).ToList();
            return View(sorgu);
        }
        
    }
}