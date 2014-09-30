using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cliquemix.Models;

namespace Test_AspNet.Controllers
{
    //Somente usuários com a permissão Administrador podem acessar essa página
    [PermissoesFiltro(Roles = "Administrador")]
    public class BannerController : Controller
    {        
        // GET: /Banner/
        public ActionResult CreateBanner()
        {
            return View();
        }

        public ActionResult UpdateBanner()
        {
            return View();
        }

        public ActionResult DeleteBanner()
        {
            return View();
        }

        public ActionResult ListBanner()
        {
            return View();
        }
	}
}