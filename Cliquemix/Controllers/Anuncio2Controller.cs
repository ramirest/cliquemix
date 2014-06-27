using System;
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
    public class Anuncio2Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Anuncio2
        public ActionResult Index()
        {
            var tbAnuncio = db.tbAnuncios.Include(t => t.tbRamoAtividade);
            return View(tbAnuncio.ToList());
        }

        // GET: Anuncio2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnuncio tbAnuncio = db.tbAnuncios.Find(id);
            if (tbAnuncio == null)
            {
                return HttpNotFound();
            }
            return View(tbAnuncio);
        }

        // GET: Anuncio2/Create
        public ActionResult Create()
        {
            ViewBag.raid = new SelectList(db.tbRamoAtividade, "raid", "descricao");
            return View();
        }

        // POST: Anuncio2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "aid,tituloAnuncio,url,dsAnuncio,videoAnuncio")] tbAnuncio tbAnuncio)
        {
            if (ModelState.IsValid)
            {
                db.tbAnuncios.Add(tbAnuncio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.raid = new SelectList(db.tbRamoAtividade, "raid", "descricao", tbAnuncio.raid);
            return View(tbAnuncio);
        }

        // GET: Anuncio2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnuncio tbAnuncio = db.tbAnuncios.Find(id);
            if (tbAnuncio == null)
            {
                return HttpNotFound();
            }
            ViewBag.raid = new SelectList(db.tbRamoAtividade, "raid", "descricao", tbAnuncio.raid);
            return View(tbAnuncio);
        }

        // POST: Anuncio2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "aid,tituloAnuncio,url,dsAnuncio,videoAnuncio,raid,comentar,curtir,compartilhar,dtCriacao,statusAnuncio")] tbAnuncio tbAnuncio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbAnuncio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.raid = new SelectList(db.tbRamoAtividade, "raid", "descricao", tbAnuncio.raid);
            return View(tbAnuncio);
        }

        // GET: Anuncio2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnuncio tbAnuncio = db.tbAnuncios.Find(id);
            if (tbAnuncio == null)
            {
                return HttpNotFound();
            }
            return View(tbAnuncio);
        }

        // POST: Anuncio2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbAnuncio tbAnuncio = db.tbAnuncios.Find(id);
            db.tbAnuncios.Remove(tbAnuncio);
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
