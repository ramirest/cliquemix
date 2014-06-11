using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Test_AspNet.Controllers
{
    public class LogoutController : Controller
    {

        public ActionResult Logout()
        {
            return View();
        }
        
        // POST: /Logout/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout(bool sair)
        {
            if (sair)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("PrincipalAnunciante", "PrincipalAnunciante");
            }
            return View();
        }        

    }
}