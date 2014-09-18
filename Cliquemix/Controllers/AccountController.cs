using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using BLToolkit.Data.Linq;
using Cliquemix.Models;

namespace Cliquemix.Controllers
{
    public class AccountController : Controller
    {       
        private ApplicationDbContext db = new ApplicationDbContext();

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
                    if (model.UsuarioAtivo(model.Username))
                    {
                        iResult = 1;
                        int expira = ProcFunc.RetornarTempoExpiracaoSessaoUsuario(); //Expiração da Sessão

                        var authTicket = new FormsAuthenticationTicket(iResult,
                            model.Username + "-" + Convert.ToString(1),
                            DateTime.Now, DateTime.Now.AddMinutes(expira), model.Remember, model.Username);
                        var encryptTicket = FormsAuthentication.Encrypt(authTicket);
                        var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptTicket);
                        Response.Cookies.Add(authCookie);
                        Session.Add(model.Username, iResult.ToString());
                        FormsAuthentication.RedirectFromLoginPage(authTicket.UserData, model.Remember, authCookie.Path);

                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            LoginModel._salvarLog(model.CodUsuario, expira, iResult, Session.SessionID);

                            if (ProcFunc.RetornarTipoUsuarioLogado(model.Username) == 1)
                                return RedirectToAction("PrincipalAnunciante", "PrincipalAnunciante");

                            if (ProcFunc.RetornarTipoUsuarioLogado(model.Username) == 2)
                                return RedirectToAction("PrincipalAdmin", "PrincipalAdmin");

                            if (ProcFunc.RetornarTipoUsuarioLogado(model.Username) == 4)
                                return RedirectToAction("PrincipalAnuncios", "PrincipalConsumidor");

                            return RedirectToAction("PrincipalDemo", "PrincipalDemo");

                        }
                    }
                    else
                    {
                        model.Username = string.Empty;
                        model.Password = string.Empty;
                        ModelState.AddModelError("",
                            "Usuário está inativo no sistema. Entre em contato com os administradores [Cod 003.00001].");
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


        [Authorize]
        // GET: /Account/Logout
        public ActionResult Logout()
        {
            if (Request.IsAuthenticated)
            {   return View("Logout"); }

            return RedirectToAction("PrincipalAnunciante", "PrincipalAnunciante");
        }


        [Authorize]
        //Método que realiza o Logout
        // POST: /Account/LogOff
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult _Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        public bool UsuarioExiste(string _nomeUsuario)
        {
            try
            {
                var a = (from usu in db.tbUsers where usu.username == _nomeUsuario select usu).First();
                return true; 
            }
            catch (Exception)
            {                               
                return false;
            }
        }

        public ActionResult Negado()
        {
            return View();
        }

    }
}