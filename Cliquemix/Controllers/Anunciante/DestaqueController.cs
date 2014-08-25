using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cliquemix.Models;
using Microsoft.AspNet.Identity;
using PagedList;

namespace Cliquemix.Controllers.Anunciante
{
    public class DestaqueController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Destaque/ListDestaque
        public ActionResult ListDestaque(int? pagina)
        {
            int paginaTamanho = 6; //Define o número de elementos por página
            int paginaNumero = (pagina ?? 1); //Define o número inicial da página como 1
            var codAnunciante = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
            var cpde = ProcFunc.RetornarStatusPadraoDestaqueExcluido();
            var tbDestaqueAnunciante = db.tbDestaqueAnunciante.Include(t => t.tbDestaque).
                Where(m => m.pid == codAnunciante).ToList();
            return View(tbDestaqueAnunciante.ToPagedList(paginaNumero, paginaTamanho));
        }



        // GET: Destaque
        public ActionResult Index()
        {
            var tbDestaque = db.tbDestaque.Include(t => t.tbDestaqueDuracao).Include(t => t.tbPacoteClique);
            return View(tbDestaque.ToList());
        }

        // GET: Destaque/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDestaque tbDestaque = db.tbDestaque.Find(id);
            if (tbDestaque == null)
            {
                return HttpNotFound();
            }
            return View(tbDestaque);
        }

        // GET: Destaque/Create
        public ActionResult ComprarDestaque2()
        {
            ViewBag.ddid = new SelectList(db.tbDestaqueDuracao, "ddid", "descricao");
            ViewBag.pcid = new SelectList(db.tbPacoteClique, "pcid", "tituloPacote");
            return View();
        }

        // POST: Destaque/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ComprarDestaque2([Bind(Include = "did,tituloDestaque,qtCredito,tmpEspera,dsDestaque,imgDestaque,ddid,qtDuracao,pcid")] tbDestaque tbDestaque)
        {
            if (ModelState.IsValid)
            {
                db.tbDestaque.Add(tbDestaque);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ddid = new SelectList(db.tbDestaqueDuracao, "ddid", "descricao", tbDestaque.ddid);
            ViewBag.pcid = new SelectList(db.tbPacoteClique, "pcid", "tituloPacote", tbDestaque.pcid);
            return View(tbDestaque);
        }


        public ActionResult ComprarDestaque()
        {
            return View();
        }


        // GET: Destaque/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDestaque tbDestaque = db.tbDestaque.Find(id);
            if (tbDestaque == null)
            {
                return HttpNotFound();
            }
            return View(tbDestaque);
        }

        // POST: Destaque/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbDestaque tbDestaque = db.tbDestaque.Find(id);
            db.tbDestaque.Remove(tbDestaque);
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
