using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cliquemix.Models;

namespace Cliquemix.Controllers
{
    public class EnderecoController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public string _cep;
        // GET: Endereco
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult DefinicaoArquitetura()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult DefinicaoArquitetura(string txtCep)
        {
            Endereco.PesquisarLocal("35162351");
            ViewBag.Cep = Endereco.Cep;
            ViewBag.Endereco = Endereco.Logradouro;
            ViewBag.Bairro = Endereco.Bairro;
            ViewBag.Cidade = Endereco.Cidade;
            ViewBag.UF = Endereco.UF;
            ViewBag.Tipo = Endereco.TipoLogradouro;
            ViewBag.Resultado = Endereco.ResultadoTXT;
            return PartialView();
        }
        
    }
}