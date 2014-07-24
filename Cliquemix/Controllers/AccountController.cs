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
                string _pwd = @Request.Form.Get("tbUsers.pwd");
                string _cpwd = @Request.Form.Get("tbUsers.cpwd");
                //Caso o usuário e senha estejam vazios ele não processa
                if (string.IsNullOrEmpty(_usuario) || string.IsNullOrEmpty(_pwd))
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
                //Verifica se a senha atende os padrões de senha do sistema (SE A SENHA É FORTE)
                if (!verificaSenhaForte(_pwd))
                {
                    ViewBag.Error = "A senha informada não é segura";
                    return View(tbanunciante);
                }
                //Caso a senha seja diferente da confirmação ele não processa
                else if (_pwd != _cpwd)
                {
                    ViewBag.Error = "A Senha e a Confirmação devem ser idênticas";
                    return View(tbanunciante);
                }
                else
                {
                    ViewBag.Error = "";
                    //*** Salvar dados na tabela de Usuário ***
                    tbUsers users = new tbUsers();
                    users.username = _usuario; //50
                    users.pwd = ProcFunc.CryptographyPass(_pwd); //50
                    users.cpwd = users.pwd; //50
                    users.utid = ProcFunc.RetornarCodigoTipoUsuario("Anunciante");
                    db.tbUsers.Add(users);
                    db.SaveChanges();

                    //*** Salvar dados na tabela de Anunciante ***
                    tbanunciante.uid = ProcFunc.RetornarCodigoUsuario(_usuario);
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
                    anuncianteEndereco.pid = ProcFunc.RetornarCodigoAnuncianteCnpj(tbanunciante.cnpj);
                    db.tbAnuncianteEndereco.Add(anuncianteEndereco);
                    db.SaveChanges();  
                  
                    /*** Cria diretório para arquivos do anúnciante ***/
                    ProcFunc.CriarDiretorio(Server.MapPath("~/Arquivos/Anunciantes/"),
                        String.Format("{0:000000}", anuncianteEndereco.pid));
                    /*** Cria diretório para o perfil do anúnciante ***/
                    ProcFunc.CriarDiretorio(Server.MapPath("~/Arquivos/Anunciantes/"),
                        String.Format("{0:000000}", anuncianteEndereco.pid), "Perfil");
                    /*** Cria diretório para imagens de anúncios do anúnciante ***/
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


        public void CriarEnderecoAnunciante(tbAnuncianteEndereco anuncianteEndereco)
        {
            db.tbAnuncianteEndereco.Add(anuncianteEndereco);
            db.SaveChanges();
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

        public static Boolean verificaSenhaForte(string senha)
        {
            if (senha.Length < 6 || senha.Length > 12)
                return false;
            if (!senha.Any(c => char.IsDigit(c)))
                return false;
            //if (!senha.Any(c => char.IsUpper(c)))  //Verifica se a senha contém letras Maiúsculas
                //return false;
            //if (!senha.Any(c => char.IsLower(c)))  //Verifica se a senha contém letras Minúsculas
                //return false;
            //if (!senha.Any(c => char.IsSymbol(c))) //Verifica se a senha contém algum síbolo { @, #, $, etc. }
                //return false;
            
            /* Verifica se a senha contém caracteres repetidos 
            var contadorRepetido = 0;
            var ultimoCaracter = '\0';
            foreach (var c in senha) 
            {
                if (c == ultimoCaracter)
                    contadorRepetido++;
                else
                    contadorRepetido = 0;
                if (contadorRepetido == 2)
                    return false;
                ultimoCaracter = c;
            }*/
            return true;
        }

     }
}