using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cliquemix.Models;

namespace Test_AspNet.Controllers.Consumidor
{
    public class PrincipalConsumidorController : Controller
    {
        //Somente usuários com a permissão Consumidor podem acessar essa página
        [PermissoesFiltro(Roles = "Consumidor")]

        // GET: /PrincipalConsumidor/
        public ActionResult PrincipalConsumidor()
        {
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