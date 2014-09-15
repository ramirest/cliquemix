using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using Cliquemix.Models;
using Microsoft.Reporting.WebForms;

namespace Cliquemix.Controllers
{
    public class TesteController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Teste
        public ActionResult Index()
        {
            return View();
        }



    }
}