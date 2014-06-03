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
    public class AnuncianteDestaqueController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /AnuncianteDestaque/
        public ActionResult Index()
        {
            var tbanunciantedestaque = db.tbAnuncianteDestaques.Include(t => t.tbAnunciante).Include(t => t.tbAnuncianteDestaqueStatus).Include(t => t.tbDestaque);
            return View(tbanunciantedestaque.ToList());
        }

        // GET: /AnuncianteDestaque/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnuncianteDestaque tbanunciantedestaque = db.tbAnuncianteDestaques.Find(id);
            if (tbanunciantedestaque == null)
            {
                return HttpNotFound();
            }
            return View(tbanunciantedestaque);
        }

        // GET: /AnuncianteDestaque/Create
        public ActionResult Create()
        {
            ViewBag.pid = new SelectList(db.tbAnunciantes, "pid", "cnpj");
            ViewBag.adsid = new SelectList(db.tbAnuncianteDestaqueStatus, "adsid", "dsAnuncianteDestaqueStatus");
            ViewBag.did = new SelectList(db.tbDestaque, "did", "tituloDestaque");
            return View();
        }

        // POST: /AnuncianteDestaque/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="adid,pid,did,adsid,qtCreditoCompra,dtAssociacao")] tbAnuncianteDestaque tbanunciantedestaque)
        {
            if (ModelState.IsValid)
            {
                db.tbAnuncianteDestaques.Add(tbanunciantedestaque);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pid = new SelectList(db.tbAnunciantes, "pid", "cnpj", tbanunciantedestaque.pid);
            ViewBag.adsid = new SelectList(db.tbAnuncianteDestaqueStatus, "adsid", "dsAnuncianteDestaqueStatus", tbanunciantedestaque.adsid);
            ViewBag.did = new SelectList(db.tbDestaque, "did", "tituloDestaque", tbanunciantedestaque.did);
            return View(tbanunciantedestaque);
        }

        // GET: /AnuncianteDestaque/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnuncianteDestaque tbanunciantedestaque = db.tbAnuncianteDestaques.Find(id);
            if (tbanunciantedestaque == null)
            {
                return HttpNotFound();
            }
            ViewBag.pid = new SelectList(db.tbAnunciantes, "pid", "cnpj", tbanunciantedestaque.pid);
            ViewBag.adsid = new SelectList(db.tbAnuncianteDestaqueStatus, "adsid", "dsAnuncianteDestaqueStatus", tbanunciantedestaque.adsid);
            ViewBag.did = new SelectList(db.tbDestaque, "did", "tituloDestaque", tbanunciantedestaque.did);
            return View(tbanunciantedestaque);
        }

        // POST: /AnuncianteDestaque/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="adid,pid,did,adsid,qtCreditoCompra,dtAssociacao")] tbAnuncianteDestaque tbanunciantedestaque)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbanunciantedestaque).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pid = new SelectList(db.tbAnunciantes, "pid", "cnpj", tbanunciantedestaque.pid);
            ViewBag.adsid = new SelectList(db.tbAnuncianteDestaqueStatus, "adsid", "dsAnuncianteDestaqueStatus", tbanunciantedestaque.adsid);
            ViewBag.did = new SelectList(db.tbDestaque, "did", "tituloDestaque", tbanunciantedestaque.did);
            return View(tbanunciantedestaque);
        }

        // GET: /AnuncianteDestaque/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnuncianteDestaque tbanunciantedestaque = db.tbAnuncianteDestaques.Find(id);
            if (tbanunciantedestaque == null)
            {
                return HttpNotFound();
            }
            return View(tbanunciantedestaque);
        }

        // POST: /AnuncianteDestaque/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbAnuncianteDestaque tbanunciantedestaque = db.tbAnuncianteDestaques.Find(id);
            db.tbAnuncianteDestaques.Remove(tbanunciantedestaque);
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
