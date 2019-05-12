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
    public class HastaneHastaDurumController : Controller
    {
        private htsContext db = new htsContext();

        // GET: HastaneHastaDurum
        public ActionResult Index()
        {
            var hastaDurumlar = db.HastaDurumlar.Include(h => h.hastaTb);
            return View(hastaDurumlar.ToList());
        }

        // GET: HastaneHastaDurum/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HastaDurumTb hastaDurumTb = db.HastaDurumlar.Find(id);
            if (hastaDurumTb == null)
            {
                return HttpNotFound();
            }
            return View(hastaDurumTb);
        }

        // GET: HastaneHastaDurum/Create
        public ActionResult Create()
        {
            ViewBag.HastaTbhastaTc = new SelectList(db.Hastalar, "hastaTc", "adSoyad");
            return View();
        }

        // POST: HastaneHastaDurum/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,yas,kilo,boy,yagOrani,nabiz,konumY,konumX,alerjikDurumlar,ozelHastaliklar,kanSekeri,HastaTbhastaTc")] HastaDurumTb hastaDurumTb)
        {
            if (ModelState.IsValid)
            {
                db.HastaDurumlar.Add(hastaDurumTb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HastaTbhastaTc = new SelectList(db.Hastalar, "hastaTc", "adSoyad", hastaDurumTb.HastaTbhastaTc);
            return View(hastaDurumTb);
        }

        // GET: HastaneHastaDurum/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HastaDurumTb hastaDurumTb = db.HastaDurumlar.Find(id);
            if (hastaDurumTb == null)
            {
                return HttpNotFound();
            }
            ViewBag.HastaTbhastaTc = new SelectList(db.Hastalar, "hastaTc", "adSoyad", hastaDurumTb.HastaTbhastaTc);
            return View(hastaDurumTb);
        }

        // POST: HastaneHastaDurum/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,yas,kilo,boy,yagOrani,nabiz,konumY,konumX,alerjikDurumlar,ozelHastaliklar,kanSekeri,HastaTbhastaTc")] HastaDurumTb hastaDurumTb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hastaDurumTb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HastaTbhastaTc = new SelectList(db.Hastalar, "hastaTc", "adSoyad", hastaDurumTb.HastaTbhastaTc);
            return View(hastaDurumTb);
        }

        // GET: HastaneHastaDurum/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HastaDurumTb hastaDurumTb = db.HastaDurumlar.Find(id);
            if (hastaDurumTb == null)
            {
                return HttpNotFound();
            }
            return View(hastaDurumTb);
        }

        // POST: HastaneHastaDurum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HastaDurumTb hastaDurumTb = db.HastaDurumlar.Find(id);
            db.HastaDurumlar.Remove(hastaDurumTb);
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
