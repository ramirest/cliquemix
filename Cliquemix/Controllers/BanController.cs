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
    public class BanController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Ban/
        public ActionResult Index()
        {
            var tbban = db.tbBans.Include(t => t.tbBanTipo).Include(t => t.tbUsers);
            return View(tbban.ToList());
        }

        // GET: /Ban/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBan tbban = db.tbBans.Find(id);
            if (tbban == null)
            {
                return HttpNotFound();
            }
            return View(tbban);
        }

        // GET: /Ban/Create
        public ActionResult Create()
        {
            ViewBag.btid = new SelectList(db.tbBanTipo, "btid", "tituloBan");
            ViewBag.uid = new SelectList(db.tbUsers, "uid", "username");
            return View();
        }

        // POST: /Ban/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="baid,btid,uid,dtOcorrido")] tbBan tbban)
        {
            if (ModelState.IsValid)
            {
                db.tbBans.Add(tbban);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.btid = new SelectList(db.tbBanTipo, "btid", "tituloBan", tbban.btid);
            ViewBag.uid = new SelectList(db.tbUsers, "uid", "username", tbban.uid);
            return View(tbban);
        }

        // GET: /Ban/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBan tbban = db.tbBans.Find(id);
            if (tbban == null)
            {
                return HttpNotFound();
            }
            ViewBag.btid = new SelectList(db.tbBanTipo, "btid", "tituloBan", tbban.btid);
            ViewBag.uid = new SelectList(db.tbUsers, "uid", "username", tbban.uid);
            return View(tbban);
        }

        // POST: /Ban/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="baid,btid,uid,dtOcorrido")] tbBan tbban)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbban).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.btid = new SelectList(db.tbBanTipo, "btid", "tituloBan", tbban.btid);
            ViewBag.uid = new SelectList(db.tbUsers, "uid", "username", tbban.uid);
            return View(tbban);
        }

        // GET: /Ban/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBan tbban = db.tbBans.Find(id);
            if (tbban == null)
            {
                return HttpNotFound();
            }
            return View(tbban);
        }

        // POST: /Ban/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbBan tbban = db.tbBans.Find(id);
            db.tbBans.Remove(tbban);
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
