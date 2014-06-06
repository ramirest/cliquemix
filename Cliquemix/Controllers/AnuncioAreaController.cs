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
    public class AnuncioAreaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /AnuncioArea/
        public ActionResult Index()
        {
            return View(db.tbAnuncioArea.ToList());
        }

        // GET: /AnuncioArea/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnuncioArea tbanuncioarea = db.tbAnuncioArea.Find(id);
            if (tbanuncioarea == null)
            {
                return HttpNotFound();
            }
            return View(tbanuncioarea);
        }

        // GET: /AnuncioArea/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /AnuncioArea/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="aaid,tituloAnuncioArea")] tbAnuncioArea tbanuncioarea)
        {
            if (ModelState.IsValid)
            {
                db.tbAnuncioArea.Add(tbanuncioarea);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbanuncioarea);
        }

        // GET: /AnuncioArea/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnuncioArea tbanuncioarea = db.tbAnuncioArea.Find(id);
            if (tbanuncioarea == null)
            {
                return HttpNotFound();
            }
            return View(tbanuncioarea);
        }

        // POST: /AnuncioArea/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="aaid,tituloAnuncioArea")] tbAnuncioArea tbanuncioarea)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbanuncioarea).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbanuncioarea);
        }

        // GET: /AnuncioArea/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnuncioArea tbanuncioarea = db.tbAnuncioArea.Find(id);
            if (tbanuncioarea == null)
            {
                return HttpNotFound();
            }
            return View(tbanuncioarea);
        }

        // POST: /AnuncioArea/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbAnuncioArea tbanuncioarea = db.tbAnuncioArea.Find(id);
            db.tbAnuncioArea.Remove(tbanuncioarea);
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
