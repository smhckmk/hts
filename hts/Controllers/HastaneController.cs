using hts.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace hts.Controllers
{
    public class HastaneController : Controller
    {
        htsContext dbContext = new htsContext();
        
        public ActionResult HastaneAnasayfa()
        {
            int id = 0;
            id = Convert.ToInt32(Session["hastaneId"]);
            var hastane = dbContext.Hastaneler.Find(id);
            ViewBag.hastaneAdi = hastane.hastaneAdi;


            var doktorlar = dbContext.Doktorlar.Include(d => d.uzmanlikTb);
            return View(doktorlar.ToList());
        }

        //----------------------Doktor İşlemeleri-----------------------------
        public ActionResult DoktorDuzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoktorTb doktorTb = dbContext.Doktorlar.Find(id);
            if (doktorTb == null)
            {
                return HttpNotFound();
            }
            ViewBag.UzmanlikTbId = new SelectList(dbContext.Uzmanlar, "Id", "uzmanlikAdi", doktorTb.UzmanlikTbId);
            return View(doktorTb);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoktorDuzenle([Bind(Include = "doktorTc,adSoyad,telefon,adres,maas,toplamBileklikSayisi,kontrolundekiBileklikSayisi,UzmanlikTbId")] DoktorTb doktorTb)
        {
            if (ModelState.IsValid)
            {
                dbContext.Entry(doktorTb).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("HastaneAnasayfa");
            }
            ViewBag.UzmanlikTbId = new SelectList(dbContext.Uzmanlar, "Id", "uzmanlikAdi", doktorTb.UzmanlikTbId);
            return View(doktorTb);
        }

        //----------------------HastaDurum İşlemleri-----------------------
        public ActionResult HastaDurumAnasayfa()
        {
            var hastaDurumlar = dbContext.HastaDurumlar.Include(h => h.hastaTb);
            return View(hastaDurumlar.ToList());
        }

        public ActionResult HastaDurumOlusturma()
        {
            ViewBag.HastaTbhastaTc = new SelectList(dbContext.Hastalar, "hastaTc", "adSoyad");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HastaDurumOlusturma([Bind(Include = "Id,yas,kilo,boy,yagOrani,nabiz,konumY,konumX,alerjikDurumlar,ozelHastaliklar,kanSekeri,HastaTbhastaTc")] HastaDurumTb hastaDurumTb)
        {
            if (ModelState.IsValid)
            {
                dbContext.HastaDurumlar.Add(hastaDurumTb);
                dbContext.SaveChanges();
                return RedirectToAction("HastaDurumAnasayfa");
            }

            ViewBag.HastaTbhastaTc = new SelectList(dbContext.Hastalar, "hastaTc", "adSoyad", hastaDurumTb.HastaTbhastaTc);
            return View(hastaDurumTb);
        }

        public ActionResult HastaDurumDuzenleme(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HastaDurumTb hastaDurumTb = dbContext.HastaDurumlar.Find(id);
            if (hastaDurumTb == null)
            {
                return HttpNotFound();
            }
            ViewBag.HastaTbhastaTc = new SelectList(dbContext.Hastalar, "hastaTc", "adSoyad", hastaDurumTb.HastaTbhastaTc);
            return View(hastaDurumTb);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HastaDurumDuzenleme([Bind(Include = "Id,yas,kilo,boy,yagOrani,nabiz,konumY,konumX,alerjikDurumlar,ozelHastaliklar,kanSekeri,HastaTbhastaTc")] HastaDurumTb hastaDurumTb)
        {
            if (ModelState.IsValid)
            {
                dbContext.Entry(hastaDurumTb).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("HastaDurumAnasayfa");
            }
            ViewBag.HastaTbhastaTc = new SelectList(dbContext.Hastalar, "hastaTc", "adSoyad", hastaDurumTb.HastaTbhastaTc);
            return View(hastaDurumTb);
        }

      
        public ActionResult HastaDurumSilme(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HastaDurumTb hastaDurumTb = dbContext.HastaDurumlar.Find(id);
            if (hastaDurumTb == null)
            {
                return HttpNotFound();
            }
            return View(hastaDurumTb);
        }

      
        [HttpPost, ActionName("HastaDurumSilme")]
        [ValidateAntiForgeryToken]
        public ActionResult HastaDurumSilmeOnay(int id)
        {
            HastaDurumTb hastaDurumTb = dbContext.HastaDurumlar.Find(id);
            dbContext.HastaDurumlar.Remove(hastaDurumTb);
            dbContext.SaveChanges();
            return RedirectToAction("HastaDurumAnasayfa");
        }

        //------------------Hasta İşlemleri-------------------

        public ActionResult HastaAnasayfa()
        {
            var hastalar = dbContext.Hastalar.Include(h => h.doktorTb);
            return View(hastalar.ToList());
        }

        public ActionResult HastaOlusturma()
        {
            ViewBag.DoktorTbdoktorTc = new SelectList(dbContext.Doktorlar, "doktorTc", "adSoyad");
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HastaOlusturma([Bind(Include = "hastaTc,adSoyad,telefon,adres,hastaligi,DoktorTbdoktorTc")] HastaTb hastaTb)
        {
            if (ModelState.IsValid)
            {
                dbContext.Hastalar.Add(hastaTb);
                dbContext.SaveChanges();
                return RedirectToAction("HastaAnasayfa");
            }

            ViewBag.DoktorTbdoktorTc = new SelectList(dbContext.Doktorlar, "doktorTc", "adSoyad", hastaTb.DoktorTbdoktorTc);
            return View(hastaTb);
        }

        
        public ActionResult HastaDuzenleme(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HastaTb hastaTb = dbContext.Hastalar.Find(id);
            if (hastaTb == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoktorTbdoktorTc = new SelectList(dbContext.Doktorlar, "doktorTc", "adSoyad", hastaTb.DoktorTbdoktorTc);
            return View(hastaTb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HastaDuzenleme([Bind(Include = "hastaTc,adSoyad,telefon,adres,hastaligi,DoktorTbdoktorTc")] HastaTb hastaTb)
        {
            if (ModelState.IsValid)
            {
                dbContext.Entry(hastaTb).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("HastaAnasayfa");
            }
            ViewBag.DoktorTbdoktorTc = new SelectList(dbContext.Doktorlar, "doktorTc", "adSoyad", hastaTb.DoktorTbdoktorTc);
            return View(hastaTb);
        }
        
        public ActionResult HastaSilme(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HastaTb hastaTb = dbContext.Hastalar.Find(id);
            if (hastaTb == null)
            {
                return HttpNotFound();
            }
            return View(hastaTb);
        }

       
        [HttpPost, ActionName("HastaSilme")]
        [ValidateAntiForgeryToken]
        public ActionResult HastaSilmeOnay(int id)
        {
            HastaTb hastaTb = dbContext.Hastalar.Find(id);
            dbContext.Hastalar.Remove(hastaTb);
            dbContext.SaveChanges();
            return RedirectToAction("HastaAnasayfa");
        }

        //----------------Yakin İşlemleri----------------------------------

        public ActionResult YakinAnasayfa()
        {
            var yakinlar = dbContext.Yakinlar.Include(y => y.doktorTb);
            return View(yakinlar.ToList());
        }

        public ActionResult YakinOlusturma()
        {
            ViewBag.DoktorTbdoktorTc = new SelectList(dbContext.Doktorlar, "doktorTc", "adSoyad");
            ViewBag.HastaTbhastaTc = new SelectList(dbContext.Hastalar, "hastaTc", "adSoyad");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YakinOlusturma([Bind(Include = "yakinTc,adSoyad,telefon,adres,HastaTbhastaTc,DoktorTbdoktorTc")] YakinTb yakinTb)
        {
            if (ModelState.IsValid)
            {
                dbContext.Yakinlar.Add(yakinTb);
                dbContext.SaveChanges();
                return RedirectToAction("YakinAnasayfa");
            }

            ViewBag.DoktorTbdoktorTc = new SelectList(dbContext.Doktorlar, "doktorTc", "adSoyad", yakinTb.DoktorTbdoktorTc);
            ViewBag.HastaTbhastaTc = new SelectList(dbContext.Hastalar, "hastaTc", "adSoyad", yakinTb.HastaTbhastaTc);
            return View(yakinTb);
        }

       
        public ActionResult YakinDuzenleme(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YakinTb yakinTb = dbContext.Yakinlar.Find(id);
            if (yakinTb == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoktorTbdoktorTc = new SelectList(dbContext.Doktorlar, "doktorTc", "adSoyad", yakinTb.DoktorTbdoktorTc);
            ViewBag.HastaTbhastaTc = new SelectList(dbContext.Hastalar, "hastaTc", "adSoyad", yakinTb.HastaTbhastaTc);
            return View(yakinTb);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YakinDuzenleme([Bind(Include = "yakinTc,adSoyad,telefon,adres,HastaTbhastaTc,DoktorTbdoktorTc")] YakinTb yakinTb)
        {
            if (ModelState.IsValid)
            {
                dbContext.Entry(yakinTb).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("YakinAnasayfa");
            }
            ViewBag.DoktorTbdoktorTc = new SelectList(dbContext.Doktorlar, "doktorTc", "adSoyad", yakinTb.DoktorTbdoktorTc);
            ViewBag.HastaTbhastaTc = new SelectList(dbContext.Hastalar, "hastaTc", "adSoyad", yakinTb.HastaTbhastaTc);
            return View(yakinTb);
        }

        
        public ActionResult YakinSilme(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            YakinTb yakinTb = dbContext.Yakinlar.Find(id);
            if (yakinTb == null)
            {
                return HttpNotFound();
            }
            return View(yakinTb);
        }
        
        [HttpPost, ActionName("YakinSilme")]
        [ValidateAntiForgeryToken]
        public ActionResult YakinSilmeOnay(int id)
        {
            YakinTb yakinTb = dbContext.Yakinlar.Find(id);
            dbContext.Yakinlar.Remove(yakinTb);
            dbContext.SaveChanges();
            return RedirectToAction("YakinAnasayfa");
        }

        //---------------------Hastane Giris-------------------------
        public ActionResult HastaneGiris()
        {
            if (String.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                FormsAuthentication.SignOut();
                return View();
            }
            return View();
        }

        [HttpPost]
        public ActionResult HastaneGiris(hts.Entity.HastaneTb dt)
        {

            var hastaneler = dbContext.Hastaneler.ToList();

            foreach (var hastane in hastaneler)
            {
                if (hastane.kullaniciAdi == dt.kullaniciAdi && hastane.sifre == dt.sifre)
                {
                    Session["hastaneId"] = hastane.hastaneId;

                    return RedirectToAction("HastaneAnasayfa", "Hastane");
                }

            }
            ViewBag.mesaj = "hatalı";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}