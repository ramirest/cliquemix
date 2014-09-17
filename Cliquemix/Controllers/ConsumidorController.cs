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
    public class ConsumidorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Consumidor
        public ActionResult Index()
        {
            var tbConsumidor = db.tbConsumidor.Include(t => t.tbTos);
            return View(tbConsumidor.ToList());
        }

        // GET: Consumidor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbConsumidor tbConsumidor = db.tbConsumidor.Find(id);
            if (tbConsumidor == null)
            {
                return HttpNotFound();
            }
            return View(tbConsumidor);
        }

        // GET: Consumidor/Create
        public ActionResult Create()
        {
            ViewBag.tid = new SelectList(db.tbTos, "tid", "titulo");
            return View();
        }

        // POST: Consumidor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cid,uid,leuTermo,tid,conid,nomeCompleto,nomeAbreviado,usuSicove,email,cpf,rg,pis,dtNascimento,ativo")] tbConsumidor tbConsumidor)
        {
            if (ModelState.IsValid)
            {
                db.tbConsumidor.Add(tbConsumidor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.tid = new SelectList(db.tbTos, "tid", "titulo", tbConsumidor.tid);
            return View(tbConsumidor);
        }

        // GET: Consumidor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbConsumidor tbConsumidor = db.tbConsumidor.Find(id);
            if (tbConsumidor == null)
            {
                return HttpNotFound();
            }
            ViewBag.tid = new SelectList(db.tbTos, "tid", "titulo", tbConsumidor.tid);
            return View(tbConsumidor);
        }

        // POST: Consumidor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cid,uid,leuTermo,tid,conid,nomeCompleto,nomeAbreviado,usuSicove,email,cpf,rg,pis,dtNascimento,ativo")] tbConsumidor tbConsumidor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbConsumidor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.tid = new SelectList(db.tbTos, "tid", "titulo", tbConsumidor.tid);
            return View(tbConsumidor);
        }

        // GET: Consumidor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbConsumidor tbConsumidor = db.tbConsumidor.Find(id);
            if (tbConsumidor == null)
            {
                return HttpNotFound();
            }
            return View(tbConsumidor);
        }

        // POST: Consumidor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbConsumidor tbConsumidor = db.tbConsumidor.Find(id);
            db.tbConsumidor.Remove(tbConsumidor);
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
