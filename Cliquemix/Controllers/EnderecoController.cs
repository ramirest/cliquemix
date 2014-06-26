using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cliquemix.Models;

namespace Cliquemix.Controllers
{
    public class EnderecoController : Controller
    {
        Endereco endereco = new Endereco();
        //
        // GET: /Endereco/
        public ActionResult Index()
        {
            return View();            
        }
    }
}
