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
    [Authorize]
    public class AnuncioController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Anuncio/
        public ActionResult Index()
        {
            var tbanuncio = db.tbAnuncio.Include(t => t.tbRamoAtividade);
            return View(tbanuncio.ToList());
        }
        
        // GET: /Anuncio/
        public ActionResult ListAnuncio()
        {
            var tbanuncio = db.tbAnuncio.Include(t => t.tbRamoAtividade).Include(r => r.tbAnuncioStatus);
            return View(tbanuncio.ToList());
        }

        // GET: /Anuncio/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnuncio tbanuncio = db.tbAnuncio.Find(id);
            if (tbanuncio == null)
            {
                return HttpNotFound();
            }
            return View(tbanuncio);
        }

        // GET: /Anuncio/Create
        public ActionResult CreateAnuncio()
        {
            ViewBag.raid = new SelectList(db.tbRamoAtividade, "raid", "descricao");
            ViewBag.asid = new SelectList(db.tbAnuncioStatus, "asid", "dsStatus");            
            return View();
        }

        // POST: /Anuncio/CreateAnuncio
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAnuncio([Bind(Include = "aid,tituloAnuncio,url,dsAnuncio,videoAnuncio,raid,comentar,curtir,compartilhar,asid,dtCriacao")] tbAnuncio tbanuncio)
        {
            if (ModelState.IsValid)
            {
                tbanuncio.dtCriacao = DateTime.Now;
                db.tbAnuncio.Add(tbanuncio);
                db.SaveChanges();
                return RedirectToAction("ListAnuncio");
            }
            return View(tbanuncio);
        }

        // GET: /Anuncio/Create
        public ActionResult Create()
        {
            ViewBag.raid = new SelectList(db.tbRamoAtividade, "raid", "descricao");
            ViewBag.asid = new SelectList(db.tbAnuncioStatus, "asid", "dsStatus");
            return View();
        }

        // POST: /Anuncio/CreateAnuncio
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "aid,tituloAnuncio,url,dsAnuncio,videoAnuncio,raid,comentar,curtir,compartilhar,asid,dtCriacao")] tbAnuncio tbanuncio)
        {
            if (ModelState.IsValid)
            {
                db.tbAnuncio.Add(tbanuncio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.raid = new SelectList(db.tbRamoAtividade, "raid", "descricao", tbanuncio.raid);
            ViewBag.asid = new SelectList(db.tbAnuncioStatus, "asid", "dsStatus", tbanuncio.asid);
            return View(tbanuncio);
        }

        // GET: /Anuncio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnuncio tbanuncio = db.tbAnuncio.Find(id);
            if (tbanuncio == null)
            {
                return HttpNotFound();
            }
            ViewBag.raid = new SelectList(db.tbRamoAtividade, "raid", "descricao", tbanuncio.raid);
            ViewBag.asid = new SelectList(db.tbAnuncioStatus, "asid", "dsStatus", tbanuncio.asid);
            return View(tbanuncio);
        }

        // POST: /Anuncio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "aid,tituloAnuncio,url,dsAnuncio,videoAnuncio,raid,comentar,curtir,compartilhar,asid,dtCriacao")] tbAnuncio tbanuncio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbanuncio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.aaid = new SelectList(db.tbRamoAtividade, "raid", "descricao", tbanuncio.raid);
            return View(tbanuncio);
        }

        // GET: /Anuncio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnuncio tbanuncio = db.tbAnuncio.Find(id);
            if (tbanuncio == null)
            {
                return HttpNotFound();
            }
            return View(tbanuncio);
        }

        // POST: /Anuncio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbAnuncio tbanuncio = db.tbAnuncio.Find(id);
            db.tbAnuncio.Remove(tbanuncio);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Parcial View que retorna as imagens do anúncio
        public PartialViewResult AnuncioImagem()
        {
            //var tbanunciante = db.tbAnunciante.Include(t => t.tbCondicaoPagto).Include(t => t.tbTos).Include(t => t.tbUsers).Include(t => t.tbRamoAtividade);
            //return View(tbanunciante.ToList());
            ViewBag.Img = "http://placehold.it/400x300";
            //var tbanuncioimg = db.tbAnuncioImg.Include(t => t.tbAnuncio);
            //ViewBag.AnuncioId = 18;
            return PartialView();
        }

        //Partial View que salva uma nova imagem ao anúncio
        [HttpPost]
        public PartialViewResult AnuncioImagem(string imagem)
            //[Bind(Include = "imgid,aid,anuncioImg1,anuncioImg2,anuncioImg3,anuncioImg4,anuncioImg5,anuncioImg6,anuncioImg7,anuncioImg8")]
            //tbAnuncioImg tbAnuncioImg)
        {
            ViewBag.AnuncioId = 18;
            ViewBag.Img1 = "http://placehold.it/400x300";
            ViewBag.Img2 = "http://placehold.it/400x300";
            ViewBag.Img3 = "http://placehold.it/400x300";
            ViewBag.Img4 = "http://placehold.it/400x300";
            ViewBag.Img5 = "http://placehold.it/400x300";
            ViewBag.Img6 = "http://placehold.it/400x300";
            ViewBag.Img7 = "http://placehold.it/400x300";
            ViewBag.Img8 = "http://placehold.it/400x300";
            return PartialView();
        }

        /*
        // POST: /Anuncio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "aid,tituloAnuncio,url,dsAnuncio,videoAnuncio,raid,comentar,curtir,compartilhar,asid,dtCriacao")] tbAnuncio tbanuncio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbanuncio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.aaid = new SelectList(db.tbRamoAtividade, "raid", "descricao", tbanuncio.raid);
            return View(tbanuncio);
        }

        */

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
