using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Cliquemix.Models;

namespace Cliquemix.Controllers
{
    [Authorize]
    public class AnuncioController : Controller
    {
        private Random random = new Random();
        private int VlrTempImg = 0;
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
        public ActionResult CreateAnuncio([Bind(Include = "tituloAnuncio,url,dsAnuncio,raid,asid,videoAnuncio,comentar,curtir," +
                                                          "compartilhar,dtCriacao,imagem1,imagem2,imagem3,imagem4,imagem5,imagem6,imagem7,imagem8")] tbAnuncio tbanuncio)
        {
            if (ModelState.IsValid)
            {
                tbanuncio.dtCriacao = DateTime.Now;
                string teste = @Request.Form.Get("Imagem1");
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

        /*
        //Parcial View que retorna as imagens do anúncio
        public PartialViewResult AnuncioImagem()
        {
            ViewBag.Img = "http://placehold.it/400x300";
            return PartialView();
        }
        
        //Partial View que salva uma nova imagem ao anúncio
        [HttpPost]
        public PartialViewResult AnuncioImagem(string _img1, string _img2, string _img3, string _img4, string _img5, string _img6, string _img7, string _img8)
            //[Bind(Include = "imgid,aid,anuncioImg1,anuncioImg2,anuncioImg3,anuncioImg4,anuncioImg5,anuncioImg6,anuncioImg7,anuncioImg8")]
            //tbAnuncioImg tbAnuncioImg)
        {
            ViewBag.Img1 = _img1;
            ViewBag.Img2 = _img2;
            ViewBag.Img3 = _img3;
            ViewBag.Img4 = _img4;
            ViewBag.Img5 = _img5;
            ViewBag.Img6 = _img6;
            ViewBag.Img7 = _img7;
            ViewBag.Img8 = _img8;
            return null;
        }
        */
        [HttpGet]
        public ActionResult UploadImagem()
        {
            var tbAnuncioImg = db.tbAnuncioImg.ToList();            
            if (tbAnuncioImg.Count > 0)
            {
                return PartialView(tbAnuncioImg);
            }
            else
            {
                return PartialView();
            }
        }

        [HttpPost]
        public String UploadImagem(HttpPostedFileBase filedata, int? idAlbum)
        {
            VlrTempImg = random.Next(1000, 100000);
            filedata.SaveAs(Server.MapPath("~/Images/Fotos/") + VlrTempImg.ToString()+".jpeg");
            //filedata.SaveAs(Server.MapPath("~/Images/Fotos/") + filedata.FileName);
            return "1";
        }

        public int RetornaCodigoTipoUsuario(string _tipo)
        {
            var a = (from tipo in db.tbUsersTipo where tipo.dsUsersTipo == _tipo select tipo).First();
            return a.utid;
        }

        public bool AnuncioTemImagem(string _nomeUsuario)
        {
            try
            {
                var a = (from usu in db.tbUsers where usu.username == _nomeUsuario select usu).First();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
