using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cliquemix.Models;
using Microsoft.AspNet.Identity;

namespace Cliquemix.Controllers
{
    [Authorize]
    public class AnuncioController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: /Anuncio/ListAnuncio
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAnuncio([Bind(Include = "tituloAnuncio,url,dsAnuncio,raid,pid,videoAnuncio,comentar,curtir,idTempImg," +
                                                          "compartilhar,dtCriacao,imagem1,imagem2,imagem3,imagem4,imagem5,imagem6,imagem7,imagem8")] tbAnuncio tbanuncio)
        {
            if (ModelState.IsValid)
            {
                tbanuncio.pid = ProcFunc.RetornarCodigoAnuncianteCodUsuario(ProcFunc.RetornaCodigoUsuario(User.Identity.GetUserName()));
                tbanuncio.dtCriacao = DateTime.Now;
                tbanuncio.asid = ProcFunc.RetornaStatusPadraoAnuncio();
                db.tbAnuncio.Add(tbanuncio);
                db.SaveChanges();
                AlterarImgAnuncioSalvo(tbanuncio.aid, Convert.ToInt32(Request.Form.Get("idTempImg")));
                SalvarLogAnuncio(tbanuncio.aid, (int)tbanuncio.asid, ProcFunc.RetornaCodigoUsuario(User.Identity.GetUserName()), "Sim");

                return RedirectToAction("ListAnuncio");
            }
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
        public ActionResult Edit([Bind(Include = "aid,tituloAnuncio,url,dsAnuncio,videoAnuncio,raid,comentar,curtir,compartilhar,asid,dtCriacao,idTempImg")] tbAnuncio tbanuncio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbanuncio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListAnuncio");
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
            return RedirectToAction("ListAnuncio");
        }


        [HttpGet]
        public ActionResult UploadImagem(int temp)
        {
            var tbAnuncioImg = db.tbAnuncioImg.Where(m=>m.idTemp == temp);
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
            int codAnunciante = ProcFunc.RetornarCodigoAnuncianteCodUsuario(ProcFunc.RetornaCodigoUsuario(User.Identity.GetUserName()));

            if (codAnunciante > 0)
            {
                string urlDestino = Server.MapPath("~/Arquivos/Anunciantes/") +
                                    String.Format("{0:000000}", codAnunciante) + "\\Anúncios\\";
                /*** Cria diretório para arquivos do anúnciante ***/
                if (tempIdItem == "000")
                {
                    return null;
                }
                else
                {
                    tbAnuncioImg.url_imagem = urlDestino + tempId + tempIdItem + ".jpeg";
                    tbAnuncioImg.tipo = "jpeg";
                    tbAnuncioImg.idTemp = Convert.ToInt32(tempId);
                    tbAnuncioImg.idTempItem = Convert.ToInt32(tempIdItem);
                    tbAnuncioImg.tamanho = Convert.ToString(filedata.ContentLength);
                    tbAnuncioImg.tempRenomeado = false;
                    db.tbAnuncioImg.Add(tbAnuncioImg);
                    db.SaveChanges();
                    filedata.SaveAs(urlDestino + tempId + tempIdItem + ".jpeg");
                    return Convert.ToString(idAlbum);
                }
            }
            else
            {
                return "O usuário logado não tem uma pasta para os arquivos";
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

        public void SalvarLogAnuncio(int _aid, int _asid, int _uid, string _imgRename)
        {
            tbAnuncioImgLog tbAnuncioImgLog = new tbAnuncioImgLog();
            tbAnuncioImgLog.aid = _aid;
            tbAnuncioImgLog.asid = _asid;
            tbAnuncioImgLog.uid = _uid;
            tbAnuncioImgLog.dtMovimento = DateTime.Now;
            tbAnuncioImgLog.imagensRenomeadas = _imgRename;
            db.tbAnuncioImgLog.Add(tbAnuncioImgLog);
            db.SaveChanges();
        }

        public void AlterarImgAnuncioSalvo(int pAid, int pIdTemp)
        {
            var img = db.tbAnuncioImg.Where(a => a.idTemp == pIdTemp);
            
            string tempId = String.Format("{0:000000000}", pIdTemp); // Ex: "000000015"
            string aid = String.Format("{0:000000000}", pAid); // Ex: "000000015"
            int codAnunciante = ProcFunc.RetornarCodigoAnuncianteCodUsuario(ProcFunc.RetornaCodigoUsuario(User.Identity.GetUserName()));
            
            foreach (var item in img)
            {
                string tempIdItem = String.Format("{0:000}", item.idTempItem); // Ex: "001"
                string url = Server.MapPath("~/Arquivos/Anunciantes/") +
                                    String.Format("{0:000000}", codAnunciante) + "\\Anúncios\\";

                ProcFunc.MoverArquivosEntrePastas(
                    (url + tempId + tempIdItem + ".jpeg"), //Origem
                    (url + aid + tempIdItem + ".jpeg")); //Destino

                item.aid = pAid;
                item.idTemp = pAid;
                item.tempRenomeado = true;
            }
        }

    }
}
