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
    public class UsuarioController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Usuario/
        public ActionResult Index()
        {
            var tbusers = db.tbUsers.Include(t => t.tbUsersTipo);
            return View(tbusers.ToList());
        }

        // GET: /Usuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsers tbusers = db.tbUsers.Find(id);
            if (tbusers == null)
            {
                return HttpNotFound();
            }
            return View(tbusers);
        }

        // GET: /Usuario/Create
        public ActionResult Create()
        {
            ViewBag.utid = new SelectList(db.tbUsersTipoes, "utid", "dsUsersTipo");
            return View();
        }

        // POST: /Usuario/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="uid,username,pwd,utid")] tbUsers tbusers)
        {
            if (ModelState.IsValid)
            {
                db.tbUsers.Add(tbusers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.utid = new SelectList(db.tbUsersTipoes, "utid", "dsUsersTipo", tbusers.utid);
            return View(tbusers);
        }

        // GET: /Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsers tbusers = db.tbUsers.Find(id);
            if (tbusers == null)
            {
                return HttpNotFound();
            }
            ViewBag.utid = new SelectList(db.tbUsersTipoes, "utid", "dsUsersTipo", tbusers.utid);
            return View(tbusers);
        }

        // POST: /Usuario/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="uid,username,pwd,utid")] tbUsers tbusers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbusers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.utid = new SelectList(db.tbUsersTipoes, "utid", "dsUsersTipo", tbusers.utid);
            return View(tbusers);
        }

        // GET: /Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbUsers tbusers = db.tbUsers.Find(id);
            if (tbusers == null)
            {
                return HttpNotFound();
            }
            return View(tbusers);
        }

        // POST: /Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbUsers tbusers = db.tbUsers.Find(id);
            db.tbUsers.Remove(tbusers);
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
