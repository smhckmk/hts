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
        int did;

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

        //----------------------------------------Hasta Guncel Durum-------------------------------------

        public ActionResult HastaGuncelDurum()
        {
            YakinHastaGuncelOlcum hastaDurum = new YakinHastaGuncelOlcum();

            int id = 0;
            id = Convert.ToInt32(Session["yakinTc"]);
            var yakin = dbContext.Yakinlar.Find(id);
            ViewBag.yakinAd = yakin.adSoyad;


            hastaDurum.yakin = dbContext.Yakinlar.Where(x => x.yakinTc == id).ToList();
            hastaDurum.hastalar = dbContext.Hastalar.ToList();
            hastaDurum.guncelOlcum = dbContext.GuncelOlcumler.ToList();

            return View(hastaDurum);
        }

        //----------------------------------------Yakin Mesaj İşlemleri----------------------------------

        public ActionResult GelenKutusu()
        {
            int id = 0;
            id = Convert.ToInt32(Session["yakinTc"]);
            var yakın = dbContext.Yakinlar.Find(id);
            ViewBag.adSoyad = yakın.adSoyad;

            DoktorYakinHastaYakinMesaj dyhym = new DoktorYakinHastaYakinMesaj();

            dyhym.yakinlar = dbContext.Yakinlar.Where(x => x.yakinTc == id).ToList();
            dyhym.hastalar = dbContext.Hastalar.ToList();
            dyhym.doktorlar = dbContext.Doktorlar.ToList();
            dyhym.mesajlar = dbContext.YakinMesaj.ToList();


            return View(dyhym);
        }

        public ActionResult GelenMesajiYaz(int? doktorTc)
        {        
            var doktor = dbContext.Doktorlar.Find(doktorTc);
            ViewBag.adSoyad = doktor.adSoyad;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GelenMesajiYaz(hts.Entity.DoktorMesajTb dMT, string mesaj, int doktorTc, int yakinTc)
        {
            DateTime dateTime = DateTime.Now;
            string tarih = dateTime.ToString();

            if (ModelState.IsValid)
            {
                dMT.mesaj = mesaj;
                dMT.tarih = tarih;
                dMT.DoktorTbdoktorTc = doktorTc;
                dMT.YakinTbyakinTc = yakinTc;
                dbContext.DoktorMesaj.Add(dMT);
                dbContext.SaveChanges();

                return RedirectToAction("GelenKutusu");
            }

            return View();
        }


        public ActionResult MesajYaz()
        {
           
            int id = 0;
            id = Convert.ToInt32(Session["yakinTc"]);
            var doktor = dbContext.Yakinlar.Where(i => i.yakinTc == id);
            foreach (var item in doktor)
            {
                did = item.DoktorTbdoktorTc;
            }
            ViewBag.DoktorTbDoktorTc = new SelectList(dbContext.Doktorlar.Where(i => i.doktorTc == did), "doktorTc", "adSoyad");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MesajYaz(hts.Entity.DoktorMesajTb dMT, string mesaj, int DoktorTbdoktorTc)
        {
            int id = 0;
            id = Convert.ToInt32(Session["yakinTc"]);

            DateTime dateTime = DateTime.Now;
            string tarih = dateTime.ToString();

            if (ModelState.IsValid)
            {
                dMT.mesaj = mesaj;
                dMT.tarih = tarih;
                dMT.DoktorTbdoktorTc = DoktorTbdoktorTc;
                dMT.YakinTbyakinTc = id;
                dbContext.DoktorMesaj.Add(dMT);
                dbContext.SaveChanges();

                return RedirectToAction("Anasayfa");
            }

            var doktor = dbContext.Yakinlar.Where(i => i.yakinTc == id);
            foreach (var item in doktor)
            {
                did = item.DoktorTbdoktorTc;
            }

            ViewBag.DoktorTbDoktorTc = new SelectList(dbContext.Doktorlar.Where(i => i.doktorTc == did), "doktorTc", "adSoyad", dMT.DoktorTbdoktorTc);
            return View(dMT);

        }
        //----------------------------------------Yakin Giris--------------------------------------------
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