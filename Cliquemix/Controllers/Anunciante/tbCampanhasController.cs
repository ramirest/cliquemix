using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cliquemix.Models;

namespace Cliquemix.Controllers.Anunciante
{
    public class tbCampanhasController : Controller
    {
        private cliquemixEntities db = new cliquemixEntities();

        // GET: tbCampanhas
        public ActionResult Index()
        {
            var tbCampanha = db.tbCampanha.Include(t => t.tbCampanhaStatus).Include(t => t.tbDestaque).Include(t => t.tbAnunciante);
            return View(tbCampanha.ToList());
        }

        // GET: tbCampanhas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCampanha tbCampanha = db.tbCampanha.Find(id);
            if (tbCampanha == null)
            {
                return HttpNotFound();
            }
            return View(tbCampanha);
        }

        // GET: tbCampanhas/Create
        public ActionResult Create()
        {
            ViewBag.csid = new SelectList(db.tbCampanhaStatus, "csid", "dsCampStatus");
            ViewBag.did = new SelectList(db.tbDestaque, "did", "tituloDestaque");
            ViewBag.pid = new SelectList(db.tbAnunciante, "pid", "cnpj");
            return View();
        }

        // POST: tbCampanhas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cid,tituloCampanha,dtInicio,dtTermino,did,csid,pid")] tbCampanha tbCampanha)
        {
            if (ModelState.IsValid)
            {
                db.tbCampanha.Add(tbCampanha);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.csid = new SelectList(db.tbCampanhaStatus, "csid", "dsCampStatus", tbCampanha.csid);
            ViewBag.did = new SelectList(db.tbDestaque, "did", "tituloDestaque", tbCampanha.did);
            ViewBag.pid = new SelectList(db.tbAnunciante, "pid", "cnpj", tbCampanha.pid);
            return View(tbCampanha);
        }

        // GET: tbCampanhas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCampanha tbCampanha = db.tbCampanha.Find(id);
            if (tbCampanha == null)
            {
                return HttpNotFound();
            }
            ViewBag.csid = new SelectList(db.tbCampanhaStatus, "csid", "dsCampStatus", tbCampanha.csid);
            ViewBag.did = new SelectList(db.tbDestaque, "did", "tituloDestaque", tbCampanha.did);
            ViewBag.pid = new SelectList(db.tbAnunciante, "pid", "cnpj", tbCampanha.pid);
            return View(tbCampanha);
        }

        // POST: tbCampanhas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cid,tituloCampanha,dtInicio,dtTermino,did,csid,pid")] tbCampanha tbCampanha)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbCampanha).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.csid = new SelectList(db.tbCampanhaStatus, "csid", "dsCampStatus", tbCampanha.csid);
            ViewBag.did = new SelectList(db.tbDestaque, "did", "tituloDestaque", tbCampanha.did);
            ViewBag.pid = new SelectList(db.tbAnunciante, "pid", "cnpj", tbCampanha.pid);
            return View(tbCampanha);
        }

        // GET: tbCampanhas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCampanha tbCampanha = db.tbCampanha.Find(id);
            if (tbCampanha == null)
            {
                return HttpNotFound();
            }
            return View(tbCampanha);
        }

        // POST: tbCampanhas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbCampanha tbCampanha = db.tbCampanha.Find(id);
            db.tbCampanha.Remove(tbCampanha);
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
