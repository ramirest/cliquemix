using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using Cliquemix.Models;

namespace Cliquemix.Controllers
{
    [Authorize]
    public class TesteController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Teste
        public ActionResult Index()
        {
            ViewBag.Cidades = ProcFunc.FiltrarCidades("Ipatinga");
            return View();
        }

    }
}