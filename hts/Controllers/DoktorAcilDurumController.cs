using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using hts.Entity;

namespace hts.Controllers
{
    public class DoktorAcilDurumController : Controller
    {
        private htsContext db = new htsContext();

        // GET: DoktorAcilDurum
        public ActionResult Index()
        {
            var acilDurumlar = db.AcilDurumlar.Include(a => a.hastaTb);
            return View(acilDurumlar.ToList());
        }

      
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

        // GET: DoktorAcilDurum/Create
        public ActionResult Create()
        {
            ViewBag.HastaTbhastaTc = new SelectList(db.Hastalar, "hastaTc", "adSoyad");
            return View();
        }

        // POST: DoktorAcilDurum/Create
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

        // GET: DoktorAcilDurum/Edit/5
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

        // POST: DoktorAcilDurum/Edit/5
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

        // GET: DoktorAcilDurum/Delete/5
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

        // POST: DoktorAcilDurum/Delete/5
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
       
    }
}
