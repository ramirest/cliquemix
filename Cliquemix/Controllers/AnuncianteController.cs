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
    public class AnuncianteController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Anunciante/
        public ActionResult Index()
        {
            var tbanunciante = db.tbAnunciantes.Include(t => t.tbCondicaoPagto).Include(t => t.tbRamoAtividade).Include(t => t.tbTos).Include(t => t.tbUsers);
            return View(tbanunciante.ToList());
        }

        // GET: /Anunciante/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnunciante tbanunciante = db.tbAnunciantes.Find(id);
            if (tbanunciante == null)
            {
                return HttpNotFound();
            }
            return View(tbanunciante);
        }

        // GET: /Anunciante/Create
        public ActionResult Create()
        {
            ViewBag.cpid = new SelectList(db.tbCondicaoPagto, "cpid", "descricao");
            ViewBag.raid = new SelectList(db.tbRamoAtividade, "raid", "descricao");
            ViewBag.tid = new SelectList(db.tbTos, "tid", "titulo");
            ViewBag.uid = new SelectList(db.tbUsers, "uid", "username");
            return View();
        }

        // POST: /Anunciante/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="pid,cnpj,razaoSocial,nmFantasia,contato,ie,im,email,site,obs,cpid,raid,tid,saldoCreditos,leuTermo,uid")] tbAnunciante tbanunciante)
        {
            if (ModelState.IsValid)
            {
                db.tbAnunciantes.Add(tbanunciante);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cpid = new SelectList(db.tbCondicaoPagto, "cpid", "descricao", tbanunciante.cpid);
            ViewBag.raid = new SelectList(db.tbRamoAtividade, "raid", "descricao", tbanunciante.raid);
            ViewBag.tid = new SelectList(db.tbTos, "tid", "titulo", tbanunciante.tid);
            ViewBag.uid = new SelectList(db.tbUsers, "uid", "username", tbanunciante.uid);
            return View(tbanunciante);
        }

        // GET: /Anunciante/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnunciante tbanunciante = db.tbAnunciantes.Find(id);
            if (tbanunciante == null)
            {
                return HttpNotFound();
            }
            ViewBag.cpid = new SelectList(db.tbCondicaoPagto, "cpid", "descricao", tbanunciante.cpid);
            ViewBag.raid = new SelectList(db.tbRamoAtividade, "raid", "descricao", tbanunciante.raid);
            ViewBag.tid = new SelectList(db.tbTos, "tid", "titulo", tbanunciante.tid);
            ViewBag.uid = new SelectList(db.tbUsers, "uid", "username", tbanunciante.uid);
            return View(tbanunciante);
        }

        // POST: /Anunciante/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="pid,cnpj,razaoSocial,nmFantasia,contato,ie,im,email,site,obs,cpid,raid,tid,saldoCreditos,leuTermo,uid")] tbAnunciante tbanunciante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbanunciante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cpid = new SelectList(db.tbCondicaoPagto, "cpid", "descricao", tbanunciante.cpid);
            ViewBag.raid = new SelectList(db.tbRamoAtividade, "raid", "descricao", tbanunciante.raid);
            ViewBag.tid = new SelectList(db.tbTos, "tid", "titulo", tbanunciante.tid);
            ViewBag.uid = new SelectList(db.tbUsers, "uid", "username", tbanunciante.uid);
            return View(tbanunciante);
        }

        // GET: /Anunciante/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnunciante tbanunciante = db.tbAnunciantes.Find(id);
            if (tbanunciante == null)
            {
                return HttpNotFound();
            }
            return View(tbanunciante);
        }

        // POST: /Anunciante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbAnunciante tbanunciante = db.tbAnunciantes.Find(id);
            db.tbAnunciantes.Remove(tbanunciante);
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
