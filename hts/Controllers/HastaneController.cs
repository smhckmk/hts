using hts.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace hts.Controllers
{
    public class HastaneController : Controller
    {
        htsContext dbContext = new htsContext();
        string mesaj;

        public ActionResult HastaneAnasayfa()
        {
            int id = 0;
            id = Convert.ToInt32(Session["hastaneId"]);
            var hastane = dbContext.Hastaneler.Find(id);



            var doktorlar = dbContext.Doktorlar.Include(d => d.uzmanlikTb);
            return View(doktorlar.ToList());
        }

        //-------------------Hastane kayıt-----------------------------------

        public ActionResult Kayit()
        {
            ViewBag.KurumTbkurumId = new SelectList(dbContext.Kurumlar, "kurumId", "kurumAdi");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Kayit(hts.Entity.HastaneTb ht,string hastaneAdi,string telefon,string adres,string mail,string kullaniciAdi,string sifre, int KurumTbkurumId)
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
                    mesaj = "Uyelik bilgileriniz: "+item.hastaneAdi + " " + item.telefon + " " + item.adres + " " + item.mail + " " + item.kullaniciAdi + " " + item.sifre+", seklinde olup ilgili degerlendirmeler sonucunda bilgi verilecektir. İyi Günler";
                }
                msj.Subject = "Uyelik Hakkında";

                msj.Body = mesaj;

                client.Send(msj);

                return RedirectToAction("KayitMesaj");
            }

            ViewBag.KurumTbkurumId = new SelectList(dbContext.Kurumlar, "kurumId", "kurumAdi", hastaneTb.KurumTbkurumId);
            return View();
        }

     
        public ActionResult KayitMesaj()
        {
            
            return View();
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
        public ActionResult DoktorDuzenle([Bind(Include = "doktorTc,adSoyad,telefon,adres,maas,mail,kullaniciAdi,sifre,UzmanlikTbId")] DoktorTb doktorTb, int? id, string idMail)
        {
            if (ModelState.IsValid)
            {

                dbContext.Entry(doktorTb).State = EntityState.Modified;
                dbContext.SaveChanges();

                MailMessage msj = new MailMessage();
                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("semihcakmak7126@gmail.com", "merve0804semih.");
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                msj.From = new MailAddress("semihcakmak7126@gmail.com", "merve0804semih.");
                msj.To.Add(idMail);


                var yeniDoktor = dbContext.Doktorlar.Where(i => i.doktorTc == id).ToList();
                foreach (var item in yeniDoktor)
                {
                    mesaj = item.adSoyad + " " + item.telefon + " " + item.adres + " " + item.maas + " " + item.mail + " " + item.kullaniciAdi + " " + item.sifre;
                }
                msj.Subject = "Bilgileriniz Guncellenmitir";

                msj.Body = mesaj;

                client.Send(msj);

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
        public ActionResult HastaDurumDuzenleme([Bind(Include = "Id,yas,kilo,boy,yagOrani,nabiz,konumY,konumX,ozelHastaliklar,alerjikDurumlar,kanSekeri,HastaTbhastaTc")] HastaDurumTb hastaDurumTb)
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
        public ActionResult YakinOlusturma([Bind(Include = "yakinTc,adSoyad,telefon,adres,mail,kullaniciAdi,sifre,HastaTbhastaTc,DoktorTbdoktorTc")] YakinTb yakinTb)
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
        public ActionResult YakinDuzenleme([Bind(Include = "yakinTc,adSoyad,telefon,adres,mail,kullaniciAdi,sifre,HastaTbhastaTc,DoktorTbdoktorTc")] YakinTb yakinTb,int? id,string idMail)
        {
            if (ModelState.IsValid)
            {
                dbContext.Entry(yakinTb).State = EntityState.Modified;
                dbContext.SaveChanges();

                MailMessage msj = new MailMessage();
                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("semihcakmak7126@gmail.com", "merve0804semih.");
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                msj.From = new MailAddress("semihcakmak7126@gmail.com", "merve0804semih.");
                msj.To.Add(idMail);


                var yeniYakin = dbContext.Yakinlar.Where(i => i.yakinTc == id).ToList();
                foreach (var item in yeniYakin)
                {
                    mesaj = item.adSoyad + " " + item.telefon + " " + item.adres + " " + item.mail + " " + item.kullaniciAdi + " " + item.sifre;
                }
                msj.Subject = "bilgileriniz guncellenmistir";

                msj.Body = mesaj;

                client.Send(msj);

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
                {
                    ViewBag.mesaj = "hatalı";
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