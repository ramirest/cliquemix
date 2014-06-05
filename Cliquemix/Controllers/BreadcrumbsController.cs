using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test_AspNet.Controllers
{
    public class BreadcrumbsController : Controller
    {
        // GET: Breadcrumbs
        public ActionResult Breadcrumbs()
        {
            return View();
        }
    }
}