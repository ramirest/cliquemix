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
    public class BannerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Banner/
        public ActionResult Index()
        {
            return View(db.tbBanner.ToList());
        }

        // GET: /Banner/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBanner tbbanner = db.tbBanner.Find(id);
            if (tbbanner == null)
            {
                return HttpNotFound();
            }
            return View(tbbanner);
        }

        // GET: /Banner/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Banner/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="bid,tituloBanner")] tbBanner tbbanner)
        {
            if (ModelState.IsValid)
            {
                db.tbBanner.Add(tbbanner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbbanner);
        }

        // GET: /Banner/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBanner tbbanner = db.tbBanner.Find(id);
            if (tbbanner == null)
            {
                return HttpNotFound();
            }
            return View(tbbanner);
        }

        // POST: /Banner/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="bid,tituloBanner")] tbBanner tbbanner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbbanner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbbanner);
        }

        // GET: /Banner/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbBanner tbbanner = db.tbBanner.Find(id);
            if (tbbanner == null)
            {
                return HttpNotFound();
            }
            return View(tbbanner);
        }

        // POST: /Banner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbBanner tbbanner = db.tbBanner.Find(id);
            db.tbBanner.Remove(tbbanner);
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
