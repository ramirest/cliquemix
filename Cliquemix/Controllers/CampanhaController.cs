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

namespace Cliquemix.Controllers
{
    [Authorize]
    public class CampanhaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Campanha
        public ActionResult CreateCampanha()
        {
            ViewBag.pcid = new SelectList(db.tbPacoteClique.OrderBy(p => p.qtdeCliques), "pcid", "qtdeCliques");
            ViewBag.did = new SelectList(db.tbDestaque.OrderBy(d => d.dsDestaque), "did", "tituloDestaque");
            ViewBag.ctid = ProcFunc.CriarCodTempCampanha(Session.SessionID);
            ViewBag.paid = new SelectList(db.tbPais.OrderBy(p => p.nomePais), "paid", "nomePais");
            ViewBag.eid = new SelectList(db.tbEstado.OrderBy(e => e.nomeEstado), "eid", "sgEstado");
            ViewBag.cid = new SelectList(db.tbCidade.OrderBy(c => c.nomeCidade), "cid", "nomeCidade");
            ViewBag.Tudo = 1;
            return View();
        }


        // POST: /Anuncio/CreateCampanha
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CreateCampanha([Bind(Include = "cid,tituloCampanha,did,pcid")] tbCampanha tbCampanha)
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
                DateTime dtInicio = DateTime.Parse(Request.Form.Get("dtInicio"), culturaAmericana);
                DateTime hrInicio = DateTime.Parse(Request.Form.Get("hrInicio"), culturaAmericana);
                tbCampanha.dtInicio = new DateTime(dtInicio.Year, dtInicio.Month, dtInicio.Day, hrInicio.Hour, hrInicio.Minute, hrInicio.Second);
                tbCampanha.dtTermino = new DateTime();
            }
            else if (ProcFunc.RetornarInicioTerminoPadraoPublicacaoCampanha() == 4) // 4 = Informar somente o Término e Início ao publicar
            {
                tbCampanha.dtInicio = DateTime.Now;
                DateTime dtTermino = DateTime.Parse(Request.Form.Get("dtInicio"), culturaAmericana);
                DateTime hrTermino = DateTime.Parse(Request.Form.Get("hrInicio"), culturaAmericana);
                tbCampanha.dtTermino = new DateTime(dtTermino.Year, dtTermino.Month, dtTermino.Day, hrTermino.Hour, hrTermino.Minute, hrTermino.Second);
            }
            tbCampanha.csid = ProcFunc.RetornarStatusPadraoCampanha();
            tbCampanha.pid = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
            tbCampanha.ctid = Convert.ToInt32(Request.Form.Get("ctid"));
            if (ModelState.IsValid)
            {
                db.tbCampanha.Add(tbCampanha);
                db.SaveChanges();

                ProcFunc.AtualizarCodTempAnunciosCampanha(tbCampanha.ctid, tbCampanha.cid);
                
                //Salvar Log do Sistema
                ProcFunc.SalvarLog(ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName()),
                    "Cadastro de uma nova Campanha", this.ToString(), "POST");
                
                return RedirectToAction("ListCampanha");
            }
            ViewBag.pcid = new SelectList(db.tbPacoteClique, "pcid", "qtdeCliques");
            ViewBag.did = new SelectList(db.tbDestaque, "did", "tituloDestaque");
            return View(tbCampanha);
        }


        public ActionResult UpdateCampanha()
        {
            return View();
        }

        // GET: /Campanha/ListCampanha
        public ActionResult ListCampanha()
        {
            var tbcampanha = db.tbCampanha.Include(t => t.tbCampanhaStatus).Include(r => r.tbDestaque).ToList();
            return View(tbcampanha);
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

        public ActionResult ViewerCampanha()
        {
            return View();
        }


        [HttpGet]
        public ActionResult InfoDestaque()
        {
            return PartialView();
        }


        [HttpPost]
        public ActionResult InfoDestaque(int pCodDestaque)
        {
            try
            {
                var tbDestaque = db.tbDestaque.Where(c => c.did == pCodDestaque);
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
            try
            {
                    tbCampanhaAnuncio tbCampanhaAnuncio = new tbCampanhaAnuncio();
                    if (ProcFunc.VerificarAnuncioDisponivelParaCampanha(taid))
                    {
                        tbCampanhaAnuncio.aid = taid;
                        //tbCampanhaAnuncio.cid = pCodTempCampanha;
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
                    int codAnunciante = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
                    int codStatus = ProcFunc.RetornarStatusPadraoAnuncioDisponivelParaCampanha();
                    var tbanuncio = db.tbAnuncio.Include(t => t.tbAnuncioCategoria).Include(r => r.tbAnuncioStatus).
                        Where(a => a.pid == codAnunciante); //.Where(m => m.asid == codStatus);
                    if (tbanuncio.Any())
                    {
                        ViewBag.ctid = ctid;
                        return PartialView(tbanuncio.ToList());
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
            try
            {
                if (ProcFunc.CampanhaContemAnuncio(pCodCampanha))
                {
                    ViewBag.Tudo = 2;
                    return PartialView(null);
                }
                else if (!ProcFunc.ExisteAnuncioParaVincular(
                    ProcFunc.RetornarCodigoAnuncianteUsuario(
                        User.Identity.GetUserName())))
                {
                    ViewBag.Tudo = 4;
                    return PartialView(null);
                }
                else
                {
                    var codAnunciante = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
                    var codStatus = ProcFunc.RetornarStatusPadraoAnuncioDisponivelParaCampanha();

                    if (txt == null)
                    {
                        var tbanuncio = db.tbAnuncio.Include(t => t.tbAnuncioCategoria).Include(r => r.tbAnuncioStatus).
                            Where(a => a.pid == codAnunciante); //.Where(m => m.asid == codStatus);
                        if (tbanuncio.Any())
                        {
                            ViewBag.Tudo = 1;
                            ViewBag.ctid = pCodCampanha;
                            return PartialView(tbanuncio.ToList());
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
                            Where(a => a.pid == codAnunciante).Where(m => m.tituloAnuncio.Contains(txt));
                        if (tbanuncio.Any())
                        {
                            ViewBag.Tudo = 1;
                            ViewBag.ctid = pCodCampanha;
                            return PartialView(tbanuncio.ToList());
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
                ProcFunc.SalvarLog(ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName()), e.Message, this.Url.ToString(), "Exception");
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

    }
}



/*

foreach (var item in Model)
            {
                IEnumerable<SelectListItem> items = ((IEnumerable<SelectListItem>)ViewData["CurrID"]).ToList().Select(c => new SelectListItem
                {
                    Value = c.Value.ToString(),
                    Text = c.Text.ToString(),
                    Selected = Convert.ToInt32(c.Value) == item.CurrID
                });
*/






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