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
   
    public class DoktorBileklikController : Controller
    {
        private htsContext db = new htsContext();

        // GET: DoktorBileklik
        public ActionResult Index()
        {
            var bileklikler = db.Bileklikler.Include(b => b.hastaTb);
            return View(bileklikler.ToList());
        }


        // GET: DoktorBileklik/Edit/5
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

        // POST: DoktorBileklik/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,verilebilmeDurumu,HastaTbhastaTc")] BileklikTb bileklikTb)
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
