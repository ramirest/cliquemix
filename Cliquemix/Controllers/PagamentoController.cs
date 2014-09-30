using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Cliquemix.Models;

namespace Cliquemix.Controllers
{
    //Usuários com a permissão Anunciante e Consumidor podem acessar essa página
    [PermissoesFiltro(Roles = "Anunciante,Consumidor")]
    public class PagamentoController : Controller
    {
        // GET: Pagamento
        public ActionResult Index()
        {
            return View();
        }

    }
}