using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using Cliquemix.Models;

namespace Cliquemix.Controllers.Anunciante
{
    [Authorize]
    public class PrincipalAnuncianteController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: PrincipalAnunciante
        public ActionResult PrincipalAnunciante()
        {
            ViewBag.Title = "CliqueMix";
            return View();
        }
    }
}