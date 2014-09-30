using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cliquemix.Models;

namespace Cliquemix.Controllers.Demo
{
    public class PrincipalDemoController : Controller
    {
        //Somente usuários com a permissão Demo podem acessar essa página
        [PermissoesFiltro(Roles = "Demo")]
        // GET: /PrincipalDemo/PrincipalDemo
        public ActionResult PrincipalDemo()
        {
            return View();
        }
    }
}