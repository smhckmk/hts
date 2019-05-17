using hts.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Net;
using System.Net.Mail;

namespace hts.Controllers
{
    public class KurumController : Controller
    {
        htsContext dbContext = new htsContext();
        string mesaj;
        public ActionResult KurumAnasayfa()
        {
            var bileklikler = dbContext.Bileklikler.Include(b => b.hastaTb);
            return View(bileklikler.ToList());

        }

        //------------Bileklik İslemleri---------------
        public ActionResult BileklikSilme(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BileklikTb bileklikTb = dbContext.Bileklikler.Find(id);
            if (bileklikTb == null)
            {
                return HttpNotFound();
            }
            return View(bileklikTb);

        }

        [HttpPost, ActionName("BileklikSilme")]
        [ValidateAntiForgeryToken]
        public ActionResult BileklikSilmeOnay(int id)
        {
            BileklikTb bileklikTb = dbContext.Bileklikler.Find(id);
            dbContext.Bileklikler.Remove(bileklikTb);
            dbContext.SaveChanges();
            return RedirectToAction("KurumAnasayfa");
        }

        public ActionResult BileklikDuzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BileklikTb bileklikTb = dbContext.Bileklikler.Find(id);
            if (bileklikTb == null)
            {
                return HttpNotFound();
            }
            ViewBag.HastaTbhastaTc = new SelectList(dbContext.Hastalar, "hastaTc", "adSoyad", bileklikTb.HastaTbhastaTc);
            return View(bileklikTb);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BileklikDuzenle([Bind(Include = "Id,imalatTarihi,verilebilmeDurumu,HastaTbhastaTc")] BileklikTb bileklikTb)
        {
            if (ModelState.IsValid)
            {
                dbContext.Entry(bileklikTb).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("KurumAnasayfa");
            }
            ViewBag.HastaTbhastaTc = new SelectList(dbContext.Hastalar, "hastaTc", "adSoyad", bileklikTb.HastaTbhastaTc);
            return View(bileklikTb);
        }

        public ActionResult BileklikOlusturma()
        {
            ViewBag.HastaTbhastaTc = new SelectList(dbContext.Hastalar, "hastaTc", "adSoyad");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BileklikOlusturma([Bind(Include = "Id,imalatTarihi,verilebilmeDurumu,HastaTbhastaTc")] BileklikTb bileklikTb)
        {
            if (ModelState.IsValid)
            {
                dbContext.Bileklikler.Add(bileklikTb);
                dbContext.SaveChanges();
                return RedirectToAction("KurumAnasayfa");
            }

            ViewBag.HastaTbhastaTc = new SelectList(dbContext.Hastalar, "hastaTc", "adSoyad", bileklikTb.HastaTbhastaTc);
            return View(bileklikTb);
        }

        //---------------Doktor İslemleri------------------------------------------------------------
        public ActionResult DoktorAnasayfa()
        {
            var doktorlar = dbContext.Doktorlar.Include(d => d.uzmanlikTb);
            return View(doktorlar.ToList());
        }

        public ActionResult DoktorSil(int? id)
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
            return View(doktorTb);
        }


        [HttpPost, ActionName("DoktorSil")]
        [ValidateAntiForgeryToken]
        public ActionResult DoktorSilOnay(int id)
        {
            DoktorTb doktorTb = dbContext.Doktorlar.Find(id);
            dbContext.Doktorlar.Remove(doktorTb);
            dbContext.SaveChanges();
            return RedirectToAction("DoktorAnasayfa");
        }

        public ActionResult DoktorOlusturma()
        {
            ViewBag.UzmanlikTbId = new SelectList(dbContext.Uzmanlar, "Id", "uzmanlikAdi");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoktorOlusturma([Bind(Include = "doktorTc,adSoyad,telefon,adres,maas,mail,kullaniciAdi,sifre,UzmanlikTbId")] DoktorTb doktorTb)
        {
            if (ModelState.IsValid)
            {
                dbContext.Doktorlar.Add(doktorTb);
                dbContext.SaveChanges();
                return RedirectToAction("DoktorAnasayfa");
            }

            ViewBag.UzmanlikTbId = new SelectList(dbContext.Uzmanlar, "Id", "uzmanlikAdi", doktorTb.UzmanlikTbId);
            return View(doktorTb);
        }

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
        public ActionResult DoktorDuzenle([Bind(Include = "doktorTc,adSoyad,maas")] DoktorTb doktorTb)
        {
            if (ModelState.IsValid)
            {
                dbContext.Entry(doktorTb).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("DoktorAnasayfa");
            }
            ViewBag.UzmanlikTbId = new SelectList(dbContext.Uzmanlar, "Id", "uzmanlikAdi", doktorTb.UzmanlikTbId);
            return View(doktorTb);
        }

        //-----------------Hastane İşlemleri------------------------------------------------------------

        public ActionResult HastaneAnasayfa()
        {
            var hastaneler = dbContext.Hastaneler.Include(h => h.kurumTb);
            return View(hastaneler.ToList());
        }

        public ActionResult HastaneSilme(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HastaneTb hastaneTb = dbContext.Hastaneler.Find(id);
            if (hastaneTb == null)
            {
                return HttpNotFound();
            }
            return View(hastaneTb);
        }


        [HttpPost, ActionName("HastaneSilme")]
        [ValidateAntiForgeryToken]
        public ActionResult HastaneSilmeOnay(int id)
        {
            HastaneTb hastaneTb = dbContext.Hastaneler.Find(id);
            dbContext.Hastaneler.Remove(hastaneTb);
            dbContext.SaveChanges();
            return RedirectToAction("HastaneAnasayfa");
        }

        public ActionResult HastaneDetay(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HastaneTb hastaneTb = dbContext.Hastaneler.Find(id);
            if (hastaneTb == null)
            {
                return HttpNotFound();
            }
            return View(hastaneTb);
        }


        public ActionResult HastaneOlusturma()
        {
            ViewBag.KurumTbkurumId = new SelectList(dbContext.Kurumlar, "kurumId", "kurumAdi");
           
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HastaneOlusturma(hts.Entity.HastaneTb ht, string hastaneAdi, string telefon, string adres, string mail, string kullaniciAdi, string sifre,bool uyelik, int KurumTbkurumId)
        {
            HastaneTb hastaneTb = new HastaneTb();

            if (ModelState.IsValid)
            {
                hastaneTb.hastaneAdi = hastaneAdi;
                hastaneTb.telefon = telefon;
                hastaneTb.adres = adres;
                hastaneTb.mail = mail;
                hastaneTb.kullaniciAdi = kullaniciAdi;
                hastaneTb.sifre = sifre;
                hastaneTb.uyelik = uyelik;               
                hastaneTb.KurumTbkurumId = KurumTbkurumId;

                dbContext.Hastaneler.Add(hastaneTb);

                dbContext.SaveChanges();

                MailMessage msj = new MailMessage();
                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("semihcakmak7126@gmail.com", "merve0804semih.");
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                msj.From = new MailAddress("semihcakmak7126@gmail.com", "merve0804semih.");
                msj.To.Add(mail);


                var yeniHastane = dbContext.Hastaneler.Where(i => i.mail == mail).ToList();
                foreach (var item in yeniHastane)
                {
                    mesaj = "Uyelik bilgileriniz: " + item.hastaneAdi + " " + item.telefon + " " + item.adres + " " + item.mail + " " + item.kullaniciAdi + " " + item.sifre + ", seklinde sisteme kayit edilmiştir. İyi Günler";
                }
                msj.Subject = "Uyelik Hakkında";

                msj.Body = mesaj;

                client.Send(msj);

                return RedirectToAction("HastaneAnasayfa");
            }

            ViewBag.KurumTbkurumId = new SelectList(dbContext.Kurumlar, "kurumId", "kurumAdi", hastaneTb.KurumTbkurumId);
           
            return View();
        }
       
        public ActionResult HastaneDuzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HastaneTb hastaneTb = dbContext.Hastaneler.Find(id);
            if (hastaneTb == null)
            {
                return HttpNotFound();
            }
            ViewBag.KurumTbkurumId = new SelectList(dbContext.Kurumlar, "kurumId", "kurumAdi", hastaneTb.KurumTbkurumId);
            return View(hastaneTb);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HastaneDuzenle([Bind(Include = "hastaneId,hastaneAdi,telefon,adres,mail,kullaniciAdi,sifre,uyelik,KurumTbkurumId")] HastaneTb hastaneTb, int id, string idMail, bool uyelik)
        {
            if (ModelState.IsValid)
            {
                dbContext.Entry(hastaneTb).State = EntityState.Modified;
                dbContext.SaveChanges();

                MailMessage msj = new MailMessage();
                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("semihcakmak7126@gmail.com", "merve0804semih.");
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                msj.From = new MailAddress("semihcakmak7126@gmail.com", "merve0804semih.");
                msj.To.Add(idMail);

                if (uyelik == true)
                {
                    var yeniHastane = dbContext.Hastaneler.Where(i => i.hastaneId == id).ToList();
                    foreach (var item in yeniHastane)
                    {
                        mesaj = " Sisteme Başarı ile Kayıt Edildiniz. Kullanıcı Adınız ve Sifreniz ile sisteme giriş yapabilirsiniz\n" + item.kullaniciAdi + " ," + item.sifre;
                    }
                    msj.Subject = "Üyelik Hakkında";

                    msj.Body = mesaj;

                    client.Send(msj);
                }
                else
                {
                    mesaj = "Degerlendirmelerimiz Sonucunda Sisteme Uyelik Talebiniz Olumsuz olarak sonuclanmıştır. İyi Günler Dileriz.";
                    msj.Subject = "Uyelik Hakkında";

                    msj.Body = mesaj;

                    client.Send(msj);
                }

                return RedirectToAction("HastaneAnasayfa");
            }
            ViewBag.KurumTbkurumId = new SelectList(dbContext.Kurumlar, "kurumId", "kurumAdi", hastaneTb.KurumTbkurumId);
            return View(hastaneTb);
        }


        //-------------------------------Kurum Giris----------------------------------------------------

        public ActionResult KurumGiris()
        {
            if (String.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                FormsAuthentication.SignOut();
                return View();
            }
            return View();
        }

        [HttpPost]
        public ActionResult KurumGiris(hts.Entity.KurumTb dt)
        {
            var kurumlar = dbContext.Kurumlar.ToList();

            foreach (var kurum in kurumlar)
            {
                if (kurum.kullaniciAdi == dt.kullaniciAdi && kurum.sifre == dt.sifre)
                {
                    //Session["yakinTc"] = kurum.yakinTc;

                    return RedirectToAction("KurumAnasayfa", "Kurum");
                }
                else
                {
                    ViewBag.mesaj = "kullanıcı ve sifre hatalı";
                }

            }


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