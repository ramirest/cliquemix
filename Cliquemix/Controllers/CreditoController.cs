using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
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
            int sAgPagto = ProcFunc.RetornarStatusPadraoCompraCreditoAguardandoPagamento();
            int sApPagto = ProcFunc.RetornarStatusPadraoCompraCreditoPagtoAprovado();

            List<tbCreditoCompra> credCompras = null;

            try
            {
                //Status padrão para compra de crédito aguardando pagamento
                credCompras = db.tbCreditoCompra.Where(c => c.pid == codAnunciante).
                    Where(s => s.crsid == sAgPagto).ToList();
            }
            catch (Exception)
            {
                //Status padrão para compra de crédito aguardando pagamento
                credCompras = null;
            }

            try
            {
                //Consultar todas as compras que estão em aberto e verificar se já foram quitadas ou não
                foreach (var item in credCompras)
                {
                    if (VerificaFaturaPaga(item.tbTransacaoXml.sid))
                    {
                        ProcFunc.ProcessaCompraCreditoConfPagto(codAnunciante, item.ccid);
                    }
                }
            }
            catch (Exception ex)
            {
                ProcFunc.SalvarLog(ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName()), ex.Message,
                    this.ControllerContext.ToString(), "Erro Verificar Pagamentos");
            }

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


        [HttpPost]
        public ActionResult CancelarCompra(int? ccid)
        {
            tbCreditoCompra creditoCompra = new tbCreditoCompra();
            creditoCompra = db.tbCreditoCompra.First(c => c.ccid == ccid);
            creditoCompra.crsid = ProcFunc.RetornarStatusPadraoCompraCreditoCancelada();
            if (ModelState.IsValid)
            {
                db.Entry(creditoCompra).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("ManagerCredit");
            //return View("ManagerCredit");
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

            if (tbCredito == null)
            {
                return HttpNotFound();
            }
            try
            {
                var nomeUsuario = User.Identity.GetUserName();
                var u = (from usu in db.tbUsers where usu.username == nomeUsuario select usu).First();
                
                //Recebe o anunciante logado no sistema
                var anunciante = db.tbAnunciante.First(m => m.uid == u.uid);
                //Seta as variáveis que irão para a tela de confirmação de compra
                ViewBag.idCredito = tbCredito.crid.ToString();
                ViewBag.tituloCredito = tbCredito.tituloPacote;
                ViewBag.qtdeCredito = tbCredito.qtCredito.ToString();
                ViewBag.vlrCredito = tbCredito.vlCredito.ToString();
                ViewBag.promocional = tbCredito.promocional ? "Sim" : "Não";
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

                        CriarTransacaoXml(tbCreditoCompra.ccid, tbCreditoCompra.dtCompra ?? DateTime.Now);
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

        [HttpGet]
        public ActionResult ConfirmarCompra(Int32 ccid)
        {
            if (ccid == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tbCreditoCompra tbCreditoCompra = db.tbCreditoCompra.Find(ccid);
            if (tbCreditoCompra == null)
            {
                return HttpNotFound();
            }

            if (!ValidacaoCompraCredito(tbCreditoCompra.ccid))
            {
                return RedirectToAction("ManagerCredit");
            }

            DateTime dtVencimento = tbCreditoCompra.dtCompra ?? DateTime.Now;
            var codPedido = tbCreditoCompra.ccid.ToString(format: "000000");
            
            // Cria uma instância com as informações da cultura americana
            var culturaAmericana = new CultureInfo("en-US");
            /*
            // Converte a string para um valor DateTime
            DateTime dtInicio = DateTime.Parse(Request.Form.Get("dtInicio"), culturaAmericana);
            DateTime dtTermino = DateTime.Parse(Request.Form.Get("dtTermino"), culturaAmericana);
            */

            ViewBag.CodPedido = codPedido;
            ViewBag.dtVencimento = DateTime.Parse(dtVencimento.AddDays(10).ToString("d")).ToShortDateString();
            return View(tbCreditoCompra);
        }


        public Boolean ValidacaoCompraCredito(int ccid)
        {
            tbCreditoCompra credito = db.tbCreditoCompra.First(c => c.ccid == ccid);

            //Está cancelado?
            var t = ProcFunc.RetornarStatusPadraoCompraCreditoAguardandoPagamento();
            if (credito.crsid != t)
            {
                //CÓDIGO EXCEPTION [ValidacaoCompraCredito] 00007
                //A Compra de Crédito informada não está disponível para pagamento. [007.00002]
                TipoErro = "007.00002";
                return false;
            }

            //Pertence ao anunciante logado?
            t = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
            if (credito.pid != t)
            {
                //CÓDIGO EXCEPTION [ValidacaoCompraCredito] 00007
                //A Compra de Crédito informada não está disponível para pagamento. [007.00002]
                TipoErro = "007.00002";
                return false;
            }

            return true;
        }


        [HttpGet]
        public ActionResult EnderecoMixClique()
        {
            var enderecoMixClique = new EnderecoMixClique();
            enderecoMixClique.BuscarEnderecoMixClique();
            return PartialView(enderecoMixClique);
        }


        [HttpGet]
        public ActionResult DadosAnunciante()
        {
            int cd = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
            tbAnuncianteEndereco tbAnuncianteEndereco = db.tbAnuncianteEndereco.First(a => a.pid == cd);
            return PartialView(tbAnuncianteEndereco);
        }

        public void CriarTransacaoXml(int codCompra, DateTime dt)
        {
            //Formato: 2013-09-18-11:39:45:54321
            string txt =
                dt.Year.ToString(format: "0000") +
                dt.Month.ToString(format: "00") +
                dt.Day.ToString(format: "00") +
                dt.Hour.ToString(format: "00") +
                dt.Minute.ToString(format: "00") +
                dt.Second.ToString(format: "00") +
                dt.Millisecond.ToString(format: "000");

            string sid = string.Empty;

            //Gerar criptografia da data atual
            using (MD5 md5Hash = MD5.Create())
            {
                sid = CriptografarSid(md5Hash, txt);
            }

            tbCreditoCompra tbCreditoCompra = db.tbCreditoCompra.First(c => c.ccid == codCompra);
            tbAnuncianteEndereco tbAnuncianteEndereco = db.tbAnuncianteEndereco.First(a => a.pid == tbCreditoCompra.pid);

            //Salvar a transação no banco como tabela tbTransacaoXml e retorna o ID gerado
            var txid = SalvarTransacaoXml(tbCreditoCompra, tbAnuncianteEndereco, sid);
            
            //Gera a transação como arquivo XML
            if (txid > 0)
                GerarArquivoXml(txid, tbCreditoCompra.ccid);

            post_data(sid, 1); //Criar Transação

        }


        public int SalvarTransacaoXml(tbCreditoCompra crdCompra, tbAnuncianteEndereco endereco, string sid)
        {
            try
            {
                tbTransacaoXml tbTransacaoXml = new tbTransacaoXml();

                tbTransacaoXml.transacao = "criartransacao";
                tbTransacaoXml.sid = sid;
                tbTransacaoXml.codAfiliacao = ProcFunc.RetornarCodigoAfiliacaoMixClique();
                tbTransacaoXml.chave = ProcFunc.RetornarChaveMixClique();
                tbTransacaoXml.pid = crdCompra.pid;
                tbTransacaoXml.credor = crdCompra.tbAnunciante.razaoSocial;
                tbTransacaoXml.valor = crdCompra.tbCredito.vlCredito;
                int dpvb = ProcFunc.RetornarDiaPadraoVencimentoBoleto();
                tbTransacaoXml.dataVencimento = Convert.ToDateTime(crdCompra.dtCompra).AddDays(dpvb);
                tbTransacaoXml.descricao = crdCompra.tbCredito.tituloPacote;
                tbTransacaoXml.cnpj = crdCompra.tbAnunciante.cnpj;
                tbTransacaoXml.cep = endereco.cep;
                tbTransacaoXml.cidade = endereco.nomeCidade;
                tbTransacaoXml.estado = endereco.ufEstado;
                tbTransacaoXml.bairro = endereco.nomeBairro;
                tbTransacaoXml.endereco = endereco.endereco;
                tbTransacaoXml.numero = endereco.numero_endereco;
                tbTransacaoXml.complemento = endereco.complemento_endereco;
                tbTransacaoXml.numMaximoParcelas = ProcFunc.RetornarNumMaxParcelasPagto();
                tbTransacaoXml.campoLivre = crdCompra.tbCredito.dsCredito;
                tbTransacaoXml.urlRetornoLoja = ProcFunc.RetornarUrlRetornoAposPagto();
                db.tbTransacaoXml.Add(tbTransacaoXml);
                db.SaveChanges();
                return tbTransacaoXml.txid;
            }
            catch (Exception ex)
            {
                ProcFunc.SalvarLog(ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName()), ex.Message,
                    this.ControllerContext.ToString(), "Salvar Transacao Xml " + sid);
                return 0;
            }
        }

        public void GerarArquivoXml(int cdXml, int cdCredCompra)
        {
            //Parâmetro de código do xml no banco e código da compra de crédito
            tbCreditoCompra tbCreditoCompra = db.tbCreditoCompra.First(c => c.ccid == cdCredCompra);
            tbTransacaoXml tbTransacaoXml = db.tbTransacaoXml.First(x => x.txid == cdXml);

            try
            {
                //Tipo = 0 (consultar)  |  Tipo = 1 (criartransacao)
                var doc = new XmlDocument();

                //(1) the xml declaration is recommended, but not mandatory
                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = doc.DocumentElement;
                doc.InsertBefore(xmlDeclaration, root);

                XmlElement element1 = doc.CreateElement(string.Empty, "xml", string.Empty);
                doc.AppendChild(element1);

                XmlElement element2 = doc.CreateElement(string.Empty, "transacao", string.Empty);
                XmlText text2 = doc.CreateTextNode(tbTransacaoXml.transacao);
                element2.AppendChild(text2);
                element1.AppendChild(element2);

                XmlElement element3 = doc.CreateElement(string.Empty, "sid", string.Empty);
                XmlText text3 = doc.CreateTextNode(tbTransacaoXml.sid);
                element3.AppendChild(text3);
                element1.AppendChild(element3);

                XmlElement element4 = doc.CreateElement(string.Empty, "codigoafiliacao", string.Empty);
                XmlText text4 = doc.CreateTextNode(tbTransacaoXml.codAfiliacao);
                element4.AppendChild(text4);
                element1.AppendChild(element4);

                XmlElement element5 = doc.CreateElement(string.Empty, "chave", string.Empty);
                XmlText text5 = doc.CreateTextNode(tbTransacaoXml.chave);
                element5.AppendChild(text5);
                element1.AppendChild(element5);

                XmlElement element6 = doc.CreateElement(string.Empty, "credor", string.Empty);
                XmlText text6 = doc.CreateTextNode(tbTransacaoXml.credor);
                element6.AppendChild(text6);
                element1.AppendChild(element6);

                XmlElement element7 = doc.CreateElement(string.Empty, "valor", string.Empty);
                XmlText text7 = doc.CreateTextNode(tbTransacaoXml.valor.ToString());
                element7.AppendChild(text7);
                element1.AppendChild(element7);

                XmlElement element8 = doc.CreateElement(string.Empty, "datavencimento", string.Empty);

                String dtVenc = tbTransacaoXml.dataVencimento.Value.Year.ToString(format: "0000") + "-" +
                    tbTransacaoXml.dataVencimento.Value.Month.ToString(format: "00") + "-" +
                    tbTransacaoXml.dataVencimento.Value.Day.ToString(format: "00");

                XmlText text8 = doc.CreateTextNode(dtVenc);
                element8.AppendChild(text8);
                element1.AppendChild(element8);

                XmlElement element9 = doc.CreateElement(string.Empty, "descricao", string.Empty);
                XmlText text9 = doc.CreateTextNode(tbTransacaoXml.descricao);
                element9.AppendChild(text9);
                element1.AppendChild(element9);

                XmlElement element10 = doc.CreateElement(string.Empty, "cpf", string.Empty);
                XmlText text10 = doc.CreateTextNode(tbTransacaoXml.cnpj);
                element10.AppendChild(text10);
                element1.AppendChild(element10);

                XmlElement element11 = doc.CreateElement(string.Empty, "cep", string.Empty);
                XmlText text11 = doc.CreateTextNode(tbTransacaoXml.cep);
                element11.AppendChild(text11);
                element1.AppendChild(element11);

                XmlElement element12 = doc.CreateElement(string.Empty, "cidade", string.Empty);
                XmlText text12 = doc.CreateTextNode(tbTransacaoXml.cidade);
                element12.AppendChild(text12);
                element1.AppendChild(element12);

                XmlElement element13 = doc.CreateElement(string.Empty, "estado", string.Empty);
                XmlText text13 = doc.CreateTextNode(tbTransacaoXml.estado);
                element13.AppendChild(text13);
                element1.AppendChild(element13);

                XmlElement element14 = doc.CreateElement(string.Empty, "bairro", string.Empty);
                XmlText text14 = doc.CreateTextNode(tbTransacaoXml.bairro);
                element14.AppendChild(text14);
                element1.AppendChild(element14);

                XmlElement element15 = doc.CreateElement(string.Empty, "endereco", string.Empty);
                XmlText text15 = doc.CreateTextNode(tbTransacaoXml.endereco);
                element15.AppendChild(text15);
                element1.AppendChild(element15);

                XmlElement element16 = doc.CreateElement(string.Empty, "numero", string.Empty);
                XmlText text16 = doc.CreateTextNode(tbTransacaoXml.numero);
                element16.AppendChild(text16);
                element1.AppendChild(element16);

                XmlElement element17 = doc.CreateElement(string.Empty, "complemento", string.Empty);
                XmlText text17 = doc.CreateTextNode(tbTransacaoXml.complemento);
                element17.AppendChild(text17);
                element1.AppendChild(element17);

                XmlElement element18 = doc.CreateElement(string.Empty, "nmaximoparcelas", string.Empty);
                XmlText text18 = doc.CreateTextNode(tbTransacaoXml.numMaximoParcelas.ToString());
                element18.AppendChild(text18);
                element1.AppendChild(element18);

                XmlElement element19 = doc.CreateElement(string.Empty, "campolivre", string.Empty);
                XmlText text19 = doc.CreateTextNode(tbTransacaoXml.campoLivre);
                element19.AppendChild(text19);
                element1.AppendChild(element19);

                XmlElement element20 = doc.CreateElement(string.Empty, "urlretornoloja", string.Empty);
                XmlText text20 = doc.CreateTextNode(tbTransacaoXml.urlRetornoLoja);
                element20.AppendChild(text20);
                element1.AppendChild(element20);

                doc.Save(Server.MapPath("~/Arquivos/XmlPagamento/Transacao_" + tbTransacaoXml.sid + ".xml"));

            }
            catch (Exception ex)
            {
                ProcFunc.SalvarLog(ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName()), ex.Message, 
                    this.ControllerContext.ToString(), "Gerar Arquivo XML " + tbTransacaoXml.sid);
                throw;
            }
        }


        public Boolean ConsultarTransacao(string sid)
        {
            try
            {
                //Tipo = 0 (consultar)  |  Tipo = 1 (criartransacao)
                var doc = new XmlDocument();

                //(1) the xml declaration is recommended, but not mandatory
                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = doc.DocumentElement;
                doc.InsertBefore(xmlDeclaration, root);

                XmlElement element1 = doc.CreateElement(string.Empty, "xml", string.Empty);
                doc.AppendChild(element1);

                XmlElement element2 = doc.CreateElement(string.Empty, "transacao", string.Empty);
                XmlText text2 = doc.CreateTextNode("consultar");
                element2.AppendChild(text2);
                element1.AppendChild(element2);

                XmlElement element3 = doc.CreateElement(string.Empty, "sid", string.Empty);
                XmlText text3 = doc.CreateTextNode(sid);
                element3.AppendChild(text3);
                element1.AppendChild(element3);

                doc.Save(Server.MapPath("~/Arquivos/XmlPagamento/Consultar_" + sid + ".xml"));

                return true;
            }
            catch (Exception ex)
            {
                ProcFunc.SalvarLog(ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName()), ex.Message, 
                    this.ControllerContext.ToString(), "Consultar Transacao "+sid);
                return false;
            }          
        }


        public void post_data(string sid, int tipo)
        {
            //Tipo = 0 (consultar)  |  Tipo = 1 (criartransacao)

            Stream requestStream = null;
            WebResponse response = null;
            StreamReader reader = null;

            string url = "http://pagsicove.host-up.com/ws/";

            WebRequest request = WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Post;
            request.ContentType = "application/x-www-form-urlencoded";

            byte[] byteBuffer = null;
            var fileName = "";

            if (tipo == 0) //Consultar
                fileName = Server.MapPath("~/Arquivos/XmlPagamento/Consultar_" + sid + ".xml");
            else //criartransacao
            {
                fileName = Server.MapPath("~/Arquivos/XmlPagamento/Transacao_" + sid + ".xml");
            }

            var stringXml = this.GetTextFromXMLFile(fileName);
            var postData = String.Format("mensagem={0}", stringXml);

            byteBuffer = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = byteBuffer.Length;
            requestStream = request.GetRequestStream();
            requestStream.Write(byteBuffer, 0, byteBuffer.Length);
            requestStream.Close();


            response = request.GetResponse();
            Stream responseStream = response.GetResponseStream();

            System.Text.Encoding encoding = System.Text.Encoding.Default;
            if (responseStream != null)
                reader = new StreamReader(responseStream, encoding);

            Char[] charBuffer = new Char[256];

            int count = reader.Read(charBuffer, 0, charBuffer.Length);

            var Dados = new StringBuilder();

            while (count > 0)
            {
                Dados.Append(new String(charBuffer, 0, count));
                count = reader.Read(charBuffer, 0, charBuffer.Length);
            }

            Response.Write(Dados.ToString());
        }


        private string GetTextFromXMLFile(string file)
        {
            var reader = new StreamReader(file);
            string ret = reader.ReadToEnd();
            reader.Close();
            return ret;
        }


        private bool VerificaFaturaPaga(string sid)
        {
            if (ConsultarTransacao(sid))
                post_data(sid, 0); //Consultar Transação
            return false;
        }


        #region "Criptografar Sid"
        public static string CriptografarSid(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }
        #endregion


        #region "Validar Sid"
        static bool ValidarSid(MD5 md5Hash, string input, string hash)
        {
            // Hash the input. 
            string hashOfInput = CriptografarSid(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

	}
}