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
    public class HastaneHastaController : Controller
    {
        private htsContext db = new htsContext();

        // GET: HastaneHasta
        public ActionResult Index()
        {
            var hastalar = db.Hastalar.Include(h => h.doktorTb);
            return View(hastalar.ToList());
        }

        // GET: HastaneHasta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HastaTb hastaTb = db.Hastalar.Find(id);
            if (hastaTb == null)
            {
                return HttpNotFound();
            }
            return View(hastaTb);
        }

        // GET: HastaneHasta/Create
        public ActionResult Create()
        {
            ViewBag.DoktorTbdoktorTc = new SelectList(db.Doktorlar, "doktorTc", "adSoyad");
            return View();
        }

        // POST: HastaneHasta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "hastaTc,adSoyad,telefon,adres,hastaligi,DoktorTbdoktorTc")] HastaTb hastaTb)
        {
            if (ModelState.IsValid)
            {
                db.Hastalar.Add(hastaTb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DoktorTbdoktorTc = new SelectList(db.Doktorlar, "doktorTc", "adSoyad", hastaTb.DoktorTbdoktorTc);
            return View(hastaTb);
        }

        // GET: HastaneHasta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HastaTb hastaTb = db.Hastalar.Find(id);
            if (hastaTb == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoktorTbdoktorTc = new SelectList(db.Doktorlar, "doktorTc", "adSoyad", hastaTb.DoktorTbdoktorTc);
            return View(hastaTb);
        }

        // POST: HastaneHasta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "hastaTc,adSoyad,telefon,adres,hastaligi,DoktorTbdoktorTc")] HastaTb hastaTb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hastaTb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoktorTbdoktorTc = new SelectList(db.Doktorlar, "doktorTc", "adSoyad", hastaTb.DoktorTbdoktorTc);
            return View(hastaTb);
        }

        // GET: HastaneHasta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HastaTb hastaTb = db.Hastalar.Find(id);
            if (hastaTb == null)
            {
                return HttpNotFound();
            }
            return View(hastaTb);
        }

        // POST: HastaneHasta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HastaTb hastaTb = db.Hastalar.Find(id);
            db.Hastalar.Remove(hastaTb);
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
