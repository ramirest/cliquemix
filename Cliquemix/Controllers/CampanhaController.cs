﻿using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cliquemix.Models;
using Microsoft.AspNet.Identity;

namespace Test_AspNet.Controllers
{
    public class CampanhaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Campanha
        public ActionResult CreateCampanha()
        {
            ViewBag.pcid = new SelectList(db.tbPacoteClique, "pcid", "qtdeCliques");
            ViewBag.codCampanha = 0;
            return View();
        }

        // POST: Campanha
        // POST: /Anuncio/CreateAnuncio
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCampanha([Bind(Include = "tituloCampanha, dtInicio, hrInicio, dtTermino, hrTermino, pcid")] tbCampanha tbCampanha)
        {
            if (ModelState.IsValid)
            {
                db.tbCampanha.Add(tbCampanha);
                db.SaveChanges();

                return RedirectToAction("ListCampanha");
            }
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
        {
            try
            {
                var tbCampanhaAnuncio = db.tbCampanhaAnuncio.Where(m => m.cid == pCodCampanha);
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
    }
}