using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using BLToolkit.Data.Linq;
using BLToolkit.Data.Sql;
using Cliquemix.Models;
using Microsoft.AspNet.Identity;
using System.Net;

namespace Cliquemix.Controllers
{
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

            // Converte a string para um valor DateTime
            DateTime dtInicio = DateTime.Parse(Request.Form.Get("dtInicio"), culturaAmericana);
            DateTime dtTermino = DateTime.Parse(Request.Form.Get("dtTermino"), culturaAmericana);
            DateTime hrInicio = DateTime.Parse(Request.Form.Get("hrInicio"), culturaAmericana);
            DateTime hrTermino = DateTime.Parse(Request.Form.Get("hrTermino"), culturaAmericana);

            tbCampanha.dtInicio = new DateTime(dtInicio.Year, dtInicio.Month, dtInicio.Day, hrInicio.Hour, hrInicio.Minute, hrInicio.Second);
            tbCampanha.dtTermino = new DateTime(dtTermino.Year, dtTermino.Month, dtTermino.Day, hrTermino.Hour, hrTermino.Minute, hrTermino.Second);
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
            var tbcampanha = db.tbCampanha.Include(t => t.tbCampanhaStatus).Include(r => r.tbDestaque);
            return View(tbcampanha.ToList());
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
                var tbDestaque = db.tbDestaque.Where(d => d.did == pCodDestaque);
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
                    var tbanuncio = db.tbAnuncio.Include(t => t.tbRamoAtividade).Include(r => r.tbAnuncioStatus).
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
                        var tbanuncio = db.tbAnuncio.Include(t => t.tbRamoAtividade).Include(r => r.tbAnuncioStatus).
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
                        var tbanuncio = db.tbAnuncio.Include(t => t.tbRamoAtividade).Include(r => r.tbAnuncioStatus).
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
        public ActionResult Delete(int campanha)
        {
            List<tbCampanhaAnuncio> campanhaAnuncio = db.tbCampanhaAnuncio.Where(m => m.ctid == campanha).ToList();

            foreach (var item in campanhaAnuncio)
            {
                db.tbCampanhaAnuncio.Remove(item);
                db.SaveChanges();
            }
            return null;
        }

/*
        [DataObjectMethod(DataObjectMethodType.Delete)]
        public void DeleteEmployee(Employee z)
        public void Delete(int campanha)
        {
            using (var ctx = new tbCampanhaAnuncio())
            {
                var x = (from y in ctx.Employees
                         where y.EmployeeId == z.EmployeeId
                         select y).FirstOrDefault();
                ctx.DeleteObject(x);
                ctx.SaveChanges();
            }

            try
            {
                var a = (from ca in db.tbCampanhaAnuncio where ca.ctid == campanha select ca).ToList();
                db.tbCampanhaAnuncio.Delete(m => m.ctid == campanha);
                db.SaveChanges();
                db.tbCampanhaAnuncio.Delete();
                return null;
            }
            catch (Exception e)
            {
                return null;
                throw;
            }
        }

/***/
    }
}