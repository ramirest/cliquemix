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
    [Authorize]
    public class CampanhaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Campanha/
        public ActionResult Index()
        {
            var tbcampanha = db.tbCampanha.Include(t => t.tbCampanhaStatus).Include(t => t.tbDestaque);
            return View(tbcampanha.ToList());
        }

        // GET: /Campanha/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCampanha tbcampanha = db.tbCampanha.Find(id);
            if (tbcampanha == null)
            {
                return HttpNotFound();
            }
            return View(tbcampanha);
        }

        // GET: /Campanha/Create
        public ActionResult Create()
        {
            ViewBag.csid = new SelectList(db.tbCampanhaStatus, "csid", "dsCampStatus");
            ViewBag.did = new SelectList(db.tbDestaque, "did", "tituloDestaque");
            return View();
        }

        // POST: /Campanha/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="cid,tituloCampanha,dtInicio,dtTermino,did,csid")] tbCampanha tbcampanha)
        {
            if (ModelState.IsValid)
            {
                db.tbCampanha.Add(tbcampanha);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.csid = new SelectList(db.tbCampanhaStatus, "csid", "dsCampStatus", tbcampanha.csid);
            ViewBag.did = new SelectList(db.tbDestaque, "did", "tituloDestaque", tbcampanha.did);
            return View(tbcampanha);
        }

        // GET: /Campanha/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCampanha tbcampanha = db.tbCampanha.Find(id);
            if (tbcampanha == null)
            {
                return HttpNotFound();
            }
            ViewBag.csid = new SelectList(db.tbCampanhaStatus, "csid", "dsCampStatus", tbcampanha.csid);
            ViewBag.did = new SelectList(db.tbDestaque, "did", "tituloDestaque", tbcampanha.did);
            return View(tbcampanha);
        }

        // POST: /Campanha/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="cid,tituloCampanha,dtInicio,dtTermino,did,csid")] tbCampanha tbcampanha)
        {
            if (ModelState.IsValid)
            {                
                db.Entry(tbcampanha).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.csid = new SelectList(db.tbCampanhaStatus, "csid", "dsCampStatus", tbcampanha.csid);
            ViewBag.did = new SelectList(db.tbDestaque, "did", "tituloDestaque", tbcampanha.did);
            return View(tbcampanha);
        }

        // GET: /Campanha/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCampanha tbcampanha = db.tbCampanha.Find(id);
            if (tbcampanha == null)
            {
                return HttpNotFound();
            }
            return View(tbcampanha);
        }

        // POST: /Campanha/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbCampanha tbcampanha = db.tbCampanha.Find(id);
            db.tbCampanha.Remove(tbcampanha);
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
