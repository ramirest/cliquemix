﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cliquemix.Models;

namespace Cliquemix.Controllers
{
    [Authorize]
    public class CreditoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //
        // GET: /Credito/
        public ActionResult BuyCredit()
        {
            ViewBag.crid = new SelectList((from s in db.tbCredito.ToList()
                select new
                {
                    Crid = s.crid,
                    Credito = s.qtCredito +
                              " - " +
                              String.Format(new CultureInfo("pt-BR"), "{0:C}", s.vlCredito)
                }), "Crid", "Credito", null);

            //ViewBag.crid = new SelectList(db.tbCredito, "crid", "qtCredito");
            return View();
        }

        public ActionResult ManagerCredit()
        {
            return View();
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

	}
}