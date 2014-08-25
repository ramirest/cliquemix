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
            int paginaTamanho = 6; //Define o número de elementos por página
            int paginaNumero = (pagina ?? 1); //Define o número inicial da página como 1
            var codAnunciante = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
            var cpea = ProcFunc.RetornarStatusPadraoAnuncioExcluido();
            var tbanuncio = db.tbAnuncio.Include(t => t.tbAnuncioCategoria).Include(r => r.tbAnuncioStatus).
                Where(m => m.pid == codAnunciante && m.asid != cpea).ToList();
            return View(tbanuncio.ToPagedList(paginaNumero, paginaTamanho));
        }


        // GET: Anuncio/VisualizarAnuncio/5
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

            ViewBag.actid = tbAnuncio.actid;
            ViewBag.acid = new SelectList(db.tbAnuncioCategoria, "acid", "dsCategoria", tbAnuncio.acid);
            //ViewBag.pid = new SelectList(db.tbAnunciante, "pid", "cnpj", tbAnuncio.pid);
            //ViewBag.asid = new SelectList(db.tbAnuncioStatus, "asid", "dsStatus", tbAnuncio.asid);
            return View(tbAnuncio);
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
            
            ViewBag.actid = tbAnuncio.actid;
            ViewBag.acid = new SelectList(db.tbAnuncioCategoria, "acid", "dsCategoria", tbAnuncio.acid);
            //ViewBag.pid = new SelectList(db.tbAnunciante, "pid", "cnpj", tbAnuncio.pid);
            //ViewBag.asid = new SelectList(db.tbAnuncioStatus, "asid", "dsStatus", tbAnuncio.asid);
            return View(tbAnuncio);
        }


        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAnuncio([Bind(Include = "aid,tituloAnuncio,url,dsAnuncio,acid,pid,videoAnuncio,comentar,curtir,idTempImg," +
                                                          "compartilhar,dtCriacao,imagem1,imagem2,imagem3,imagem4,imagem5,imagem6,imagem7,imagem8,idTemp")] tbAnuncio tbanuncio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int? actid = Convert.ToInt32(Request.Form.Get("actid"));
                    int? asid = Convert.ToInt32(Request.Form.Get("asid"));
                    var url = Request.Form.Get("url");
                    tbanuncio.url = url;
                    tbanuncio.asid = asid;
                    tbanuncio.actid = actid;
                    db.Entry(tbanuncio).State = EntityState.Modified;
                    db.SaveChanges();
                    SalvarLogAnuncio(tbanuncio.aid, (int)tbanuncio.asid, ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName()), "UpdateAnuncio", "");
                }
                catch (Exception e)
                {
                    ProcFunc.SalvarLog(ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName()), e.Message,
                        this.ControllerContext.Controller.ToString(),
                        "Error - UpdateAnuncio " + tbanuncio.aid.ToString());
                    ViewBag.actid = tbanuncio.actid;
                    ViewBag.acid = new SelectList(db.tbAnuncioCategoria, "acid", "dsCategoria", tbanuncio.acid);
                    ViewBag.pid = new SelectList(db.tbAnunciante, "pid", "cnpj", tbanuncio.pid);
                    ViewBag.asid = new SelectList(db.tbAnuncioStatus, "asid", "dsStatus", tbanuncio.asid);
                    return View(tbanuncio);
                }
                return RedirectToAction("ListAnuncio");
            }
            return View(tbanuncio);
        }
    

        // GET: /Anuncio/Create
        public ActionResult CreateAnuncio()
        {
            ViewBag.acid = new SelectList(db.tbAnuncioCategoria, "acid", "dsCategoria");
            ViewBag.asid = new SelectList(db.tbAnuncioStatus, "asid", "dsStatus");
            tbAnuncioCodTemp act = new tbAnuncioCodTemp();
            act.dtMovimento = DateTime.Now;
            act.uid = ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName());
            db.tbAnuncioCodTemp.Add(act);
            db.SaveChanges();
            ViewBag.actid = act.actid;
            ProcFunc.CriarDiretorio(Server.MapPath("~/Arquivos/Anúncios/") + String.Format("{0:00000000}", act.actid), "Imagens");
            return View();
        }


        // POST: /Anuncio/CreateAnuncio
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAnuncio([Bind(Include = "tituloAnuncio,url,dsAnuncio,acid,pid,videoAnuncio,comentar,curtir,idTempImg," +
                                                          "compartilhar,dtCriacao,imagem1,imagem2,imagem3,imagem4,imagem5,imagem6,imagem7,imagem8,idTemp")] tbAnuncio tbanuncio)
        {
            if (ModelState.IsValid)
            {
                tbanuncio.pid = ProcFunc.RetornarCodigoAnuncianteCodUsuario(ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName()));
                tbanuncio.dtCriacao = DateTime.Now;
                tbanuncio.actid = Convert.ToInt32(Request.Form.Get("actid"));
                tbanuncio.asid = ProcFunc.RetornarStatusPadraoAnuncio();
                db.tbAnuncio.Add(tbanuncio);
                db.SaveChanges();
                try
                {
                    ProcFunc.InserirCodAnuncioTbAnuncioCodTemp(tbanuncio.actid, tbanuncio.aid);
                    ProcFunc.InserirCodAnuncioTbAnuncioImg(tbanuncio.actid, tbanuncio.aid, ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName()));
                    SalvarLogAnuncio(tbanuncio.aid, (int)tbanuncio.asid, ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName()), "CreateAnuncio", "");
                }
                catch (Exception e)
                {
                    ProcFunc.SalvarLog(ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName()), e.Message,
                        this.ControllerContext.Controller.ToString(),
                        "Error - CreateAnuncio");
                    throw;
                }

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
        public ActionResult UploadImagem(int? temp)
        {
            var tbAnuncioImg = db.tbAnuncioImg.Where(m => m.actid == temp).
                Where(m => m.ativo == true);
            if (tbAnuncioImg.Any())
            {
                return PartialView(tbAnuncioImg.ToList());
            }
            else
            {
                return PartialView();
            }
        }


        // POST: /Anuncio/DeleteImagem/5
        [HttpPost]
        public ActionResult DeleteImagem(int id)
        {
            try
            {
                tbAnuncioImg tbAnuncioImg = db.tbAnuncioImg.Find(id);
                db.tbAnuncioImg.Remove(tbAnuncioImg);
                db.SaveChanges();
                ProcFunc.RemoverArquivos(tbAnuncioImg.url_imagem);
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }


        [HttpPost]
        public String UploadImagem(HttpPostedFileBase filedata, int? idAlbum)
        {
            var tbAnuncioImg = new tbAnuncioImg();
            
            string tempId = String.Format("{0:00000000}", idAlbum); // SERÁ A PASTA DO ANÚNCIO - Ex: "00000015"
            string tempIdItem = String.Format("{0:000}", RetornaItemImagem(idAlbum)); // SERÁ O CÓDIGO DA IMAGEM - Ex: "001"
            int codAnunciante = ProcFunc.RetornarCodigoAnuncianteCodUsuario(ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName()));

            if (codAnunciante > 0)
            {
                string urlDestino = Server.MapPath("~/Arquivos/Anúncios/") +
                                    tempId + "\\Imagens\\";
                /*** Cria diretório para arquivos do anúnciante ***/
                if (tempIdItem == "000")
                {
                    return null;
                }
                else
                {
                    tbAnuncioImg.url_imagem = urlDestino + tempIdItem + ".jpeg";
                    tbAnuncioImg.tipo = "jpeg";
                    tbAnuncioImg.actid = Convert.ToInt32(tempId);
                    tbAnuncioImg.iditem = Convert.ToInt32(tempIdItem);
                    tbAnuncioImg.tamanho = Convert.ToString(filedata.ContentLength);
                    tbAnuncioImg.ativo = true;
                    db.tbAnuncioImg.Add(tbAnuncioImg);
                    db.SaveChanges();
                    filedata.SaveAs(urlDestino + tempIdItem + ".jpeg");
                    return Convert.ToString(idAlbum);
                }
            }
            else
            {
                return "O anúncio não tem uma pasta para os arquivos";
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
                    var a = (from img in db.tbAnuncioImg where img.actid == temp && img.iditem == i && img.ativo == true select img).First();
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


        public void SalvarLogAnuncio(int _aid, int _asid, int _uid, string dsMovimento, string dsMsgError)
        {
            var tbAnuncioLog = new tbAnuncioLog(); //Instancia a tabela de log do anúncio
            tbAnuncioLog.aid = _aid; //Código do Anúncio
            tbAnuncioLog.asid = _asid; //Status do Anúncio
            tbAnuncioLog.uid = _uid; //Usuário responsável pela atualização
            tbAnuncioLog.dtMovimento = DateTime.Now; //Data atual
            tbAnuncioLog.dsControle = ControllerContext.RequestContext.ToString(); //Recebe o controle atual ("AnuncioController")
            tbAnuncioLog.dsMovimento = dsMovimento;
            tbAnuncioLog.dsMsgError = dsMsgError;
            db.tbAnuncioLog.Add(tbAnuncioLog);
            db.SaveChanges();
        }

    }
}
