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
using Microsoft.Ajax.Utilities;

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


        [HttpGet]
        public ActionResult UploadImagem()
        {
            var tbAnuncioImg = db.tbAnuncioImg;
            if (tbAnuncioImg.Any())
            {
                return PartialView(tbAnuncioImg.ToList());
            }
            else
            {
                return PartialView();
            }
        }

        [HttpPost]
        public String UploadImagem(HttpPostedFileBase filedata, int? idAlbum)
        {
            tbAnuncioImg tbAnuncioImg = new tbAnuncioImg();
            //sVariavelNova = sVariavel.Substring(0, 3);// Aproveitar os 3 primeiros caracteres
            string tempId = String.Format("{0:000000000}", idAlbum); // Ex: "000000015"
            string tempIdItem = String.Format("{0:000}", RetornaItemImagem(idAlbum)); // Ex: "001"
            if (tempIdItem == "000")
            {
                return null;
            }
            else
            {
                tbAnuncioImg.url_imagem = Server.MapPath("~/Images/Fotos/") + tempId + tempIdItem + ".jpeg";
                tbAnuncioImg.tipo = "jpeg";
                tbAnuncioImg.idTemp = Convert.ToInt32(tempId);
                tbAnuncioImg.idTempItem = Convert.ToInt32(tempIdItem);
                tbAnuncioImg.tamanho = Convert.ToString(filedata.ContentLength);
                db.tbAnuncioImg.Add(tbAnuncioImg);
                db.SaveChanges();
                filedata.SaveAs(Server.MapPath("~/Images/Fotos/") + tempId + tempIdItem + ".jpeg");
                UploadImagem();
                return Convert.ToString(idAlbum);
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


        public int RetornaItemImagem(int? temp)
        {
            int i = 1;
            try
            {                
                do
                {
                    var a = (from img in db.tbAnuncioImg where img.idTemp == temp && img.idTempItem == i select img).First();
                    //where cust.City=="London" && cust.Name == "Devon"
                    i++;
                }
                while (i < 10);
            }
            catch (Exception)
            {
                return i;
                throw;
            }
            return 0;
        }

    }
}
