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
    public class KurumHastaneController : Controller
    {
        private htsContext db = new htsContext();

        // GET: KurumHastane
        public ActionResult Index()
        {
            var hastaneler = db.Hastaneler.Include(h => h.kurumTb);
            return View(hastaneler.ToList());
        }

        // GET: KurumHastane/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HastaneTb hastaneTb = db.Hastaneler.Find(id);
            if (hastaneTb == null)
            {
                return HttpNotFound();
            }
            return View(hastaneTb);
        }

        // GET: KurumHastane/Create
        public ActionResult Create()
        {
            ViewBag.KurumTbkurumId = new SelectList(db.Kurumlar, "kurumId", "kurumAdi");
            return View();
        }

        // POST: KurumHastane/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "hastaneId,hastaneAdi,stoktakiBileklik,telefon,adres,KurumTbkurumId")] HastaneTb hastaneTb)
        {
            if (ModelState.IsValid)
            {
                db.Hastaneler.Add(hastaneTb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KurumTbkurumId = new SelectList(db.Kurumlar, "kurumId", "kurumAdi", hastaneTb.KurumTbkurumId);
            return View(hastaneTb);
        }

        // GET: KurumHastane/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HastaneTb hastaneTb = db.Hastaneler.Find(id);
            if (hastaneTb == null)
            {
                return HttpNotFound();
            }
            ViewBag.KurumTbkurumId = new SelectList(db.Kurumlar, "kurumId", "kurumAdi", hastaneTb.KurumTbkurumId);
            return View(hastaneTb);
        }

        // POST: KurumHastane/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "hastaneId,hastaneAdi,stoktakiBileklik,telefon,adres,KurumTbkurumId")] HastaneTb hastaneTb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hastaneTb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KurumTbkurumId = new SelectList(db.Kurumlar, "kurumId", "kurumAdi", hastaneTb.KurumTbkurumId);
            return View(hastaneTb);
        }

        // GET: KurumHastane/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HastaneTb hastaneTb = db.Hastaneler.Find(id);
            if (hastaneTb == null)
            {
                return HttpNotFound();
            }
            return View(hastaneTb);
        }

        // POST: KurumHastane/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HastaneTb hastaneTb = db.Hastaneler.Find(id);
            db.Hastaneler.Remove(hastaneTb);
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
