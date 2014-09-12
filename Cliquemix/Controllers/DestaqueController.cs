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
            var spde = ProcFunc.RetornarStatusPadraoDestaqueExcluido();
            var tbDestaqueAnunciante = db.tbDestaqueAnunciante.Include(t => t.tbDestaque).
                Where(m => m.pid == codAnunciante && m.dasid != spde).ToList();
            return View(tbDestaqueAnunciante.ToPagedList(paginaNumero, paginaTamanho));
        }


        [HttpGet]
        public ActionResult ExibirDestaques()
        {
            //Retornar o código do status de destaque disponível
            var spdd = ProcFunc.RetornarStatusPadraoDestaqueDisponivel();
            //Cria um item para armazenamento de todos os destaques disponíveis
            var tbDestaque = db.tbDestaque.Where(m => m.dsid == spdd).ToList();

            if (tbDestaque.Any())
            {
                return PartialView(tbDestaque);
            }
            else
            {
                return PartialView();
            }
        }
        
        // GET: Destaque/Detalhes/5
        [HttpGet]
        public ActionResult DetalhesDestaque(int? id)
        {
            decimal sa = 0;
            decimal vd = 0;
            decimal sf = 0;
            ViewBag.Info = 0;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbDestaque tbDestaque = db.tbDestaque.Find(id);
            if (tbDestaque == null)
            {
                return HttpNotFound();
            }
            try
            {
                var _nomeUsuario = User.Identity.GetUserName();
                var u = (from usu in db.tbUsers where usu.username == _nomeUsuario select usu).First();
                //return a.uid;
                //Recebe o anunciante logado no sistema
                var anunciante = db.tbAnunciante.First(m => m.uid == u.uid);
                //Seta as variáveis que irão para a tela de confirmação de compra
                ViewBag.idDestaque = tbDestaque.did.ToString();
                ViewBag.tituloDestaque = tbDestaque.tituloDestaque;
                ViewBag.creditosDestaque = tbDestaque.qtCredito.ToString();
                ViewBag.duracaoDestaque = tbDestaque.qtDuracao.ToString() +" "+ tbDestaque.tbDestaqueDuracao.item;
                ViewBag.cliquesDestaque = tbDestaque.tbPacoteClique.qtdeCliques;
                try
                {
                    sa = anunciante.saldoCreditos ?? 0;
                    vd = Convert.ToDecimal(tbDestaque.qtCredito ?? 0);
                    sf = sa - vd;
                }
                catch (Exception)
                {   throw;  }
            }
            catch (Exception)
            {   throw;  }

            ViewBag.saldoAtual = sa.ToString("N", new System.Globalization.CultureInfo("pt-BR"));
            ViewBag.precoDestaque = vd.ToString("N", new System.Globalization.CultureInfo("pt-BR"));
            ViewBag.saldoFinal = sf.ToString("N", new System.Globalization.CultureInfo("pt-BR")); 
            if (sf < 0)
                ViewBag.Info = 2;
            else
                ViewBag.Info = 1;

            return PartialView(tbDestaque);
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

        // CONFIRMAÇÃO DA COMPRA DE DESTAQUE PELO ANUNCIANTE
        // POST: Destaque/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DetalhesDestaque(int idDestaque)
        {
            decimal sa = 0;
            decimal vd = 0;
            decimal sf = 0;
            var cdAnun = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
            var anun = db.tbAnunciante.First(m => m.pid == cdAnun);
            try
            {
                if (idDestaque == 0)
                    return null;

                var destaque = db.tbDestaque.First(m => m.did == idDestaque);
                var tbDestaqueAnunciante = new tbDestaqueAnunciante();

                try
                {
                    sa = anun.saldoCreditos ?? 0;
                    vd = Convert.ToDecimal(destaque.qtCredito ?? 0);
                    sf = sa - vd;

                    if (sf >= 0)
                    {
                        tbDestaqueAnunciante.did = destaque.did;
                        tbDestaqueAnunciante.dtMovimento = DateTime.Now;
                        tbDestaqueAnunciante.pid = cdAnun;
                        tbDestaqueAnunciante.dasid = ProcFunc.RetornarStatusPadraoDestaqueAnuncianteComprado();
                        db.tbDestaqueAnunciante.Add(tbDestaqueAnunciante);
                        db.SaveChanges();
                        ProcFunc.AlterarSaldoAnunciante(cdAnun, 0, vd, "Compra de Destaque para o Anunciante");
                        ProcFunc.InserirMovCreditoAnunciante(cdAnun, sa, sf, tbDestaqueAnunciante.daid);

                        return RedirectToAction("ListDestaque");
                    }
                    else
                    {
                        ProcFunc.SalvarLog(ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName()),
                            "Tentativa de inserção de valores pelo browser", "DestaqueController", "Comprar Destaque");
                    }
                }
                catch (Exception e)
                {
                    ProcFunc.SalvarLog(ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName()),
                        e.Message, "DestaqueController", "Comprar Destaque");
                    return RedirectToAction("ComprarDestaque");
                }

            }
            catch (Exception e)
            {
                ProcFunc.SalvarLog(ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName()),
                    e.Message, "DestaqueController", "Comprar Destaque");
                return RedirectToAction("ComprarDestaque");
            }
            return RedirectToAction("ComprarDestaque");
        }


        public ActionResult ComprarDestaque()
        {
            //Retornar o código do status de destaque disponível
            var spdd = ProcFunc.RetornarStatusPadraoDestaqueDisponivel();
            //Cria um item para armazenamento de todos os destaques disponíveis
            var tbDestaque = db.tbDestaque.Where(m => m.dsid == spdd).ToList();

            if (tbDestaque.Any())
            {
                return View(tbDestaque);
            }
            else
            {
                return View();
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

        public ActionResult CriarDestaqueAdmin()
        {
            return View();
        }

        public ActionResult GerirDestaqueAdmin()
        {
            return View();
        }

        public ActionResult EditarDestaqueAdmin()
        {
            return View();
        }

        public ActionResult DeletarDestaqueAdmin()
        {
            return View();
        }
    }
}
