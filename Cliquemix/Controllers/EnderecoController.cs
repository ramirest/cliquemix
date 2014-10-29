using System.Web.Mvc;
using Cliquemix.Models;

namespace Cliquemix.Controllers
{
    [Authorize]
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
        public PartialViewResult DefinicaoArquitetura(string cep)
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
        
     
    }
}