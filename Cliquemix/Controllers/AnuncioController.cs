﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cliquemix.Models;

namespace Cliquemix.Controllers
{
    public class AnuncioController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Anuncio/
        public ActionResult Index()
        {
            var tbanuncio = db.tbAnuncios.Include(t => t.tbAnuncioArea);
            return View(tbanuncio.ToList());
        }

        // GET: /Anuncio/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnuncio tbanuncio = db.tbAnuncios.Find(id);
            if (tbanuncio == null)
            {
                return HttpNotFound();
            }
            return View(tbanuncio);
        }

        // GET: /Anuncio/Create
        public ActionResult Create()
        {
            ViewBag.aaid = new SelectList(db.tbAnuncioArea, "aaid", "tituloAnuncioArea");
            return View();
        }

        // POST: /Anuncio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="aid,tituloAnuncio,url,dsAnuncio,videoAnuncio,aaid")] tbAnuncio tbanuncio)
        {
            if (ModelState.IsValid)
            {
                db.tbAnuncios.Add(tbanuncio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.aaid = new SelectList(db.tbAnuncioArea, "aaid", "tituloAnuncioArea", tbanuncio.aaid);
            return View(tbanuncio);
        }

        // GET: /Anuncio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnuncio tbanuncio = db.tbAnuncios.Find(id);
            if (tbanuncio == null)
            {
                return HttpNotFound();
            }
            ViewBag.aaid = new SelectList(db.tbAnuncioArea, "aaid", "tituloAnuncioArea", tbanuncio.aaid);
            return View(tbanuncio);
        }

        // POST: /Anuncio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="aid,tituloAnuncio,url,dsAnuncio,videoAnuncio,aaid")] tbAnuncio tbanuncio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbanuncio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.aaid = new SelectList(db.tbAnuncioArea, "aaid", "tituloAnuncioArea", tbanuncio.aaid);
            return View(tbanuncio);
        }

        // GET: /Anuncio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnuncio tbanuncio = db.tbAnuncios.Find(id);
            if (tbanuncio == null)
            {
                return HttpNotFound();
            }
            return View(tbanuncio);
        }

        // POST: /Anuncio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbAnuncio tbanuncio = db.tbAnuncios.Find(id);
            db.tbAnuncios.Remove(tbanuncio);
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
