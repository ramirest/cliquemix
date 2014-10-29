using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using Cliquemix.Models;
using Microsoft.AspNet.Identity;

namespace Cliquemix.Controllers.Admin
{
    //Somente usuários com a permissão Administrador podem acessar essa página
    [PermissoesFiltro(Roles = "Administrador")]
    public class PrincipalAdminController : Controller
    {
        //
        // GET: /PrincipalAdmin/
        public ActionResult PrincipalAdmin()
        {
            return View();
        }
	}
}