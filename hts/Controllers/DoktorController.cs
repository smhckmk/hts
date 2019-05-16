﻿using hts.Entity;
using hts.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace hts.Controllers
{
    public class DoktorController : Controller
    {
        htsContext dbContext = new htsContext();


        public ActionResult Anasayfa() //acil durum gosteriliyor anasayfa da
        {
            int id = 0;
            id = Convert.ToInt32(Session["doktorTc"]);
            var doktor = dbContext.Doktorlar.Find(id);
            ViewBag.adSoyad = doktor.adSoyad;

            ViewBag.id = id;
            DoktorHastaAcilDurum doktorHastaAcilDurum = new DoktorHastaAcilDurum();

            doktorHastaAcilDurum.doktorlar = dbContext.Doktorlar.Where(x => x.doktorTc == id).ToList();
            doktorHastaAcilDurum.acilDurumlar = dbContext.AcilDurumlar.ToList();
            doktorHastaAcilDurum.hastalar = dbContext.Hastalar.ToList();


            return View(doktorHastaAcilDurum);
        }

        //-------------------Doktor Bileklik İşlemleri

        public ActionResult Bileklik()
        {
            DoktorBileklikHasta doktorBilekHasta = new DoktorBileklikHasta();

            int id = 0;
            id = Convert.ToInt32(Session["doktorTc"]);
            doktorBilekHasta.doktorlar = dbContext.Doktorlar.Where(x => x.doktorTc == id).ToList();
            doktorBilekHasta.bileklikler = dbContext.Bileklikler.ToList();
            doktorBilekHasta.hastalar = dbContext.Hastalar.ToList();

            return View(doktorBilekHasta);
        }


        public ActionResult BileklikDuzenle(int? id, int? did)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BileklikTb bileklikTb = dbContext.Bileklikler.Find(id);
            if (bileklikTb == null)
            {
                return HttpNotFound();
            }
            ViewBag.HastaTbhastaTc = new SelectList(dbContext.Hastalar.Where(i => i.DoktorTbdoktorTc == did), "hastaTc", "adSoyad", bileklikTb.HastaTbhastaTc);
            return View(bileklikTb);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BileklikDuzenle([Bind(Include = "Id,verilebilmeDurumu,HastaTbhastaTc")] BileklikTb bileklikTb, int? did)
        {
            if (ModelState.IsValid)
            {
                dbContext.Entry(bileklikTb).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("Bileklik");
            }
            ViewBag.HastaTbhastaTc = new SelectList(dbContext.Hastalar.Where(i => i.DoktorTbdoktorTc == did), "hastaTc", "adSoyad", bileklikTb.HastaTbhastaTc);
            return View(bileklikTb);
        }

        //-------------------Doktor Guncel Durum -------------------------

        public ActionResult GuncelDurum()
        {
            int id = 0;
            id = Convert.ToInt32(Session["doktorTc"]);
            var doktor = dbContext.Doktorlar.Find(id);
            ViewBag.adSoyad = doktor.adSoyad;

            DoktorHastaGuncelDurum doktorHastaGuncelDurum = new DoktorHastaGuncelDurum();

            doktorHastaGuncelDurum.doktorlar = dbContext.Doktorlar.Where(x => x.doktorTc == id).ToList();
            doktorHastaGuncelDurum.guncelOlcumler = dbContext.GuncelOlcumler.ToList();
            doktorHastaGuncelDurum.hastalar = dbContext.Hastalar.ToList();


            return View(doktorHastaGuncelDurum);
        }

        //--------------Hasta Muayene İşlemleri-------------------------------

        public ActionResult HastaMuayene() //ındex
        {

            int id = 0;
            id = Convert.ToInt32(Session["doktorTc"]);
            var doktor = dbContext.Doktorlar.Find(id);
            ViewBag.adSoyad = doktor.adSoyad;

            DoktorHastaMuayene doktorHastaMuayene = new DoktorHastaMuayene();

            doktorHastaMuayene.doktorlar = dbContext.Doktorlar.Where(x => x.doktorTc == id).ToList();
            doktorHastaMuayene.muayeneler = dbContext.Muayeneler.ToList();
            doktorHastaMuayene.hastalar = dbContext.Hastalar.ToList();


            return View(doktorHastaMuayene);
        }


        public ActionResult HastaMuayeneOlusturma()
        {
            int did = 0;
            did = Convert.ToInt32(Session["doktorTc"]);


            ViewBag.HastaTbhastaTc = new SelectList(dbContext.Hastalar.Where(i => i.DoktorTbdoktorTc == did), "hastaTc", "hastaTc");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HastaMuayeneOlusturma([Bind(Include = "Id,konumMaxX,konumMaxY,nabizMax,nabizMin,HastaTbhastaTc")] MuayeneTb muayeneTb, int? did)
        {
            if (ModelState.IsValid)
            {
                dbContext.Muayeneler.Add(muayeneTb);
                dbContext.SaveChanges();
                return RedirectToAction("HastaMuayene");
            }

            ViewBag.HastaTbhastaTc = new SelectList(dbContext.Hastalar.Where(i => i.DoktorTbdoktorTc == did), "hastaTc", "adSoyad", muayeneTb.HastaTbhastaTc);
            return View(muayeneTb);
        }

        public ActionResult HastaMuayeneDuzenleme(int? id, int? did)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MuayeneTb muayeneTb = dbContext.Muayeneler.Find(id);
            if (muayeneTb == null)
            {
                return HttpNotFound();
            }
            ViewBag.HastaTbhastaTc = new SelectList(dbContext.Hastalar.Where(i => i.DoktorTbdoktorTc == did), "hastaTc", "hastaTc", muayeneTb.HastaTbhastaTc);
            return View(muayeneTb);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HastaMuayeneDuzenleme([Bind(Include = "Id,konumMaxX,konumMaxY,nabizMax,nabizMin,HastaTbhastaTc")] MuayeneTb muayeneTb, int? did)
        {
            if (ModelState.IsValid)
            {
                dbContext.Entry(muayeneTb).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("HastaMuayene");
            }
            ViewBag.HastaTbhastaTc = new SelectList(dbContext.Hastalar.Where(i => i.DoktorTbdoktorTc == did), "hastaTc", "adSoyad", muayeneTb.HastaTbhastaTc);
            return View(muayeneTb);
        }


        public ActionResult HastaMuayeneSilme(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MuayeneTb muayeneTb = dbContext.Muayeneler.Find(id);
            if (muayeneTb == null)
            {
                return HttpNotFound();
            }
            return View(muayeneTb);
        }


        [HttpPost, ActionName("HastaMuayeneSilme")]
        [ValidateAntiForgeryToken]
        public ActionResult HastaMuayeneSilmeOnay(int id)
        {
            MuayeneTb muayeneTb = dbContext.Muayeneler.Find(id);
            dbContext.Muayeneler.Remove(muayeneTb);
            dbContext.SaveChanges();
            return RedirectToAction("HastaMuayene");
        }



        //-----------------------Doktor Giris---------------------
        public ActionResult DoktorGiris()
        {
            if (String.IsNullOrEmpty(HttpContext.User.Identity.Name))
            {
                FormsAuthentication.SignOut();
                return View();
            }
            return View();
        }

        [HttpPost]
        public ActionResult DoktorGiris(hts.Entity.DoktorTb dt)
        {

            var doktorlar = dbContext.Doktorlar.ToList();

            foreach (var doktor in doktorlar)
            {
                if (doktor.kullaniciAdi == dt.kullaniciAdi && doktor.sifre == dt.sifre)
                {
                    Session["doktorTc"] = doktor.doktorTc;

                    return RedirectToAction("Anasayfa", "Doktor");
                }
                if (doktor.kullaniciAdi == dt.kullaniciAdi && doktor.sifre != dt.sifre)
                {
                    ViewBag.mesaj = "sifre hatalı";
                    return View();
                }

                if (doktor.kullaniciAdi != dt.kullaniciAdi && doktor.sifre == dt.sifre)
                {
                    ViewBag.mesaj = "kullanıcı adı hatalı";
                    return View();
                }
                if (doktor.kullaniciAdi != dt.kullaniciAdi && doktor.sifre != dt.sifre)
                {
                    ViewBag.mesaj = "kullanıcı ve sifre hatalı";
                    return View();
                }

            }
          
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}