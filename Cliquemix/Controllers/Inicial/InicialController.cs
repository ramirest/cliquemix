using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using Cliquemix.Models;
using DocumentFormat.OpenXml.ExtendedProperties;

namespace Cliquemix.Controllers.Inicial
{
    public class InicialController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Inicial
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Account/Register
        public ActionResult Register()
        {
            ViewBag.raid = new SelectList(db.tbRamoAtividade, "raid", "descricao");
            ViewBag.cpid = new SelectList(db.tbCondicaoPagto.SqlQuery("SELECT * FROM dbo.tbCondicaoPagto WHERE ativo = 1"), "cpid", "descricao");
            ViewBag.tpAnunciante = new List<SelectListItem>
            {
                new SelectListItem {Selected = true, Text = "Pessoa Jurídica", Value = "0"},
                new SelectListItem {Selected = true, Text = "Pessoa Física", Value = "1"}
            };

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
        public ActionResult Register([Bind(Include = "pid,cnpj,razaoSocial,nmFantasia,contato,telResidencial,telComercial,telCelular1,telCelular2,ie,im,email,site,obs,cpid,raid,tid,saldoCreditos,leuTermo,uid,peid,tipo")] tbAnunciante tbanunciante)
        {
            if (ModelState.IsValid)
            {
                string _usuario = @Request.Form.Get("tbUsers.username");
                string _pwd = @Request.Form.Get("tbUsers.pwd");
                string _cpwd = @Request.Form.Get("tbUsers.cpwd");
                string _patrocinador = @Request.Form.Get("patrocinador");

                if (_patrocinador == string.Empty)
                    tbanunciante.tipo = 1;
                else
                    tbanunciante.tipo = 0;

                //Caso já tenha um CNPJ identico cadastrado não será processado
                if (ProcFunc.CnpjExiste(tbanunciante.cnpj))
                {
                    ViewBag.tpError = "002.00008";
                    return View(tbanunciante);
                }
                //Caso o usuário e senha estejam vazios ele não processa
                if (String.IsNullOrEmpty(_usuario) || String.IsNullOrEmpty(_pwd) || (_usuario.Length > 20))
                {
                    ViewBag.tpError = "002.00001";
                    return View(tbanunciante);
                }
                //Verifica se o usuário atende os padrões do sistema
                if (!loginValido(_usuario))
                {
                    ViewBag.tpError = "002.00009";
                    return View(tbanunciante);
                }
                //Caso já tenha um usuário cadastrado com o mesmo nome não será processado
                if (ProcFunc.UsuarioExiste(_usuario))
                {
                    ViewBag.tpError = "002.00002";
                    return View(tbanunciante);
                }
                //Verifica se a senha atende os padrões de senha do sistema (SE A SENHA É FORTE)
                if (!verificaSenhaForte(_pwd))
                {
                    ViewBag.tpError = "002.00003";
                    return View(tbanunciante);
                }
                //Caso a senha seja diferente da confirmação ele não processa
                if (_pwd != _cpwd)
                {
                    ViewBag.tpError = "002.00004";
                    return View(tbanunciante);
                }
                if (tbanunciante.razaoSocial == string.Empty)
                {
                    ViewBag.tpError = "002.00005";
                    return View(tbanunciante);
                }

                if (tbanunciante.nmFantasia == string.Empty)
                {
                    ViewBag.tpError = "002.00006";
                    return View(tbanunciante);
                }
                if (tbanunciante.cnpj == string.Empty)
                {
                    ViewBag.tpError = "002.00007";
                    return View(tbanunciante);
                }

                //Verifica o tipo do anunciante (p Comum ou p Agência) e valida os campos
                if (!(tbanunciante.tipo == 0 || tbanunciante.tipo == 1))
                    tbanunciante.tipo = 0;

                //Verifica se o patrocinador é válido
                if (tbanunciante.tipo == 0)
                    if (String.IsNullOrEmpty(_patrocinador) || (!ProcFunc.PatrocinadorExiste(_patrocinador)))
                    {
                        ViewBag.tpError = "002.00010";
                        return View(tbanunciante);
                    }

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
                tbanunciante.ativo = true;
                tbanunciante.ansid = ProcFunc.RetornarStatusPadraoNovoAnuncianteCadastrado();
                db.tbAnunciante.Add(tbanunciante);
                db.SaveChanges();

                //*** Definir saldo inicial do Anunciante ***
                var sfa = ProcFunc.RetornarSaldoCreditoPadraoNovoAnunciante();
                ProcFunc.AlterarSaldoAnunciante(tbanunciante.pid, sfa, sfa, "Saldo Inicial do Anunciante | Cód "+tbanunciante.pid.ToString());

                //*** Definir Patrocinador do Anunciante (Se Anunciante Comum) ***
                if ((tbanunciante.tipo == 0) && (ProcFunc.RetornarCodigoPatrocinadorAnunciante(_patrocinador) > 0))
                {
                    var anunciantePatrocinador = new tbAnunciantePatrocinador();
                    anunciantePatrocinador.pid = tbanunciante.pid;
                    anunciantePatrocinador.cid = ProcFunc.RetornarCodigoPatrocinadorAnunciante(_patrocinador);
                    anunciantePatrocinador.ativo = true;
                    CriarPatrocinadorAnunciante(anunciantePatrocinador);
                }

                //*** Salvar dados na tabela de Endereço do Anunciante ***
                var anuncianteEndereco = new tbAnuncianteEndereco();
                anuncianteEndereco.endereco = @Request.Form.Get("tbAnuncianteEndereco.endereco");
                anuncianteEndereco.nomeBairro = @Request.Form.Get("tbAnuncianteEndereco.nomeBairro");
                anuncianteEndereco.nomeCidade = @Request.Form.Get("tbAnuncianteEndereco.nomeCidade");
                anuncianteEndereco.ufEstado = @Request.Form.Get("tbAnuncianteEndereco.ufEstado");
                anuncianteEndereco.nomePais = @Request.Form.Get("tbAnuncianteEndereco.nomePais");
                anuncianteEndereco.cep = @Request.Form.Get("tbAnuncianteEndereco.cep");
                anuncianteEndereco.numero_endereco = @Request.Form.Get("tbAnuncianteEndereco.numero_endereco");
                anuncianteEndereco.complemento_endereco = @Request.Form.Get("tbAnuncianteEndereco.complemento_endereco");
                anuncianteEndereco.pid = ProcFunc.RetornarCodigoAnuncianteCnpj(tbanunciante.cnpj);
                CriarEnderecoAnunciante(anuncianteEndereco);

                CriarDiretorios(tbanunciante.pid);
            }
            return Redirect("/account/login");
        }

        public void CriarEnderecoAnunciante(tbAnuncianteEndereco anuncianteEndereco)
        {
            db.tbAnuncianteEndereco.Add(anuncianteEndereco);
            db.SaveChanges();
        }

        public void CriarPatrocinadorAnunciante(tbAnunciantePatrocinador anunciantePatrocinador)
        {
            db.tbAnunciantePatrocinador.Add(anunciantePatrocinador);
            db.SaveChanges();
        }

        public void CriarDiretorios(int? codAnunciante)
        {
                /*** Cria diretório para arquivos do anúnciante ***/
                ProcFunc.CriarDiretorio(Server.MapPath("~/Arquivos/Anunciantes/"),
                    String.Format("{0:000000}", codAnunciante));
                /*** Cria diretório para o perfil do anúnciante ***/
                ProcFunc.CriarDiretorio(Server.MapPath("~/Arquivos/Anunciantes/"),
                    String.Format("{0:000000}", codAnunciante), "Perfil");
                /*** Cria diretório para imagens de anúncios do anúnciante ***/
                ProcFunc.CriarDiretorio(Server.MapPath("~/Arquivos/Anunciantes/"),
                    String.Format("{0:000000}", codAnunciante), "Anúncios");
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

        public static Boolean loginValido(string _usu)
        {
            if (_usu.Length < 6 || _usu.Length > 20)
                return false;
            //if (!_usu.Any(c => Char.IsDigit(c)))
                //return false;
            if (_usu.Any(c => char.IsUpper(c)))  //Verifica se a senha contém letras Maiúsculas
                return false;
            if (!_usu.Any(c => char.IsLower(c)))  //Verifica se a senha contém letras Minúsculas
                return false;
            if (_usu.Any(c => char.IsSymbol(c))) //Verifica se a senha contém algum síbolo { @, #, $, etc. }
                return false;

            return true;
        }

        [HttpGet]
        public JsonResult ValidaPatrocinador(string nome)
        {
            var resultado = new
            {
                Error = "",
                tpError = ""
            };

            //Verifica se o patrocinador é válido
            if (String.IsNullOrEmpty(nome) || (!ProcFunc.PatrocinadorExiste(nome)))
            {
                resultado = new
                {
                    Error = "O Patrocinador informado não existe no banco de dados do sistema [Cod: 002.00010]",
                    tpError = "002.00010"
                };
            }

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

    }
}