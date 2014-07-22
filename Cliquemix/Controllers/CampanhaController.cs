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
            tbCampanha.csid = ProcFunc.RetornaStatusPadraoCampanha();
            tbCampanha.pid = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
            if (ModelState.IsValid)
            {
                db.tbCampanha.Add(tbCampanha);
                db.SaveChanges();
                //SalvarLogAnuncio(tbanuncio.aid, (int)tbanuncio.asid, ProcFunc.RetornaCodigoUsuario(User.Identity.GetUserName()), "Sim");
                return RedirectToAction("ListCampanha");
            }
            ViewBag.pcid = new SelectList(db.tbPacoteClique, "pcid", "qtdCliques");
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
                var tbCampanhaAnuncio = db.tbCampanhaAnuncio.Where(m => m.cid == pCodCampanha);
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


        public ActionResult NovoAnuncioCampanha()
        {
            var tbanuncio = db.tbAnuncio.Include(t => t.tbRamoAtividade).Include(r => r.tbAnuncioStatus);
            if (tbanuncio.Any())
            {
                return PartialView(tbanuncio.ToList());
            }
            else
            {
                return PartialView();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NovoAnuncioCampanha(int codAnuncio)
        {
            //if ()
            return null;
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


    }
}