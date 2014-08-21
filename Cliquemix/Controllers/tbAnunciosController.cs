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
    public class tbAnunciosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: tbAnuncios
        public ActionResult Index()
        {
            var tbAnuncio = db.tbAnuncio.Include(t => t.tbAnunciante).Include(t => t.tbAnuncioCategoria).Include(t => t.tbAnuncioCodTemp).Include(t => t.tbAnuncioStatus);
            return View(tbAnuncio.ToList());
        }

        // GET: tbAnuncios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnuncio tbAnuncio = db.tbAnuncio.Find(id);
            if (tbAnuncio == null)
            {
                return HttpNotFound();
            }
            return View(tbAnuncio);
        }

        // GET: tbAnuncios/Create
        public ActionResult Create()
        {
            ViewBag.pid = new SelectList(db.tbAnunciante, "pid", "cnpj");
            ViewBag.acid = new SelectList(db.tbAnuncioCategoria, "acid", "dsCategoria");
            ViewBag.actid = new SelectList(db.tbAnuncioCodTemp, "actid", "actid");
            ViewBag.asid = new SelectList(db.tbAnuncioStatus, "asid", "dsStatus");
            return View();
        }

        // POST: tbAnuncios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "aid,tituloAnuncio,url,dsAnuncio,videoAnuncio,comentar,curtir,compartilhar,asid,dtCriacao,pid,acid,actid")] tbAnuncio tbAnuncio)
        {
            if (ModelState.IsValid)
            {
                db.tbAnuncio.Add(tbAnuncio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pid = new SelectList(db.tbAnunciante, "pid", "cnpj", tbAnuncio.pid);
            ViewBag.acid = new SelectList(db.tbAnuncioCategoria, "acid", "dsCategoria", tbAnuncio.acid);
            ViewBag.actid = new SelectList(db.tbAnuncioCodTemp, "actid", "actid", tbAnuncio.actid);
            ViewBag.asid = new SelectList(db.tbAnuncioStatus, "asid", "dsStatus", tbAnuncio.asid);
            return View(tbAnuncio);
        }

        // GET: tbAnuncios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnuncio tbAnuncio = db.tbAnuncio.Find(id);
            if (tbAnuncio == null)
            {
                return HttpNotFound();
            }
            ViewBag.pid = new SelectList(db.tbAnunciante, "pid", "cnpj", tbAnuncio.pid);
            ViewBag.acid = new SelectList(db.tbAnuncioCategoria, "acid", "dsCategoria", tbAnuncio.acid);
            ViewBag.actid = new SelectList(db.tbAnuncioCodTemp, "actid", "actid", tbAnuncio.actid);
            ViewBag.asid = new SelectList(db.tbAnuncioStatus, "asid", "dsStatus", tbAnuncio.asid);
            return View(tbAnuncio);
        }

        // POST: tbAnuncios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "aid,tituloAnuncio,url,dsAnuncio,videoAnuncio,comentar,curtir,compartilhar,asid,dtCriacao,pid,acid,actid")] tbAnuncio tbAnuncio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbAnuncio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pid = new SelectList(db.tbAnunciante, "pid", "cnpj", tbAnuncio.pid);
            ViewBag.acid = new SelectList(db.tbAnuncioCategoria, "acid", "dsCategoria", tbAnuncio.acid);
            ViewBag.actid = new SelectList(db.tbAnuncioCodTemp, "actid", "actid", tbAnuncio.actid);
            ViewBag.asid = new SelectList(db.tbAnuncioStatus, "asid", "dsStatus", tbAnuncio.asid);
            return View(tbAnuncio);
        }

        // GET: tbAnuncios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnuncio tbAnuncio = db.tbAnuncio.Find(id);
            if (tbAnuncio == null)
            {
                return HttpNotFound();
            }
            return View(tbAnuncio);
        }

        // POST: tbAnuncios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbAnuncio tbAnuncio = db.tbAnuncio.Find(id);
            db.tbAnuncio.Remove(tbAnuncio);
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
