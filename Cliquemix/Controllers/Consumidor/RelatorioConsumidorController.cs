using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test_AspNet.Controllers.Consumidor
{
    public class RelatorioConsumidorController : Controller
    {
        //
        // GET: /RelatorioConsumidor/
        public ActionResult RelConsPrincipal()
        {
            return View();
        }

        public ActionResult RelConsAnun()
        {
            return View();
        }

        public ActionResult RelConsPrem()
        {
            return View();
        }

	}
}