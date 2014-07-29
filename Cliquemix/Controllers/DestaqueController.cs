using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cliquemix.Controllers
{
    public class DestaqueController : Controller
    {
        //
        // GET: /Destaque/
        public ActionResult EscolheDestaque()
        {
            return View();
        }

        public ActionResult ListDestaque()
        {
            return View();
        }
	}
}