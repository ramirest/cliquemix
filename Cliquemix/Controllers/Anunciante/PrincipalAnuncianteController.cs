﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Test_AspNet.Controllers.Anunciante
{
    public class PrincipalAnuncianteController : Controller
    {
        // GET: PrincipalAnunciante
        public ActionResult PrincipalAnunciante()
        {          
            ViewBag.Title = "CliqueMix";
            return View();
        }
    }
}