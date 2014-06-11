using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Cliquemix.Models;
using System.Threading.Tasks;

namespace Cliquemix.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            int iResult = 0;

            if (ModelState.IsValid)
            {
                if (model.UserIsValid(model.Username, model.Password))
                {
                    iResult = 1;
                    var _authTicket = new FormsAuthenticationTicket(iResult, model.Username.ToString() + "-" + Convert.ToString(1),
                        DateTime.Now, DateTime.Now.AddMinutes(30), model.Remember, model.Username.ToString());
                    var _encryptTicket = FormsAuthentication.Encrypt(_authTicket);
                    var _authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, _encryptTicket);
                    Response.Cookies.Add(_authCookie);
                    Session.Add(model.Username, iResult.ToString());
                    FormsAuthentication.RedirectFromLoginPage(_authTicket.UserData, model.Remember);
                }
                else
                {
                    model.Username = string.Empty;
                    model.Password = string.Empty;
                    ModelState.AddModelError("", "Usuário ou senha inválido!");
                }
            }
            return View();
        }
	}
}