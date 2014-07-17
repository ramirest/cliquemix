using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Cliquemix.Models;

namespace Cliquemix.Controllers
{
    [Authorize]
    public class TipoUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: tipoUsers
        public ActionResult Index()
        {
            return View(db.tbUsersTipo.ToList());
        }

        // GET: tipoUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsersTipo tbUsersTipo = db.tbUsersTipo.Find(id);
            if (tbUsersTipo == null)
            {
                return HttpNotFound();
            }
            return View(tbUsersTipo);
        }

        // GET: tipoUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tipoUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "utid,dsUsersTipo")] tbUsersTipo tbUsersTipo)
        {
            if (ModelState.IsValid)
            {
                db.tbUsersTipo.Add(tbUsersTipo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbUsersTipo);
        }

        // GET: tipoUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsersTipo tbUsersTipo = db.tbUsersTipo.Find(id);
            if (tbUsersTipo == null)
            {
                return HttpNotFound();
            }
            return View(tbUsersTipo);
        }

        // POST: tipoUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "utid,dsUsersTipo")] tbUsersTipo tbUsersTipo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbUsersTipo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbUsersTipo);
        }

        // GET: tipoUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsersTipo tbUsersTipo = db.tbUsersTipo.Find(id);
            if (tbUsersTipo == null)
            {
                return HttpNotFound();
            }
            return View(tbUsersTipo);
        }

        // POST: tipoUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbUsersTipo tbUsersTipo = db.tbUsersTipo.Find(id);
            db.tbUsersTipo.Remove(tbUsersTipo);
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
