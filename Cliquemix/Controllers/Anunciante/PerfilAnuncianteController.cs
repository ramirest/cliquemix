using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cliquemix.Models;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNet.Identity;

namespace Cliquemix.Controllers.Anunciante
{
    //Somente usuários com a permissão Anunciante podem acessar essa página
    [PermissoesFiltro(Roles = "Anunciante")]
    public class PerfilAnuncianteController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private LoginModel model = new LoginModel();
        internal static string TipoAviso { get; set; }
        //TipoAviso = 1 =>  Dados da Empresa alterados com sucesso
        //TipoAviso = 2 =>  Dados de Contato alterados com sucesso
        //TipoAviso = 3 =>  Dados de Endereço alterados com sucesso
        //TipoAviso = 4 =>  Dados de Usuário alterados com sucesso
        //TipoAviso = 0 =>  Erro ao tentar alterar os dados

        //
        // GET: /PerfilAnunciante/
        public ActionResult PerfilAnunciante()
        {
            ViewBag.AvisoPerfil = TipoAviso;
            TipoAviso = string.Empty;
            return View();
        }

        [HttpGet]
        public ActionResult PerfilAnunciantePrincipal()
        {
            var cdAnun = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
            var anun = db.tbAnunciante.First(m => m.pid == cdAnun);
            ViewBag.razaoSocial = anun.razaoSocial;
            ViewBag.nomeFantasia = anun.nmFantasia;
            ViewBag.cnpj = anun.cnpj;
            ViewBag.email = anun.email;
            if (anun.saldoCreditos != null)
            {
                var sa = (decimal)anun.saldoCreditos;
                ViewBag.saldoAtual = sa.ToString("N", new System.Globalization.CultureInfo("pt-BR"));
            }
            else
            {
                ViewBag.saldoAtual = (0).ToString("N", new System.Globalization.CultureInfo("pt-BR"));
            }
            return PartialView();
        }


        public ActionResult EditarPerfilDadosEmpresa()
        {
            try
            {
                var cdAnun = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
                var tbAnunciante = db.tbAnunciante.First(m => m.pid == cdAnun);

                ViewBag.razaoSocial = tbAnunciante.razaoSocial;
                ViewBag.nomeFantasia = tbAnunciante.nmFantasia;
                ViewBag.cnpj = tbAnunciante.cnpj;
                ViewBag.ie = tbAnunciante.email;
                ViewBag.im = tbAnunciante.im;
                ViewBag.contato = tbAnunciante.contato;
                ViewBag.ramoAtividade = tbAnunciante.tbRamoAtividade.descricao;
                return View();
            }
            catch (Exception)
            {
                TipoAviso = "0";
                return RedirectToAction("PerfilAnunciante");
            }

        }


        public ActionResult EditarPerfilDadosContato()
        {
            try
            {
                var cdAnun = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
                var tbAnunciante = db.tbAnunciante.First(m => m.pid == cdAnun);
                return View(tbAnunciante);
            }
            catch (Exception)
            {
                TipoAviso = "0";
                return RedirectToAction("PerfilAnunciante");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPerfilDadosContato(
            [Bind(Include = "pid,cnpj,razaoSocial,nmFantasia,contato,telResidencial,telComercial,telCelular1,telCelular2,ie,im,email,site,obs,cpid,raid,tid,saldoCreditos,leuTermo,uid,peid,tipo")] tbAnunciante tbanunciante)
        {
            if (ModelState.IsValid)
            {
                var cdAnun = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
                var anun = db.tbAnunciante.First(m => m.pid == cdAnun);
                anun.email = tbanunciante.email; //Request.Form.Get("email");
                anun.site = tbanunciante.site;//Request.Form.Get("site");
                anun.telResidencial = tbanunciante.telResidencial;//Request.Form.Get("telResidencial");
                anun.telComercial = tbanunciante.telComercial;//Request.Form.Get("telComercial");
                anun.telCelular1 = tbanunciante.telCelular1;//Request.Form.Get("telCelular1");
                anun.telCelular2 = tbanunciante.telCelular2;//Request.Form.Get("telCelular2");
                db.Entry(anun).State = EntityState.Modified;
                db.SaveChanges();
                TipoAviso = "2";
                return RedirectToAction("PerfilAnunciante");
            }
            TipoAviso = "0";
            return View(tbanunciante);
        }

        public ActionResult EditarPerfilDadosEndereco()
        {
            try
            {
                var cdAnun = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
                var tbAnuncianteEndereco = db.tbAnuncianteEndereco.First(m => m.pid == cdAnun);
                return View(tbAnuncianteEndereco);
            }
            catch (Exception)
            {
                TipoAviso = "0";
                return RedirectToAction("PerfilAnunciante");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPerfilDadosEndereco(
            [Bind(Include = "peid,pid,numero_endereco,complemento_endereco,nomeBairro,nomeCidade,ufEstado,nomePais,cep,endereco")] tbAnuncianteEndereco tbAnuncianteEndereco)
        {
            if (ModelState.IsValid)
            {
                var cdAnun = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
                var anun = db.tbAnuncianteEndereco.First(m => m.pid == cdAnun);
                anun.cep = tbAnuncianteEndereco.cep;//Request.Form.Get("cep");
                anun.endereco = tbAnuncianteEndereco.endereco;//Request.Form.Get("endereco");
                anun.numero_endereco = tbAnuncianteEndereco.numero_endereco;//Request.Form.Get("numero_endereco");
                anun.complemento_endereco = tbAnuncianteEndereco.complemento_endereco;//Request.Form.Get("complemento_endereco");
                anun.nomeBairro = tbAnuncianteEndereco.nomeBairro;//Request.Form.Get("nomeBairro");
                anun.nomeCidade = tbAnuncianteEndereco.nomeCidade;//Request.Form.Get("nomeCidade");
                anun.ufEstado = tbAnuncianteEndereco.ufEstado;//Request.Form.Get("ufEstado");
                anun.nomePais = tbAnuncianteEndereco.nomePais;//Request.Form.Get("nomePais");
                db.Entry(anun).State = EntityState.Modified;
                db.SaveChanges();
                TipoAviso = "3";
                return RedirectToAction("PerfilAnunciante");
            }
            TipoAviso = "0";
            return View(tbAnuncianteEndereco);
        }

        public ActionResult EditarPerfilDadosUsuario()
        {
            try
            {
                var cdAnun = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
                var anun = db.tbAnunciante.First(a => a.pid == cdAnun);
                var tbUsers = db.tbUsers.First(u => u.uid == anun.uid);
                return View();
            }
            catch (Exception)
            {
                TipoAviso = "0";
                return RedirectToAction("PerfilAnunciante");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPerfilDadosUsuario([Bind(Include = "uid,pid,pwd,cpwd")] tbUsers tbUsers)
        {
            if (ModelState.IsValid)
            {
                var cdAnun = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
                var anun = db.tbAnunciante.First(m => m.pid == cdAnun);
                var usu = db.tbUsers.First(u => u.uid == anun.uid);

                string pwdAtual = @Request.Form.Get("txtSenhaAtual");
                string pwdNova = @Request.Form.Get("txtNovaSenha");
                string cpwdNova = @Request.Form.Get("txtConfNovaSenha");

                //Caso a Senha Atual, Nova Senha e Confirmação da Nova Senha esteja vazia ele não processa
                if (String.IsNullOrEmpty(pwdAtual) || String.IsNullOrEmpty(pwdNova) || String.IsNullOrEmpty(cpwdNova))
                {
                    ViewBag.tpError = "006.00001";
                    return View();
                }

                //Caso a Senha Atual informada, seja diferente da senha armazenada no banco
                if (!model.UserIsValid(usu.username, pwdAtual))
                {
                    //A Senha Atual informada não está correta
                    ViewBag.tpError = "006.00004";
                    return View();
                }

                //Verifica se a senha atende os padrões de senha do sistema (SE A SENHA É FORTE)
                if (!verificaSenhaForte(pwdNova))
                {
                    ViewBag.tpError = "006.00002";
                    return View();
                }
                
                //Caso a senha seja diferente da confirmação ele não processa
                if (pwdNova != cpwdNova)
                {
                    ViewBag.tpError = "006.00003";
                    return View();
                }

                //*** Salvar dados na tabela de Usuário ***
                usu.pwd = ProcFunc.CryptographyPass(pwdNova); //50
                usu.cpwd = usu.pwd; //50
                db.Entry(anun).State = EntityState.Modified;
                db.SaveChanges();
                TipoAviso = "4";
                return RedirectToAction("PerfilAnunciante");
            }
            TipoAviso = "0";
            return View();
        }

        public static Boolean verificaSenhaForte(string senha)
        {
            if (senha.Length < 6 || senha.Length > 12)
                return false;
            if (!senha.Any(c => Char.IsDigit(c)))
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

        public static Boolean LoginValido(string usu)
        {
            if (usu.Length < 6 || usu.Length > 20)
                return false;
            //if (!_usu.Any(c => Char.IsDigit(c)))
            //return false;
            if (usu.Any(c => char.IsUpper(c)))  //Verifica se a senha contém letras Maiúsculas
                return false;
            if (!usu.Any(c => char.IsLower(c)))  //Verifica se a senha contém letras Minúsculas
                return false;
            if (usu.Any(c => char.IsSymbol(c))) //Verifica se a senha contém algum síbolo { @, #, $, etc. }
                return false;

            return true;
        }

	}
}