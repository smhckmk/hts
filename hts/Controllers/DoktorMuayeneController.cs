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
    public class DoktorMuayeneController : Controller
    {
        private htsContext db = new htsContext();

        // GET: DoktorMuayene
        public ActionResult Index()
        {
            var muayeneler = db.Muayeneler.Include(m => m.hastaTb);
            return View(muayeneler.ToList());
        }

        // GET: DoktorMuayene/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MuayeneTb muayeneTb = db.Muayeneler.Find(id);
            if (muayeneTb == null)
            {
                return HttpNotFound();
            }
            return View(muayeneTb);
        }

        // GET: DoktorMuayene/Create
        public ActionResult Create()
        {
            ViewBag.HastaTbhastaTc = new SelectList(db.Hastalar, "hastaTc", "hastaTc");
            return View();
        }

        // POST: DoktorMuayene/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,konumMaxX,konumMaxY,nabizMax,nabizMin,sicaklikOlcumSikligi,nabizOlcumSikligi,konumOlcumSikligi,bildirimSikligi,HastaTbhastaTc")] MuayeneTb muayeneTb)
        {
            if (ModelState.IsValid)
            {
                db.Muayeneler.Add(muayeneTb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HastaTbhastaTc = new SelectList(db.Hastalar, "hastaTc", "adSoyad", muayeneTb.HastaTbhastaTc);
            return View(muayeneTb);
        }

        // GET: DoktorMuayene/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MuayeneTb muayeneTb = db.Muayeneler.Find(id);
            if (muayeneTb == null)
            {
                return HttpNotFound();
            }
            ViewBag.HastaTbhastaTc = new SelectList(db.Hastalar, "hastaTc", "hastaTc", muayeneTb.HastaTbhastaTc);
            return View(muayeneTb);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,konumMaxX,konumMaxY,nabizMax,nabizMin,sicaklikOlcumSikligi,nabizOlcumSikligi,konumOlcumSikligi,bildirimSikligi,HastaTbhastaTc")] MuayeneTb muayeneTb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(muayeneTb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HastaTbhastaTc = new SelectList(db.Hastalar, "hastaTc", "adSoyad", muayeneTb.HastaTbhastaTc);
            return View(muayeneTb);
        }

        // GET: DoktorMuayene/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MuayeneTb muayeneTb = db.Muayeneler.Find(id);
            if (muayeneTb == null)
            {
                return HttpNotFound();
            }
            return View(muayeneTb);
        }

        // POST: DoktorMuayene/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MuayeneTb muayeneTb = db.Muayeneler.Find(id);
            db.Muayeneler.Remove(muayeneTb);
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
