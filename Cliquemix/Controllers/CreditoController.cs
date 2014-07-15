using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test_AspNet.Controllers
{
    public class CreditoController : Controller
    {
        //
        // GET: /Credito/
        public ActionResult BuyCredit()
        {
            return View();
        }

        public ActionResult ManagerCredit()
        {
            return View();
        } 
	}
}