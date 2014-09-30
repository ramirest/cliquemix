using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cliquemix.Models;

namespace Cliquemix.Controllers.Administrador
{
    //Somente usuários com a permissão Administrador podem acessar essa página
    [PermissoesFiltro(Roles = "Administrador")]
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Users//
        public ActionResult Index()
        {
            var tbusers = db.tbUsers.Include(t => t.tbUsersTipo);
            return View(tbusers.ToList());
        }

        // GET: /Users/Details/5
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

        // GET: /Users/Create
        public ActionResult Create()
        {
            ViewBag.utid = new SelectList(db.tbUsersTipo, "utid", "dsUsersTipo");
            return View();
        }

        // POST: /Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="uid,username,pwd,utid")] tbUsers tbusers)
        {
            if (ModelState.IsValid)
            {
                tbusers.pwd = ProcFunc.CryptographyPass(tbusers.pwd); //50
                tbusers.cpwd = tbusers.pwd; //50
                db.tbUsers.Add(tbusers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.utid = new SelectList(db.tbUsersTipo, "utid", "dsUsersTipo", tbusers.utid);
            return View(tbusers);
        }

        // GET: /Users/Edit/5
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
            ViewBag.utid = new SelectList(db.tbUsersTipo, "utid", "dsUsersTipo", tbusers.utid);
            //tbusers.pwd = ProcFunc.ValidCryptographyPass(tbusers.pwd); //50
            return View(tbusers);
        }

        // POST: /Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="uid,username,pwd,utid")] tbUsers tbusers)
        {
            if (ModelState.IsValid)
            {
                tbusers.pwd = ProcFunc.CryptographyPass(tbusers.pwd); //50
                tbusers.cpwd = tbusers.pwd; //50
                db.Entry(tbusers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.utid = new SelectList(db.tbUsersTipo, "utid", "dsUsersTipo", tbusers.utid);
            return View(tbusers);
        }

        // GET: /Users/Delete/5
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

        // POST: /Users/Delete/5
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


        [HttpGet]
        public ActionResult AjustarSenhas()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AjustarSenhas(FormCollection form)
        {
            var db = new ApplicationDbContext();
            try
            {
                foreach (var item in db.tbUsers.ToList())
                {
                    if (item.uid > 22)
                    {
                        item.pwd = ProcFunc.CryptographyPass(item.pwd); //50
                        item.cpwd = item.pwd; //50
                        db.Entry(item).State = EntityState.Modified;
                        db.SaveChanges();                        
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return View();
        }
    }
}

