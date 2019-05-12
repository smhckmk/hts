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
    public class KurumDoktorController : Controller
    {
        private htsContext db = new htsContext();

        // GET: KurumDoktor
        public ActionResult Index()
        {
            var doktorlar = db.Doktorlar.Include(d => d.uzmanlikTb);
            return View(doktorlar.ToList());
        }

        // GET: KurumDoktor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoktorTb doktorTb = db.Doktorlar.Find(id);
            if (doktorTb == null)
            {
                return HttpNotFound();
            }
            return View(doktorTb);
        }

        // GET: KurumDoktor/Create
        public ActionResult Create()
        {
            ViewBag.UzmanlikTbId = new SelectList(db.Uzmanlar, "Id", "uzmanlikAdi");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "doktorTc,adSoyad,telefon,adres,maas,toplamBileklikSayisi,kontrolundekiBileklikSayisi,UzmanlikTbId")] DoktorTb doktorTb)
        {
            if (ModelState.IsValid)
            {
                db.Doktorlar.Add(doktorTb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UzmanlikTbId = new SelectList(db.Uzmanlar, "Id", "uzmanlikAdi", doktorTb.UzmanlikTbId);
            return View(doktorTb);
        }

        // GET: KurumDoktor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoktorTb doktorTb = db.Doktorlar.Find(id);
            if (doktorTb == null)
            {
                return HttpNotFound();
            }
            ViewBag.UzmanlikTbId = new SelectList(db.Uzmanlar, "Id", "uzmanlikAdi", doktorTb.UzmanlikTbId);
            return View(doktorTb);
        }

        // POST: KurumDoktor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "doktorTc,adSoyad,maas")] DoktorTb doktorTb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doktorTb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UzmanlikTbId = new SelectList(db.Uzmanlar, "Id", "uzmanlikAdi", doktorTb.UzmanlikTbId);
            return View(doktorTb);
        }

        // GET: KurumDoktor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoktorTb doktorTb = db.Doktorlar.Find(id);
            if (doktorTb == null)
            {
                return HttpNotFound();
            }
            return View(doktorTb);
        }

        // POST: KurumDoktor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DoktorTb doktorTb = db.Doktorlar.Find(id);
            db.Doktorlar.Remove(doktorTb);
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
