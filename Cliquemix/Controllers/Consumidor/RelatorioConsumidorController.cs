using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cliquemix.Models;

namespace Cliquemix.Controllers.Consumidor
{
    public class RelatorioConsumidorController : Controller
    {
        //Somente usuários com a permissão Consumidor podem acessar essa página
        [PermissoesFiltro(Roles = "Consumidor")]
        // GET: /RelatorioConsumidor/
        public ActionResult RelConsumidorPrincipal()
        {
            return View();
        }

        public ActionResult RelConsumidorAnuncio()
        {
            return View();
        }

        public ActionResult RelConsumidorPremio()
        {
            return View();
        }

	}
}