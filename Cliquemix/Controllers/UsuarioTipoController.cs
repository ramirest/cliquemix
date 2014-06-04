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
    public class UsuarioTipoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /UsuarioTipo/
        public ActionResult Index()
        {
            return View(db.tbUsersTipoes.ToList());
        }

        // GET: /UsuarioTipo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsersTipo tbuserstipo = db.tbUsersTipoes.Find(id);
            if (tbuserstipo == null)
            {
                return HttpNotFound();
            }
            return View(tbuserstipo);
        }

        // GET: /UsuarioTipo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /UsuarioTipo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="utid,dsUsersTipo")] tbUsersTipo tbuserstipo)
        {
            if (ModelState.IsValid)
            {
                db.tbUsersTipoes.Add(tbuserstipo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbuserstipo);
        }

        // GET: /UsuarioTipo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsersTipo tbuserstipo = db.tbUsersTipoes.Find(id);
            if (tbuserstipo == null)
            {
                return HttpNotFound();
            }
            return View(tbuserstipo);
        }

        // POST: /UsuarioTipo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="utid,dsUsersTipo")] tbUsersTipo tbuserstipo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbuserstipo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbuserstipo);
        }

        // GET: /UsuarioTipo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsersTipo tbuserstipo = db.tbUsersTipoes.Find(id);
            if (tbuserstipo == null)
            {
                return HttpNotFound();
            }
            return View(tbuserstipo);
        }

        // POST: /UsuarioTipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbUsersTipo tbuserstipo = db.tbUsersTipoes.Find(id);
            db.tbUsersTipoes.Remove(tbuserstipo);
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
