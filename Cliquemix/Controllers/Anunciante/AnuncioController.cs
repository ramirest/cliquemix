using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cliquemix.Models;
using Microsoft.AspNet.Identity;
using PagedList;

namespace Cliquemix.Controllers
{
    [Authorize]
    public class AnuncioController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Anuncio/ListAnuncio
        public ActionResult ListAnuncio(int? pagina)
        {
            //var listaAnuncios = db.tbAnuncio.ToList();
            int paginaTamanho = 6; //Define o número de elementos por página
            int paginaNumero = (pagina ?? 1); //Define o número inicial da página como 1
            var codAnunciante = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
            var cpea = ProcFunc.RetornarStatusPadraoAnuncioExcluido();
            var tbanuncio = db.tbAnuncio.Include(t => t.tbAnuncioCategoria).Include(r => r.tbAnuncioStatus).
                Where(m => m.pid == codAnunciante && m.asid != cpea).ToList();
            return View(tbanuncio.ToPagedList(paginaNumero, paginaTamanho));
            //return View(tbanuncio.ToList());
        }

        // GET: Anuncio/UpdateAnuncio/5
        public ActionResult UpdateAnuncio(int? id)
        {        
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnuncio tbAnuncio = db.tbAnuncio.Find(id);
            if (tbAnuncio == null)
            {
                return HttpNotFound();
            }
            ViewBag.acid = new SelectList(db.tbAnuncioCategoria, "acid", "dsCategoria");
            ViewBag.asid = new SelectList(db.tbAnuncioStatus, "asid", "dsStatus");
            return View(tbAnuncio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAnuncio([Bind(Include = "tituloAnuncio,url,dsAnuncio,acid,pid,videoAnuncio,comentar,curtir,idTempImg," +
                                                          "compartilhar,dtCriacao,imagem1,imagem2,imagem3,imagem4,imagem5,imagem6,imagem7,imagem8")] tbAnuncio tbanuncio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbanuncio).State = EntityState.Modified;
                tbanuncio.pid = ProcFunc.RetornarCodigoAnuncianteCodUsuario(ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName()));
                tbanuncio.dtCriacao = DateTime.Now;
                tbanuncio.asid = ProcFunc.RetornarStatusPadraoAnuncio();
                //db.tbAnuncio.Add(tbanuncio);
                db.SaveChanges();
                //AlterarImgAnuncioSalvo(tbanuncio.aid, Convert.ToInt32(Request.Form.Get("idTempImg")));
                //SalvarLogAnuncio(tbanuncio.aid, (int)tbanuncio.asid, ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName()));
                return RedirectToAction("ListAnuncio");
            }
            return View(tbanuncio);
        }
    

        // GET: /Anuncio/Details/5
        public ActionResult VisualizarAnuncio(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbAnuncio tbAnuncio = db.tbAnuncio.Find(id);
            if (tbAnuncio == null)
            {
                return HttpNotFound();
            }
            if (tbAnuncio.pid == ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName()))
            {
                ViewBag.acid = new SelectList(db.tbAnuncioCategoria, "acid", "dsCategoria");
                ViewBag.asid = new SelectList(db.tbAnuncioStatus, "asid", "dsStatus");
                return View(tbAnuncio);
            }
            else
                return HttpNotFound();
        }


        // GET: /Anuncio/Create
        public ActionResult CreateAnuncio()
        {
            ViewBag.acid = new SelectList(db.tbAnuncioCategoria, "acid", "dsCategoria");
            ViewBag.asid = new SelectList(db.tbAnuncioStatus, "asid", "dsStatus");
            return View();
        }


        // POST: /Anuncio/CreateAnuncio
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAnuncio([Bind(Include = "tituloAnuncio,url,dsAnuncio,acid,pid,videoAnuncio,comentar,curtir,idTempImg," +
                                                          "compartilhar,dtCriacao,imagem1,imagem2,imagem3,imagem4,imagem5,imagem6,imagem7,imagem8,idTemp")] tbAnuncio tbanuncio)
        {
            if (ModelState.IsValid)
            {
                tbanuncio.pid = ProcFunc.RetornarCodigoAnuncianteCodUsuario(ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName()));
                tbanuncio.dtCriacao = DateTime.Now;
                tbanuncio.asid = ProcFunc.RetornarStatusPadraoAnuncio();
                db.tbAnuncio.Add(tbanuncio);
                db.SaveChanges();
                AlterarImgAnuncioSalvo(tbanuncio.aid, Convert.ToInt32(Request.Form.Get("idTempImg")));
                SalvarLogAnuncio(tbanuncio.aid, (int)tbanuncio.asid, ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName()));

                return RedirectToAction("ListAnuncio");
            }
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
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            tbAnuncio tbanuncio = db.tbAnuncio.Find(id);
            tbanuncio.asid = ProcFunc.RetornarStatusPadraoAnuncioExcluido();
            db.Entry(tbanuncio).State = EntityState.Modified;
            db.SaveChanges();
            //var a = tbanuncio.asid;
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
            int codAnunciante = ProcFunc.RetornarCodigoAnuncianteCodUsuario(ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName()));

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


        public void SalvarLogAnuncio(int _aid, int _asid, int _uid)
        {
            tbAnuncioLog tbAnuncioLog = new tbAnuncioLog(); //Instancia a tabela de log do anúncio
            tbAnuncioLog.aid = _aid; //Código do Anúncio
            tbAnuncioLog.asid = _asid; //Status do Anúncio
            tbAnuncioLog.uid = _uid; //Usuário responsável pela atualização
            tbAnuncioLog.dtMovimento = DateTime.Now; //Data atual
            //tbAnuncioLog.imagensRenomeadas = _imgRename;
            db.tbAnuncioLog.Add(tbAnuncioLog);
            db.SaveChanges();
        }


        public void AlterarImgAnuncioSalvo(int pAid, int pIdTemp)
        {
            var img = db.tbAnuncioImg.Where(a => a.idTemp == pIdTemp);
            
            string tempId = String.Format("{0:000000000}", pIdTemp); // Ex: "000000015"
            string aid = String.Format("{0:000000000}", pAid); // Ex: "000000015"
            int codAnunciante = ProcFunc.RetornarCodigoAnuncianteCodUsuario(ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName()));
            
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


        [HttpGet]
        public ActionResult ImagensAnuncio(int pIdTemp)
        //Método que retornar todas as imagens do anúncio
        //de acordo com o código temporário informado nas imagens (idTemp)
        {/*
            try
            {
                var tbAnuncioImg = db.tbAnuncioImg.Where(m => m.idTemp == pIdTemp);
                if (tbAnuncioImg.Any())
                {
                    ViewBag.CodTemp = pIdTemp;
                    return PartialView(tbAnuncioImg.ToList());
                }




                //var tbCampanhaAnuncio = db.tbCampanhaAnuncio.Where(m => m.ctid == pCodCampanha);
                //if (tbCampanhaAnuncio.Any())
                {
                    ViewBag.Tudo = 2;
                    //ViewBag.CodCampanha = pCodCampanha;
                    //return PartialView(tbCampanhaAnuncio.ToList());
                }
                //else
                {
                  //  return PartialView();
                }
            }
            catch (Exception)
            {
                return PartialView();
                throw;
            }*/
            return View();
        }


    }
}
