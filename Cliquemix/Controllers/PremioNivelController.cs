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
    public class PremioNivelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /PremioNivel/
        public ActionResult Index()
        {
            return View(db.tbPremioNivels.ToList());
        }

        // GET: /PremioNivel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPremioNivel tbpremionivel = db.tbPremioNivels.Find(id);
            if (tbpremionivel == null)
            {
                return HttpNotFound();
            }
            return View(tbpremionivel);
        }

        // GET: /PremioNivel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /PremioNivel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="pnid,tituloNivelPremio,dsPremio,premioImg")] tbPremioNivel tbpremionivel)
        {
            if (ModelState.IsValid)
            {
                db.tbPremioNivels.Add(tbpremionivel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbpremionivel);
        }

        // GET: /PremioNivel/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPremioNivel tbpremionivel = db.tbPremioNivels.Find(id);
            if (tbpremionivel == null)
            {
                return HttpNotFound();
            }
            return View(tbpremionivel);
        }

        // POST: /PremioNivel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="pnid,tituloNivelPremio,dsPremio,premioImg")] tbPremioNivel tbpremionivel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbpremionivel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbpremionivel);
        }

        // GET: /PremioNivel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbPremioNivel tbpremionivel = db.tbPremioNivels.Find(id);
            if (tbpremionivel == null)
            {
                return HttpNotFound();
            }
            return View(tbpremionivel);
        }

        // POST: /PremioNivel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbPremioNivel tbpremionivel = db.tbPremioNivels.Find(id);
            db.tbPremioNivels.Remove(tbpremionivel);
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
