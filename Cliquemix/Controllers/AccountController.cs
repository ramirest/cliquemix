using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using Cliquemix.Models;
using System.Threading.Tasks;

namespace Cliquemix.Controllers
{
    public class AccountController : Controller
    {
        private Endereco endereco = new Endereco();
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Search(string _cep)
        {
            //some operations goes here
            //            
            tbAnunciante anunciante = new tbAnunciante();
            return View("Register"); //return some view to the user
        }

        //
        // GET: /Account/Login
        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
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
                    FormsAuthentication.RedirectFromLoginPage(_authTicket.UserData, model.Remember, _authCookie.Path);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("PrincipalAnunciante", "PrincipalAnunciante");
                    }
                }
                else
                {
                    model.Username = string.Empty;
                    model.Password = string.Empty;
                    ModelState.AddModelError("", "Usuário ou senha inválido!");
                }
            }
            return View(model);
        }
        /*
        public Action Buscar(string cep)
        {
            
        }
        */

        //
        // GET: /Account/Register
        public ActionResult Register()
        {
            ViewBag.raid = new SelectList(db.tbRamoAtividade, "raid", "descricao");
            return View();
        }

/*
 *
        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

*/

        //
        // GET: /Account/Logout
        public ActionResult Logout()
        {
            if (Request.IsAuthenticated)
            {   return View("Logout"); }

            return RedirectToAction("PrincipalAnunciante", "PrincipalAnunciante");
        }

        //Método que realiza o Logout
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

/*
        //
        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout(bool sair)
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("PrincipalAnunciante", "PrincipalAnunciante");
        }
*/

	}
}