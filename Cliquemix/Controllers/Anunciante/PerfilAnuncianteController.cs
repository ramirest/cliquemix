using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test_AspNet.Controllers.Anunciante
{
    public class PerfilAnuncianteController : Controller
    {
        //
        // GET: /PerfilAnunciante/
        public ActionResult PerfilAnunciante()
        {
            return View();
        }

        public ActionResult PerfilAnunciantePrincipal()
        {
            return PartialView();
        }


        public ActionResult EditarPerfilDadosEmpresa()
        {
            return View();
        }

        public ActionResult EditarPerfilDadosContato()
        {
            return View();
        }

        public ActionResult EditarPerfilDadosEndereco()
        {
            return View();
        }

        public ActionResult EditarPerfilDadosUsuario()
        {
            return View();
        }
	}
}