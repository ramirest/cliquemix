using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cliquemix.Models;

namespace Cliquemix.Controllers.Administrador
{
    public class PremioController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Premio/
        public ActionResult Index()
        {
            var tbpremio = db.tbPremios.Include(t => t.tbPremioNivel);
            return View(tbpremio.ToList());
        }

        // GET: /Premio/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPremio tbpremio = db.tbPremios.Find(id);
            if (tbpremio == null)
            {
                return HttpNotFound();
            }
            return View(tbpremio);
        }

        // GET: /Premio/Create
        public ActionResult Create()
        {
            ViewBag.pnid = new SelectList(db.tbPremioNivels, "pnid", "tituloNivelPremio");
            return View();
        }

        // POST: /Premio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="prid,pnid,tituloPremio,dsPremio,dtCriacao,ativo")] tbPremio tbpremio)
        {
            if (ModelState.IsValid)
            {
                db.tbPremios.Add(tbpremio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pnid = new SelectList(db.tbPremioNivels, "pnid", "tituloNivelPremio", tbpremio.pnid);
            return View(tbpremio);
        }

        // GET: /Premio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPremio tbpremio = db.tbPremios.Find(id);
            if (tbpremio == null)
            {
                return HttpNotFound();
            }
            ViewBag.pnid = new SelectList(db.tbPremioNivels, "pnid", "tituloNivelPremio", tbpremio.pnid);
            return View(tbpremio);
        }

        // POST: /Premio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="prid,pnid,tituloPremio,dsPremio,dtCriacao,ativo")] tbPremio tbpremio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbpremio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pnid = new SelectList(db.tbPremioNivels, "pnid", "tituloNivelPremio", tbpremio.pnid);
            return View(tbpremio);
        }

        // GET: /Premio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPremio tbpremio = db.tbPremios.Find(id);
            if (tbpremio == null)
            {
                return HttpNotFound();
            }
            return View(tbpremio);
        }

        // POST: /Premio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbPremio tbpremio = db.tbPremios.Find(id);
            db.tbPremios.Remove(tbpremio);
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
