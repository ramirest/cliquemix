using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cliquemix.Models;
using Microsoft.AspNet.Identity;

namespace Cliquemix.Controllers.Consumidor
{
    public class PrincipalConsumidorController : Controller
    {
        //Somente usuários com a permissão Consumidor podem acessar essa página
        [PermissoesFiltro(Roles = "Consumidor")]

        // GET: /PrincipalConsumidor/
        public ActionResult PrincipalConsumidor()
        {
            var cdUsu = ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName());
            ViewBag.QtdeAnunPontuados = ProcFunc.RetornarQtdeAnunciosPontuadosDashboardConsumidor(cdUsu);
            ViewBag.QtdeAnunVisualizados = ProcFunc.RetornarQtdeAnunciosVisualizadosDashboardAnunciante(cdUsu);
            ViewBag.QtdeAnunPontosAdquiridos = ProcFunc.RetornarQtdePontosAdquiridosDashboardAnunciante(cdUsu);
            return View();
        }



        // Página principal do site, onde serão exibidos os anúncios para pontuação
        // GET: /PrincipalConsumidor/
        public ActionResult PrincipalAnuncios()
        {
            return View();
        }
	}
}