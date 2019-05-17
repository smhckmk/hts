using hts.Entity;
using hts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace hts.Controllers
{
    public class YakinController : Controller
    {

        htsContext dbContext = new htsContext();

        public ActionResult Anasayfa()
        {

            int id = 0;
            id = Convert.ToInt32(Session["yakinTc"]);
            var yakin = dbContext.Yakinlar.Find(id);
            ViewBag.yakinAd = yakin.adSoyad;

            YakinHastaAcilDurum hastayakin = new YakinHastaAcilDurum();

            hastayakin.acildurum = dbContext.AcilDurumlar.ToList();
            hastayakin.yakin = dbContext.Yakinlar.Where(x => x.yakinTc == id).ToList();
            hastayakin.hastalar = dbContext.Hastalar.ToList();

            return View(hastayakin);
        }

        public ActionResult HastaGuncelDurum()
        {
            YakinHastaGuncelOlcum hastaDurum= new YakinHastaGuncelOlcum();

            int id = 0;
            id = Convert.ToInt32(Session["yakinTc"]);
            var yakin = dbContext.Yakinlar.Find(id);
            ViewBag.yakinAd = yakin.adSoyad;


            hastaDurum.yakin = dbContext.Yakinlar.Where(x => x.yakinTc == id).ToList();
            hastaDurum.hastalar = dbContext.Hastalar.ToList();
            hastaDurum.guncelOlcum = dbContext.GuncelOlcumler.ToList();

            return View(hastaDurum);
        }

        public ActionResult Giris()
        {
            if (String.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                FormsAuthentication.SignOut();
                return View();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Giris(hts.Entity.YakinTb dt)
        {
            var yakinlar = dbContext.Yakinlar.ToList();

            foreach (var yakin in yakinlar)
            {
                if (yakin.kullaniciAdi == dt.kullaniciAdi && yakin.sifre == dt.sifre)
                {
                    Session["yakinTc"] = yakin.yakinTc;

                    return RedirectToAction("Anasayfa", "Yakin");
                }

            }
            ViewBag.mesaj = "kullanıcı adı hatalı";


            return View();
        }
    }
}