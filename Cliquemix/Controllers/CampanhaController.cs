using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BLToolkit.Data.Linq;
using BLToolkit.Data.Sql;
using Cliquemix.Models;
using Microsoft.AspNet.Identity;
using System.Net;
using PagedList;

namespace Cliquemix.Controllers
{
    [Authorize]
    public class CampanhaController : Controller
    {
        //CÓDIGO EXCEPTION [CAMPANHA] 001
        private ApplicationDbContext db = new ApplicationDbContext();

        internal static string TipoErro { get; set; }
        
        // GET: /Campanha/ListCampanha
        public ActionResult ListCampanha(int? pagina) //CÓDIGO EXCEPTION [ListCampanha] 00001
        {
            int paginaTamanho = 6; //Define o número de elementos por página
            int paginaNumero = (pagina ?? 1); //Define o número inicial da página como 1
            var codAnunciante = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
            var cpec = ProcFunc.RetornarStatusPadraoCampanhaExcluida();
            ViewBag.ErroCriarCampanha = TipoErro;

            var tbcampanha = db.tbCampanha.Include(a => a.tbAnunciante).Include(s => s.tbCampanhaStatus).
                Include(c => c.tbCampanhaTmp).Include(d => d.tbDestaqueAnunciante).Include(p => p.tbPacoteClique).
                Where(m => m.pid == codAnunciante && m.csid != cpec).ToList();
            TipoErro = string.Empty;
            return View(tbcampanha.ToPagedList(paginaNumero, paginaTamanho));
        }
        

        // GET: Campanha
        public ActionResult CreateCampanha()
        {
            try
            {
                var cdAnun = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
                var spdad = ProcFunc.RetornarStatusPadraoDestaqueAnuncianteDisponivel();
                var s = ProcFunc.RetornarStatusPadraoAnuncio();
                var ad = db.tbAnuncio.Where(a => a.asid == s && a.pid == cdAnun);

                if (!ad.Any())//CÓDIGO EXCEPTION [CreateCampanha] 00002
                {   //Não tem anúncios disponíveis
                    TipoErro = "001.00004";
                    return RedirectToAction("ListCampanha");
                }

                var da = db.tbDestaqueAnunciante.Where(d => d.pid == cdAnun && d.dasid == spdad)
                         .OrderBy(d => d.tbDestaque.tituloDestaque);
                if (!da.Any())//CÓDIGO EXCEPTION [CreateCampanha] 00002
                {   //Não tem destaques disponíveis
                    TipoErro = "001.00002";
                    return RedirectToAction("ListCampanha");
                }

                /*  CASO OS DESTAQUES SEJAM AGRUPADOS, UTILIZE ESTE
                 * 
                var destaques = from de in db.tbDestaque
                    join dea in db.tbDestaqueAnunciante on de.did equals dea.did
                    join an in db.tbAnunciante on dea.pid equals an.pid
                    where an.pid == cdAnun && dea.dasid == spdad
                    group de by new {de.did, de.tituloDestaque} into destaque
                    select new
                    {
                        destaque.Key.did,
                        destaque.Key.tituloDestaque
                    };
                ViewBag.daid = new SelectList(destaques.OrderBy(d => d.tituloDestaque), "daid", "tituloDestaque");
                */

                //CASO CONTRÁRIO ESTE:
                ViewBag.daid = new SelectList(da, "daid", "tbDestaque.tituloDestaque");

                ViewBag.pcid = new SelectList(db.tbPacoteClique.OrderBy(p => p.qtdeCliques), "pcid", "qtdeCliques");
                ViewBag.ctid = ProcFunc.CriarCodTempCampanha(Session.SessionID);
                ViewBag.paid = new SelectList(db.tbPais.OrderBy(p => p.nomePais), "paid", "nomePais");
                ViewBag.eid = new SelectList(db.tbEstado.OrderBy(e => e.nomeEstado), "eid", "sgEstado");
                ViewBag.cid = new SelectList(db.tbCidade.OrderBy(c => c.nomeCidade), "cid", "nomeCidade");
                ViewBag.Tudo = 1;
                return View();
            }
            catch (Exception)
            {//CÓDIGO EXCEPTION [CreateCampanha] 00003
                TipoErro = "001.00003";
                return RedirectToAction("ListCampanha");
            }
        }


        // POST: /Anuncio/CreateCampanha
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CreateCampanha([Bind(Include = "cid,tituloCampanha,daid,pcid")] tbCampanha tbCampanha)
        {
            
            // Cria uma instância com as informações da cultura americana
            CultureInfo culturaAmericana = new CultureInfo("en-US");
            /*
            // Converte a string para um valor DateTime
            DateTime dtInicio = DateTime.Parse(Request.Form.Get("dtInicio"), culturaAmericana);
            DateTime dtTermino = DateTime.Parse(Request.Form.Get("dtTermino"), culturaAmericana);
            DateTime hrInicio = DateTime.Parse(Request.Form.Get("hrInicio"), culturaAmericana);
            DateTime hrTermino = DateTime.Parse(Request.Form.Get("hrTermino"), culturaAmericana);
            */
            if (ProcFunc.RetornarInicioTerminoPadraoPublicacaoCampanha() == 0) // 0 = Iniciar ao Publicar e Término no fim dos cliques
            {
                tbCampanha.dtInicio = DateTime.Now;
                tbCampanha.dtTermino = new DateTime();
            }
            else if (ProcFunc.RetornarInicioTerminoPadraoPublicacaoCampanha() == 2) // 2 = Definido manualmente pelos administradores
            {
                tbCampanha.dtInicio = new DateTime();
                tbCampanha.dtTermino = new DateTime();                
            }
            else if (ProcFunc.RetornarInicioTerminoPadraoPublicacaoCampanha() == 3) // 3 = Informar somente o Início e Término no fim dos cliques
            {
                DateTime dtInicio = DateTime.Parse(Request.Form.Get("dtInicio"));
                DateTime hrInicio = DateTime.Parse(Request.Form.Get("hrInicio"));
                tbCampanha.dtInicio = new DateTime(dtInicio.Year, dtInicio.Month, dtInicio.Day, hrInicio.Hour, hrInicio.Minute, hrInicio.Second);
                var QtdeDiasTermino = ProcFunc.RetornarQtdeDiasPadraoTerminoCampanha();
                tbCampanha.dtTermino = tbCampanha.dtInicio.AddDays(QtdeDiasTermino);
            }
            else if (ProcFunc.RetornarInicioTerminoPadraoPublicacaoCampanha() == 4) // 4 = Informar somente o Término e Início ao publicar
            {
                tbCampanha.dtInicio = DateTime.Now;
                DateTime dtTermino = DateTime.Parse(Request.Form.Get("dtInicio"));
                DateTime hrTermino = DateTime.Parse(Request.Form.Get("hrInicio"));
                tbCampanha.dtTermino = new DateTime(dtTermino.Year, dtTermino.Month, dtTermino.Day, hrTermino.Hour, hrTermino.Minute, hrTermino.Second);
            }

            //Verifica se a data inicial informada é maior que a data atual
            DateTime Inicio = new DateTime(tbCampanha.dtInicio.Year, tbCampanha.dtInicio.Month, tbCampanha.dtInicio.Day);
            DateTime dtAtual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            //Caso positivo o sistema seta para a configuração padrão de campanha programada
            if (Inicio > dtAtual)
                tbCampanha.csid = ProcFunc.RetornarStatusPadraoCampanhaProgramada();
            //Caso a data informada seja igual a data atual o sistema seta para a configuração padrão de nova campanha
            else if (Inicio == dtAtual)
                tbCampanha.csid = ProcFunc.RetornarStatusPadraoCampanha();
            //Caso negativo o sistema retorna para a tela principal solicitando uma alteração de data
            else
            {
                tbCampanha.csid = ProcFunc.RetornarStatusPadraoCampanha();
                tbCampanha.dtInicio = DateTime.Now;
            }

            tbCampanha.pid = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
            tbCampanha.ctid = Convert.ToInt32(Request.Form.Get("ctid"));
            tbCampanha.pcid = db.tbDestaqueAnunciante.First(d => d.daid == tbCampanha.daid).tbDestaque.tbPacoteClique.pcid;
            tbCampanha.QtdeCreditosInicio = db.tbDestaqueAnunciante.First(d => d.daid == tbCampanha.daid).tbDestaque.qtCredito;
            tbCampanha.QtdeCreditosConsumidos = 0;

            if (ModelState.IsValid)
            {
                db.tbCampanha.Add(tbCampanha);
                db.SaveChanges();
                ProcFunc.AtualizarCodTempAnunciosCampanha(tbCampanha.ctid, tbCampanha.cid, tbCampanha.tbDestaqueAnunciante.daid);
                ProcFunc.UtilizarDestaqueAnuncianteCampanha(tbCampanha.pid, tbCampanha.daid);
                var cda = tbCampanha.tbCampanhaAnuncio.First(c => c.cid == tbCampanha.cid).aid;
                ProcFunc.AnuncioNaCampanha(cda);
                //Salvar Log do Sistema
                ProcFunc.SalvarLog(ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName()),
                    "Cadastro de uma nova Campanha ["+tbCampanha.cid.ToString(format: "000000")+"]", this.ToString(), "POST");
                
                return RedirectToAction("ListCampanha");
            }
            var cdAnun = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());/*
            var destaques = from de in db.tbDestaque
                            join dea in db.tbDestaqueAnunciante on de.did equals dea.did
                            join an in db.tbAnunciante on dea.pid equals an.pid
                            where an.pid == cdAnun && dea.dasid == spdad
                            group de by new { dea.daid, de.tituloDestaque } into destaque
                            select new
                            {
                                destaque.Key.daid,
                                destaque.Key.tituloDestaque
                            };
            ViewBag.daid = new SelectList(destaques.OrderBy(d => d.tituloDestaque), "daid", "tituloDestaque");*/
            ViewBag.pcid = new SelectList(db.tbPacoteClique, "pcid", "qtdeCliques");
            return View(tbCampanha);
        }


        [HttpGet]
        public ActionResult AnunciosCampanha(int pCodCampanha)
        //public ActionResult AnunciosCampanha()
        {
            try
            {
                var tbCampanhaAnuncio = db.tbCampanhaAnuncio.Where(m => m.ctid == pCodCampanha);
                if (tbCampanhaAnuncio.Any())
                {
                    var cdAnun = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
                    try
                    {
                        var tipo = ProcFunc.RetornarTipoAnunciante(cdAnun);
                        ViewBag.patrocinador = tipo.ToString();
                    }
                    catch (Exception)
                    {
                        ViewBag.patrocinador = 0;
                    }

                    ViewBag.Tudo = 2;
                    ViewBag.CodCampanha = pCodCampanha;
                    return PartialView(tbCampanhaAnuncio.ToList());
                }
                else
                {
                    return PartialView();
                }
            }
            catch (Exception)
            {
                return PartialView();
                throw;
            }
        }

        [HttpGet]
        public ActionResult LocalidadesCampanha(int pCodCampanha)
        {/*
            try
            {
                var tbCampanhaAnuncio = db.tbCampanhaAnuncio.Where(m => m.ctid == pCodCampanha);
                if (tbCampanhaAnuncio.Any())
                {
                    ViewBag.Tudo = 2;
                    ViewBag.CodCampanha = pCodCampanha;
                    return PartialView(tbCampanhaAnuncio.ToList());
                }
                else
                {
                    return PartialView();
                }
            }
            catch (Exception)
            {
                return PartialView();
                throw;
            }*/
            return PartialView();
        }

        public ActionResult DeleteCampanha()
        {
            return View();
        }

        // GET: /Anuncio/Details/5
        public ActionResult VisualizarCampanha(int? id)
        {
            var cdAnun = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCampanha tbCampanha = db.tbCampanha.Find(id);
            if (tbCampanha == null)
            {
                return HttpNotFound();
            }
            if (tbCampanha.pid == cdAnun)
            {
                ViewBag.pcid = new SelectList(db.tbPacoteClique.OrderBy(p => p.qtdeCliques), "pcid", "qtdeCliques");

                var destaques = from de in db.tbDestaque
                                join dea in db.tbDestaqueAnunciante on de.did equals dea.did
                                join an in db.tbAnunciante on dea.pid equals an.pid
                                where an.pid == cdAnun && dea.dasid == ProcFunc.RetornarStatusPadraoDestaqueAnuncianteDisponivel()
                                group de by new { dea.daid, de.tituloDestaque } into destaque
                                select new
                                {
                                    destaque.Key.daid,
                                    destaque.Key.tituloDestaque
                                };
                ViewBag.daid = new SelectList(destaques.OrderBy(d => d.tituloDestaque), "daid", "tituloDestaque");
                
                //Consultar e retornar a quantidade de créditos restante
                try
                {   ViewBag.CreditosRestante = tbCampanha.QtdeCreditosInicio - tbCampanha.QtdeCreditosConsumidos;   }
                catch (Exception)
                {   ViewBag.CreditosRestante = 0;   }

                //Consultar e retornar a quantidade de cliques já efetuados
                try
                {   ViewBag.CliquesEfetuados = db.tbCampanhaAnuncio.First(m => m.cid == tbCampanha.cid).contCliquesAtual;   }
                catch (Exception)
                {   ViewBag.CliquesEfetuados = db.tbCampanhaAnuncio.First(m => m.cid == tbCampanha.cid).contCliquesAtual;   }

                //Consultar e retornar a quantidade de cliques total e subtrair com a quantidade de cliques atual
                try
                {   ViewBag.CliquesRestantes = db.tbCampanhaAnuncio.First(m => m.cid == tbCampanha.cid).contCliquesFinal -
                        ViewBag.CliquesEfetuados;    }
                catch (Exception)
                {   ViewBag.CliquesRestantes = 0;   }
                
                return View(tbCampanha);
            }
            else
                return HttpNotFound();
        }


        [HttpGet]
        public ActionResult InfoDestaque()
        {
            return PartialView();
        }


        [HttpPost]
        public ActionResult InfoDestaque(int pCodDestaqueAnunciante)
        {
            try
            {
                var tbDestaque = db.tbDestaqueAnunciante.Where(c => c.daid == pCodDestaqueAnunciante);
                if (tbDestaque.Any())
                {
                    return PartialView(tbDestaque.ToList());
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
        public ActionResult NovoAnuncio(int taid, int ctid)
        {
            var cdAnun = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
            try
            {
                tbCampanhaAnuncio tbCampanhaAnuncio = new tbCampanhaAnuncio();
                if (ProcFunc.VerificarAnuncioDisponivelParaCampanha(taid))
                {
                    tbCampanhaAnuncio.aid = taid;
                    tbCampanhaAnuncio.ctid = ctid;
                    tbCampanhaAnuncio.casid = ProcFunc.RetornarStatusPadraoAnuncioDisponivelParaCampanha();
                    tbCampanhaAnuncio.dtMovimento = DateTime.Now;

                    if (ModelState.IsValid)
                    {
                        db.tbCampanhaAnuncio.Add(tbCampanhaAnuncio);
                        db.SaveChanges();
                        ViewBag.Tudo = 3;
                    }
                    return null;
                }
                else
                {
                    int codStatus = ProcFunc.RetornarStatusPadraoAnuncioDisponivelParaCampanha();
                    var tbanuncio = db.tbAnuncio.Include(t => t.tbAnuncioCategoria).Include(r => r.tbAnuncioStatus).
                        Where(a => a.pid == cdAnun).Where(m => m.asid == codStatus).ToList();
                    if (tbanuncio.Any())
                    {
                        ViewBag.ctid = ctid;
                        return PartialView(tbanuncio);
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        [HttpGet]
        public ActionResult NovoAnuncio(string txt, int pCodCampanha)
        {
            var cdAnun = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
            try
            {
                if (ProcFunc.CampanhaContemAnuncio(pCodCampanha))
                {
                    //Já existe anúncio vinculado na campanha
                    ViewBag.Tudo = 2;
                    return PartialView(null);
                }
                else if (!ProcFunc.ExisteAnuncioParaVincular(cdAnun))
                {
                    //Não existe anúncio disponível para ser vinculado
                    ViewBag.Tudo = 4;
                    return PartialView(null);
                }
                else
                {
                    var codStatus = ProcFunc.RetornarStatusPadraoAnuncioDisponivelParaCampanha();
                    try
                    {
                        var tipo = ProcFunc.RetornarTipoAnunciante(cdAnun);
                        ViewBag.patrocinador = tipo.ToString();
                    }
                    catch (Exception)
                    {
                        ViewBag.patrocinador = 0;
                    }

                    if (txt == null)
                    {
                        var tbanuncio = db.tbAnuncio.Include(t => t.tbAnuncioCategoria).Include(r => r.tbAnuncioStatus).
                            Where(a => a.pid == cdAnun).Where(m => m.asid == codStatus).ToList();
                        if (tbanuncio.Any())
                        {
                            ViewBag.Tudo = 1;
                            ViewBag.ctid = pCodCampanha;
                            return PartialView(tbanuncio);
                        }
                        else
                        {
                            ViewBag.Tudo = 4;
                            ViewBag.ctid = pCodCampanha;
                            return PartialView(null);
                        }
                    }
                    else
                    {
                        var tbanuncio = db.tbAnuncio.Include(t => t.tbAnuncioCategoria).Include(r => r.tbAnuncioStatus).
                            Where(a => a.pid == cdAnun).Where(m => m.tituloAnuncio.Contains(txt)).ToList();
                        if (tbanuncio.Any())
                        {
                            ViewBag.Tudo = 1;
                            ViewBag.ctid = pCodCampanha;
                            return PartialView(tbanuncio);
                        }
                        else
                        {
                            ViewBag.Tudo = 4;
                            ViewBag.ctid = pCodCampanha;
                            return PartialView(null);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Tudo = 5;
                ProcFunc.SalvarLog(cdAnun, e.Message, this.ToString(), "Exception");
                return PartialView(null);
            }
        }

        
        // POST: CampanhaAnuncio/Delete/5
        [HttpPost]
        public ActionResult DeleteAnuncio(int campanha)
        {
            List<tbCampanhaAnuncio> campanhaAnuncio = db.tbCampanhaAnuncio.Where(m => m.ctid == campanha).ToList();

            foreach (var item in campanhaAnuncio)
            {
                db.tbCampanhaAnuncio.Remove(item);
                db.SaveChanges();
            }
            return null;
        }

        // POST: CampanhaLocalizacao/DeleteLocalidade/5
        [HttpPost]
        public ActionResult DeleteLocalidade(int pCodLocalidade)
        {
            List<tbCampanhaLocalizacao> campanhaLocalidade = db.tbCampanhaLocalizacao.Where(m => m.clid == pCodLocalidade).ToList();

            foreach (var item in campanhaLocalidade)
            {
                db.tbCampanhaLocalizacao.Remove(item);
                db.SaveChanges();
            }
            return null;
        }

        [HttpGet]
        public ActionResult GeoLocalizacao(int pCodCampanha)
        {
            var tbcidade = db.tbCidade.Include(t => t.tbEstado).ToList();
            return PartialView();
        }

        [HttpPost]
        public ActionResult GeoLocalizacao(string cidade)
        {
            var tbcidade = db.tbCidade.Include(t => t.tbEstado).Where(m => m.nomeCidade.Contains(cidade)).ToList();
            return PartialView(tbcidade);
        }

        // POST: /Campanha/DesativarCampanha/5
        [HttpPost]
        public ActionResult DesativarCampanha(int id)
        {
            tbCampanha tbCampanha = db.tbCampanha.Find(id);
            tbCampanha.csid = ProcFunc.RetornarStatusPadraoCampanhaDesativada();
            db.Entry(tbCampanha).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ListCampanha");
        }


    }
}



/*  BACKUP GeoLocalizaçãoBackup

        [HttpGet]
        public ActionResult GeoLocalizacao()
        {
            var iEstado = new List<SelectListItem>();
            var iCidade = new List<SelectListItem>();

            foreach (var iUf in db.tbEstado)
            {
                iEstado.Add(new SelectListItem
                {
                    Text = iUf.sgEstado,
                    Value = iUf.eid.ToString(),
                    Selected = false
                });
            }
            
            foreach (var iCid in db.tbCidade.Where(c => c.eid == 14))
            {
                iCidade.Add(new SelectListItem
                {
                    Text = iCid.nomeCidade,
                    Value = iCid.cid.ToString(),
                    Selected = false
                });
            }
            

            ViewBag.estados = iEstado.OrderBy(e => e.Text).ToList();
            ViewBag.cidades = iCidade.OrderBy(e => e.Text).ToList();
            return PartialView();
        }

        [HttpPost]
        public ActionResult GeoLocalizacao(string estados)
        {
            var iCidade = new List<SelectListItem>();
            string[] array = estados.Split(',');
            foreach (string value in array)
            {
                var a = Convert.ToInt32(value);
                Convert.ToInt32(value);
                foreach (var iCid in db.tbCidade.Where(c => c.eid == a))
                {
                    iCidade.Add(new SelectListItem
                    {
                        Text = iCid.nomeCidade,
                        Value = iCid.nomeCidade,
                        Selected = false
                    });
                }
            }
            ViewBag.cidades = iCidade.OrderBy(e => e.Text).ToList();
            return PartialView();
            //return null;
        }
*/