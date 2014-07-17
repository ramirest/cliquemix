using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cliquemix.Controllers.Anunciante
{
    public class RelatorioAnuncianteController : Controller
    {
        //
        // GET: /RelatorioAnunciante/

        public ActionResult RelAnunPrincipal()
        {
            return View();
        }
        
        public ActionResult RelAnun()
        {
            return View();
        }

        public ActionResult RelCamp()
        {
            return View();
        }

        public ActionResult RelMov()
        {
            return View();
        }

        public ActionResult RelPrem()
        {
            return View();
        }
	}
}