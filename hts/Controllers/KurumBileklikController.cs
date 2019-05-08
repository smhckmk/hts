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
    public class KurumBileklikController : Controller
    {
        private htsContext db = new htsContext();

        // GET: KurumBileklik
        public ActionResult Index()
        {
            var bileklikler = db.Bileklikler.Include(b => b.hastaTb);
            return View(bileklikler.ToList());
        }

        // GET: KurumBileklik/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BileklikTb bileklikTb = db.Bileklikler.Find(id);
            if (bileklikTb == null)
            {
                return HttpNotFound();
            }
            return View(bileklikTb);
        }

        // GET: KurumBileklik/Create
        public ActionResult Create()
        {
            ViewBag.HastaTbhastaTc = new SelectList(db.Hastalar, "hastaTc", "adSoyad");
            return View();
        }

        // POST: KurumBileklik/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,imalatTarihi,verilebilmeDurumu,HastaTbhastaTc")] BileklikTb bileklikTb)
        {
            if (ModelState.IsValid)
            {
                db.Bileklikler.Add(bileklikTb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HastaTbhastaTc = new SelectList(db.Hastalar, "hastaTc", "adSoyad", bileklikTb.HastaTbhastaTc);
            return View(bileklikTb);
        }

        // GET: KurumBileklik/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BileklikTb bileklikTb = db.Bileklikler.Find(id);
            if (bileklikTb == null)
            {
                return HttpNotFound();
            }
            ViewBag.HastaTbhastaTc = new SelectList(db.Hastalar, "hastaTc", "adSoyad", bileklikTb.HastaTbhastaTc);
            return View(bileklikTb);
        }

        // POST: KurumBileklik/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,imalatTarihi,verilebilmeDurumu,HastaTbhastaTc")] BileklikTb bileklikTb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bileklikTb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HastaTbhastaTc = new SelectList(db.Hastalar, "hastaTc", "adSoyad", bileklikTb.HastaTbhastaTc);
            return View(bileklikTb);
        }

        // GET: KurumBileklik/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BileklikTb bileklikTb = db.Bileklikler.Find(id);
            if (bileklikTb == null)
            {
                return HttpNotFound();
            }
            return View(bileklikTb);
        }

        // POST: KurumBileklik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BileklikTb bileklikTb = db.Bileklikler.Find(id);
            db.Bileklikler.Remove(bileklikTb);
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
