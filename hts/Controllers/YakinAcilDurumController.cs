using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using hts.Entity;
using hts.Models;

namespace hts.Controllers
{
    public class YakinAcilDurumController : Controller
    {
        private htsContext db = new htsContext();

        // GET: YakinAcilDurum
        public ActionResult Index()
        {
            int id = 0;
            id = Convert.ToInt32(Session["yakinTc"]);

           

            YakinHastaAcilDurum hy = new YakinHastaAcilDurum();
            hy.acildurum = db.AcilDurumlar.ToList();
            hy.yakin = db.Yakinlar.Where(x => x.yakinTc == id).ToList() ;
            hy.hastalar = db.Hastalar.ToList();

            return View(hy);
        }

        // GET: YakinAcilDurum/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcilDurumTb acilDurumTb = db.AcilDurumlar.Find(id);
            if (acilDurumTb == null)
            {
                return HttpNotFound();
            }
            return View(acilDurumTb);
        }

        // GET: YakinAcilDurum/Create
        public ActionResult Create()
        {
            ViewBag.HastaTbhastaTc = new SelectList(db.Hastalar, "hastaTc", "adSoyad");
            return View();
        }

        // POST: YakinAcilDurum/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,aKonumX,aKonumY,aSicaklik,aNabiz,olcumZamani,HastaTbhastaTc")] AcilDurumTb acilDurumTb)
        {
            if (ModelState.IsValid)
            {
                db.AcilDurumlar.Add(acilDurumTb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HastaTbhastaTc = new SelectList(db.Hastalar, "hastaTc", "adSoyad", acilDurumTb.HastaTbhastaTc);
            return View(acilDurumTb);
        }

        // GET: YakinAcilDurum/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcilDurumTb acilDurumTb = db.AcilDurumlar.Find(id);
            if (acilDurumTb == null)
            {
                return HttpNotFound();
            }
            ViewBag.HastaTbhastaTc = new SelectList(db.Hastalar, "hastaTc", "adSoyad", acilDurumTb.HastaTbhastaTc);
            return View(acilDurumTb);
        }

        // POST: YakinAcilDurum/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,aKonumX,aKonumY,aSicaklik,aNabiz,olcumZamani,HastaTbhastaTc")] AcilDurumTb acilDurumTb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(acilDurumTb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HastaTbhastaTc = new SelectList(db.Hastalar, "hastaTc", "adSoyad", acilDurumTb.HastaTbhastaTc);
            return View(acilDurumTb);
        }

        // GET: YakinAcilDurum/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcilDurumTb acilDurumTb = db.AcilDurumlar.Find(id);
            if (acilDurumTb == null)
            {
                return HttpNotFound();
            }
            return View(acilDurumTb);
        }

        // POST: YakinAcilDurum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AcilDurumTb acilDurumTb = db.AcilDurumlar.Find(id);
            db.AcilDurumlar.Remove(acilDurumTb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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
           
            var yakin = db.Yakinlar.ToList();
           
            foreach (var item in yakin)
            {
                if (item.kullaniciAdi == dt.kullaniciAdi && item.sifre == dt.sifre)
                {
                    Session["yakinTc"] = item.yakinTc;
                    
                    return RedirectToAction("Index", "YakinAcilDurum");                  
                }

            }
            ViewBag.mesaj = "hatalı";
            return View();
        }
    }
}
