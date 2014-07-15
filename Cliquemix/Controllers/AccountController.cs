using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using BLToolkit.Data.Linq;
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
                    var _authTicket = new FormsAuthenticationTicket(iResult, model.Username + "-" + Convert.ToString(1),
                        DateTime.Now, DateTime.Now.AddMinutes(30), model.Remember, model.Username);
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
            ViewBag.cpid = new SelectList(db.tbCondicaoPagto.SqlQuery("SELECT * FROM dbo.tbCondicaoPagto WHERE ativo = 1"), "cpid", "descricao");
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
            ViewBag.Pais = Endereco.Pais;
            ViewBag.Tipo = Endereco.TipoLogradouro;
            ViewBag.Resultado = Endereco.ResultadoTXT;
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "razaoSocial,nmFantasia,cnpj,ie,im,raid,contato,email,site,telResidencial,telComercial,telCelular1,telCelular2,cpid,nomeBairro,nomeCidade,ufEstado,nomePais,cep,numero_endereco,complemento_endereco,endereco,username,pwd,cpwd")] tbAnunciante tbanunciante)
        {            
            if (ModelState.IsValid)
            {
                string _usuario = @Request.Form.Get("tbUsers.username");
                //Caso o usuário e senha estejam vazios ele não processa
                if (string.IsNullOrEmpty(_usuario) || string.IsNullOrEmpty(@Request.Form.Get("tbUsers.pwd")))
                {
                    ViewBag.Error = "Login ou password não podem estar vazios";
                    return View(tbanunciante);
                }
                //Caso já tenha um usuário cadastrado com o mesmo nome não será processado
                if (UsuarioExiste(_usuario))
                {
                    ViewBag.Error = "O Login informado já existe";
                    return View(tbanunciante);
                }
                //Caso a senha seja diferente da confirmação ele não processa
                else if ((@Request.Form.Get("tbUsers.pwd")) != (@Request.Form.Get("tbUsers.cPwd")))
                {
                    ViewBag.Error = "A Senha e a Confirmação devem ser idênticas";
                    return View(tbanunciante);
                }
                else
                {
                    //*** Salvar dados na tabela de Usuário ***                    
                    tbUsers users = new tbUsers();
                    users.username = _usuario;
                    users.pwd = @Request.Form.Get("tbUsers.pwd");
                    users.cpwd = @Request.Form.Get("tbUsers.cpwd");
                    users.utid = RetornaCodigoTipoUsuario("Anunciante");
                    db.tbUsers.Add(users);
                    db.SaveChanges();

                    //*** Salvar dados na tabela de Anunciante ***
                    tbanunciante.uid = RetornaCodigoUsuario(_usuario);
                    db.tbAnunciante.Add(tbanunciante);
                    db.SaveChanges();                    
                    
                    //*** Salvar dados na tabela de Endereço do Anunciante ***
                    tbAnuncianteEndereco anuncianteEndereco = new tbAnuncianteEndereco();
                    anuncianteEndereco.endereco = @Request.Form.Get("tbAnuncianteEndereco.endereco");
                    anuncianteEndereco.nomeBairro = @Request.Form.Get("tbAnuncianteEndereco.nomeBairro");
                    anuncianteEndereco.nomeCidade = @Request.Form.Get("tbAnuncianteEndereco.nomeCidade");
                    anuncianteEndereco.ufEstado = @Request.Form.Get("tbAnuncianteEndereco.ufEstado");
                    anuncianteEndereco.nomePais = @Request.Form.Get("tbAnuncianteEndereco.nomePais");
                    anuncianteEndereco.cep = @Request.Form.Get("tbAnuncianteEndereco.cep");
                    anuncianteEndereco.numero_endereco = @Request.Form.Get("tbAnuncianteEndereco.numero_endereco");
                    anuncianteEndereco.complemento_endereco = @Request.Form.Get("tbAnuncianteEndereco.complemento_endereco");
                    anuncianteEndereco.pid = RetornaCodigoAnunciante(tbanunciante.cnpj);
                    db.tbAnuncianteEndereco.Add(anuncianteEndereco);
                    db.SaveChanges();  
                  
                    /*** Criar diretórios para arquivos do anúnciante ***/
                    ProcFunc.CriarDiretorio(Server.MapPath("~/Arquivos/Anunciantes/"),
                        String.Format("{0:000000}", anuncianteEndereco.pid));
                    /*** Criar diretórios para arquivos do anúnciante ***/
                    ProcFunc.CriarDiretorio(Server.MapPath("~/Arquivos/Anunciantes/"),
                        String.Format("{0:000000}", anuncianteEndereco.pid), "Perfil");
                    /*** Criar diretórios para arquivos do anúnciante ***/
                    ProcFunc.CriarDiretorio(Server.MapPath("~/Arquivos/Anunciantes/"),
                        String.Format("{0:000000}", anuncianteEndereco.pid), "Anúncios");
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

        public void criarEnderecoAnunciante(tbAnuncianteEndereco anuncianteEndereco)
        {
            db.tbAnuncianteEndereco.Add(anuncianteEndereco);
            db.SaveChanges();
        }

        public int RetornaCodigoAnunciante(string _cnpj)
        {
            var a = (from cont in db.tbAnunciante where cont.cnpj == _cnpj select cont).First();
            return a.pid;
        }

        public int RetornaCodigoTipoUsuario(string _tipo)
        {
            var a = (from tipo in db.tbUsersTipo where tipo.dsUsersTipo == _tipo select tipo).First();
            return a.utid;
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

        public int RetornaCodigoUsuario(string _nomeUsuario)
        {
            var a = (from usu in db.tbUsers where usu.username == _nomeUsuario select usu).First();
            return a.uid;
        }
    }
}