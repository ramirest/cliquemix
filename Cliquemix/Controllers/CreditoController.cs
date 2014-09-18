using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cliquemix.Models;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNet.Identity;
using PagedList;

namespace Cliquemix.Controllers
{
    //Somente usuários com a permissão Anunciante podem acessar essa página
    [PermissoesFiltro(Roles = "Anunciante")]
    public class CreditoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //Atributo que controla se o Anunciante tem Créditos pendentes para pagamento
        internal static string TipoErro { get; set; }

        //
        // GET: /Credito/
        public ActionResult BuyCredit()
        {
            try
            {
                var cdAnun = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
                var anun = db.tbAnunciante.First(m => m.pid == cdAnun);

                if (!ProcFunc.VerificarAnuncianteCreditoPendente(cdAnun))
                {
                    //CÓDIGO EXCEPTION [BuyCredit] 00007
                    //O Anunciante possui créditos pendentes para pagamento. [007.00001]

                    TipoErro = "007.00001";
                        return RedirectToAction("ManagerCredit");
                                      
                }


                ViewBag.crid = new SelectList((from s in db.tbCredito.ToList()
                        select new
                        {
                            Crid = s.crid,
                            Credito = s.qtCredito +
                                      " - " +
                                      String.Format(new CultureInfo("pt-BR"), "{0:C}", s.vlCredito)
                        }), "Crid", "Credito", null);

                try
                {
                    ViewBag.saldoAtualAnunciante = Convert.ToDecimal(anun.saldoCreditos.ToString())
                        .ToString("N", new System.Globalization.CultureInfo("pt-BR"));
                }
                catch (Exception)
                {
                    ViewBag.saldoAtualAnunciante = Convert.ToDecimal(0)
                        .ToString("N", new System.Globalization.CultureInfo("pt-BR"));
                }
                try
                {
                    ViewBag.dtUltimaCompra = db.tbCreditoCompra.Where(cc => cc.pid == cdAnun).Max(d => d.dtCompra);
                    if (ViewBag.dtUltimaCompra == null)
                    {
                        ViewBag.dtUltimaCompra = "00/00/0000 - 00:00:00";
                    }
                }
                catch (Exception)
                {
                    ViewBag.dtUltimaCompra = "00/00/0000 - 00:00:00";
                }
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult ManagerCredit(int? pagina)
        {
            int paginaTamanho = 6; //Define o número de elementos por página
            int paginaNumero = (pagina ?? 1); //Define o número inicial da página como 1
            var codAnunciante = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
            decimal saldo = 0;
            try
            {
                saldo = db.tbAnunciante.First(a => a.pid == codAnunciante).saldoCreditos ?? 0;
            }
            catch (Exception)
            {
                saldo = 0;
                throw;
            }
            var tbCreditoCompra = db.tbCreditoCompra.Where(m => m.pid == codAnunciante).ToList();
            ViewBag.ErroCompraCredito = TipoErro;
            ViewBag.saldoAtual = saldo.ToString("N", new System.Globalization.CultureInfo("pt-BR"));
            TipoErro = String.Empty;
            return View(tbCreditoCompra.ToPagedList(paginaNumero, paginaTamanho));
        }

        [HttpGet]
        public ActionResult DetalhesCredito()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult DetalhesCredito(int pCodPacote)
        {
            try
            {
                var tbCredito = db.tbCredito.Where(c => c.crid == pCodPacote);
                if (tbCredito.Any())
                {
                    return PartialView(tbCredito.ToList());
                }
                else
                {
                    return PartialView();
                }
            }
            catch (Exception)
            {
                return null;
                throw;
            }
        }


        // GET: Credito/ComprarCredito/5
        [HttpGet]
        public ActionResult ComprarCredito(int? id)
        {
            decimal sa = 0; //Saldo Atual
            decimal qc = 0; //Qtde de Compra
            decimal sf = 0; //Saldo Final
            ViewBag.Info = 0;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCredito tbCredito = db.tbCredito.Find(id);
            //tbDestaque tbDestaque = db.tbDestaque.Find(id);
            if (tbCredito == null)
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
                ViewBag.idCredito = tbCredito.crid.ToString();
                ViewBag.tituloCredito = tbCredito.tituloPacote;
                ViewBag.qtdeCredito = tbCredito.qtCredito.ToString();
                ViewBag.vlrCredito = tbCredito.vlCredito.ToString();
                if (tbCredito.promocional)
                    ViewBag.promocional = "Sim";
                else
                    ViewBag.promocional = "Não";
                try
                {
                    sa = anunciante.saldoCreditos ?? 0;
                    qc = Convert.ToDecimal(tbCredito.qtCredito ?? 0);
                    sf = sa + qc;
                }
                finally
                {
                    ViewBag.saldoAtual = sa.ToString("N", new System.Globalization.CultureInfo("pt-BR"));
                    ViewBag.qtdeCredito = qc.ToString("N", new System.Globalization.CultureInfo("pt-BR"));
                    ViewBag.saldoFinal = sf.ToString("N", new System.Globalization.CultureInfo("pt-BR"));
                }
            }
            catch (Exception)
            { throw; }

            if (sf < 0)
                ViewBag.Info = 2; //Retorna um estado de erro ("Não foi possível comprar esse pacote de crédito")
                //Você não tem permissão para comprar os créditos. [Cod. 005.00001]
            else
                ViewBag.Info = 1; //Retorna um estado válido

            return PartialView(tbCredito);
        }

        // CONFIRMAÇÃO DA COMPRA DE CRÉDITO PELO ANUNCIANTE
        // POST: Credito/ComprarCredito
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ComprarCredito(int idCredito)
        {
            decimal sa = 0; //Saldo Atual
            decimal qc = 0; //Qtde de Compra
            decimal sf = 0; //Saldo Final
            var cdAnun = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
            var anun = db.tbAnunciante.First(m => m.pid == cdAnun);

            try
            {
                if (idCredito == 0)
                    return null;

                var credito = db.tbCredito.First(m => m.crid == idCredito);
                var tbCreditoCompra = new tbCreditoCompra();

                try
                {
                    sa = anun.saldoCreditos ?? 0;
                    qc = Convert.ToDecimal(credito.qtCredito ?? 0);
                    sf = sa + qc;

                    if (sf > 0)
                    {
                        var cdCon = anun.tbAnunciantePatrocinador.First(m => m.pid == cdAnun).cid;

                        tbCreditoCompra.crid = credito.crid;
                        tbCreditoCompra.dtCompra = DateTime.Now;
                        tbCreditoCompra.pid = cdAnun;
                        tbCreditoCompra.crsid = ProcFunc.RetornarStatusPadraoCompraCreditoAguardandoPagamento();
                        tbCreditoCompra.promocional = credito.promocional;
                        db.tbCreditoCompra.Add(tbCreditoCompra);
                        db.SaveChanges();
                        //ProcFunc.ProcessaCompraCreditoAguardandoPagamento(cdAnun, tbCreditoCompra.ccid);
                        //ProcFunc.AlterarSaldoAnunciante(cdAnun, 1, qc, "Compra de Créditos para o Anunciante");
                        //ProcFunc.GerarPontuacaoConsumidor(cdAnun, cdCon, qc, "Compra de Créditos para o Anunciante");

                        return RedirectToAction("ManagerCredit");
                    }
                    else
                    {
                        ProcFunc.SalvarLog(ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName()),
                            "Tentativa de inserção de valores pelo browser", "CreditoController", "Comprar Crédito");
                    }
                }
                catch (Exception e)
                {
                    ProcFunc.SalvarLog(ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName()),
                        e.Message, "CreditoController", "Comprar Crédito");
                    return RedirectToAction("BuyCredit");
                }

            }
            catch (Exception e)
            {
                ProcFunc.SalvarLog(ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName()),
                    e.Message, "CreditoController", "Comprar Crédito");
                return RedirectToAction("BuyCredit");
            }
            return RedirectToAction("BuyCredit");
        }

        public ActionResult ConfirmarCompra(int ccid)
        {
            return View();
        }

	}
}