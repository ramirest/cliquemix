using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using Cliquemix.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

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

        //
        // GET: /Account/Register
        public ActionResult Register()
        {
            ViewBag.raid = new SelectList(db.tbRamoAtividade, "raid", "descricao");
            ViewBag.cpid = new SelectList(db.tbCondicaoPagto, "cpid", "descricao");
            return View();
        }

        public PartialViewResult RegisterEndereco()
        {
            return PartialView();
        }
        
        [HttpPost]
        public PartialViewResult RegisterEndereco(string cep)
        {
            Endereco.PesquisarLocal(cep);
            ViewBag.Cep = Endereco.Cep;
            ViewBag.Cepid = Endereco.Cepid;
            ViewBag.Endereco = Endereco.Logradouro;
            ViewBag.Bairro = Endereco.Bairro;
            ViewBag.Cidade = Endereco.Cidade;
            ViewBag.UF = Endereco.UF;
            ViewBag.Tipo = Endereco.TipoLogradouro;
            ViewBag.Resultado = Endereco.ResultadoTXT;
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "pid,cnpj,razaoSocial,nmFantasia,contato,ie,im,email,site,cpid,raid,saldoCreditos,uid,telResidencial,telComercial,telCelular1,telCelular2")] tbAnunciante tbanunciante)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = tbanunciante.tbUsers.username };
                if (tbanunciante.tbUsers.pwd == tbanunciante.tbUsers.cPwd)
                {
                    db.tbAnunciantes.Add(tbanunciante);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.Error = "Confirmação de senha inválida!";
                    RedirectToAction("Register");
                }
            }
            return View("Login");
        }


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

	}
}