using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Cliquemix.Models;
using Microsoft.AspNet.Identity;

namespace Cliquemix.Controllers
{
    public class CampanhaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Campanha
        public ActionResult CreateCampanha()
        {
            ViewBag.pcid = new SelectList(db.tbPacoteClique, "pcid", "qtdeCliques");
            ViewBag.did = new SelectList(db.tbDestaque, "did", "tituloDestaque");
            ViewBag.ctid = ProcFunc.CriarCodTempCampanha(Session.SessionID);
            return View();
        }


        // POST: /Anuncio/CreateCampanha
        [HttpPost]
        [ValidateAntiForgeryToken]
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
                //var tbCampanhaAnuncio = db.tbCampanhaAnuncio;
                if (tbCampanhaAnuncio.Any())
                {
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

        [HttpGet]
        public ActionResult NovoAnuncioCampanha(int pCodTempCampanha)
        {
            int codAnunciante = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
            int codStatus = ProcFunc.RetornarStatusPadraoAnuncioDisponivelParaCampanha();
            var tbanuncio = db.tbAnuncio.Include(t => t.tbRamoAtividade).Include(r => r.tbAnuncioStatus).
                Where(a => a.pid == codAnunciante);//.Where(m => m.asid == codStatus);

            if (tbanuncio.Any())
            {
                ViewBag.ctid = pCodTempCampanha;
                return PartialView(tbanuncio.ToList());
            }
            else
            {
                return PartialView();
            }
        }

        [HttpPost]
        public ActionResult NovoAnuncioCampanha(int taid, int ctid)
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
                        //return null;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}