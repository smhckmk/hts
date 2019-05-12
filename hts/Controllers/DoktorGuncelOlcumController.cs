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
    public class DoktorGuncelOlcumController : Controller
    {
        private htsContext db = new htsContext();

        // GET: DoktorGuncelOlcum
        public ActionResult Index()
        {
            var guncelOlcumler = db.GuncelOlcumler.Include(g => g.hastaTb);
            return View(guncelOlcumler.ToList());
        }

        // GET: DoktorGuncelOlcum/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuncelOlcumTb guncelOlcumTb = db.GuncelOlcumler.Find(id);
            if (guncelOlcumTb == null)
            {
                return HttpNotFound();
            }
            return View(guncelOlcumTb);
        }

        // GET: DoktorGuncelOlcum/Create
        public ActionResult Create()
        {
            ViewBag.HastaTbhastaTc = new SelectList(db.Hastalar, "hastaTc", "adSoyad");
            return View();
        }

        // POST: DoktorGuncelOlcum/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,sonOlcumTarihi,guncelKonumX,guncelKonumY,guncelSicaklik,guncelNabiz,HastaTbhastaTc")] GuncelOlcumTb guncelOlcumTb)
        {
            if (ModelState.IsValid)
            {
                db.GuncelOlcumler.Add(guncelOlcumTb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HastaTbhastaTc = new SelectList(db.Hastalar, "hastaTc", "adSoyad", guncelOlcumTb.HastaTbhastaTc);
            return View(guncelOlcumTb);
        }

        // GET: DoktorGuncelOlcum/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuncelOlcumTb guncelOlcumTb = db.GuncelOlcumler.Find(id);
            if (guncelOlcumTb == null)
            {
                return HttpNotFound();
            }
            ViewBag.HastaTbhastaTc = new SelectList(db.Hastalar, "hastaTc", "adSoyad", guncelOlcumTb.HastaTbhastaTc);
            return View(guncelOlcumTb);
        }

        // POST: DoktorGuncelOlcum/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,sonOlcumTarihi,guncelKonumX,guncelKonumY,guncelSicaklik,guncelNabiz,HastaTbhastaTc")] GuncelOlcumTb guncelOlcumTb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guncelOlcumTb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HastaTbhastaTc = new SelectList(db.Hastalar, "hastaTc", "adSoyad", guncelOlcumTb.HastaTbhastaTc);
            return View(guncelOlcumTb);
        }

        // GET: DoktorGuncelOlcum/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuncelOlcumTb guncelOlcumTb = db.GuncelOlcumler.Find(id);
            if (guncelOlcumTb == null)
            {
                return HttpNotFound();
            }
            return View(guncelOlcumTb);
        }

        // POST: DoktorGuncelOlcum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GuncelOlcumTb guncelOlcumTb = db.GuncelOlcumler.Find(id);
            db.GuncelOlcumler.Remove(guncelOlcumTb);
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
