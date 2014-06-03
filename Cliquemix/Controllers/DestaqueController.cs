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
    public class DestaqueController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Destaque/
        public ActionResult Index()
        {
            return View(db.tbDestaque.ToList());
        }

        // GET: /Destaque/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDestaque tbdestaque = db.tbDestaque.Find(id);
            if (tbdestaque == null)
            {
                return HttpNotFound();
            }
            return View(tbdestaque);
        }

        // GET: /Destaque/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Destaque/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="did,tituloDestaque,qtCredito,tmpEspera,durCampanha,qtDurCampanha,dsDestaque,imgDestaque")] tbDestaque tbdestaque)
        {
            if (ModelState.IsValid)
            {
                db.tbDestaque.Add(tbdestaque);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbdestaque);
        }

        // GET: /Destaque/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDestaque tbdestaque = db.tbDestaque.Find(id);
            if (tbdestaque == null)
            {
                return HttpNotFound();
            }
            return View(tbdestaque);
        }

        // POST: /Destaque/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="did,tituloDestaque,qtCredito,tmpEspera,durCampanha,qtDurCampanha,dsDestaque,imgDestaque")] tbDestaque tbdestaque)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbdestaque).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbdestaque);
        }

        // GET: /Destaque/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDestaque tbdestaque = db.tbDestaque.Find(id);
            if (tbdestaque == null)
            {
                return HttpNotFound();
            }
            return View(tbdestaque);
        }

        // POST: /Destaque/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbDestaque tbdestaque = db.tbDestaque.Find(id);
            db.tbDestaque.Remove(tbdestaque);
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
