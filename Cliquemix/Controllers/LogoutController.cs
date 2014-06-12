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

        // POST: /Logout/Sair
        [HttpPost, ActionName("Logout")]
        [ValidateAntiForgeryToken]
        public ActionResult Logout(bool sair)
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("PrincipalAnunciante", "PrincipalAnunciante");
        }

        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Response.Redirect("~/PrincipalAnunciante/PrincipalAnunciante");
        }

    }
}