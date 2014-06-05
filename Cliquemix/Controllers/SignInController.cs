using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test_AspNet.Controllers
{
    public class SignInController : Controller
    {
        // GET: SignIn
        public ActionResult SignInAnunciante()
        {
            return View();
        }
    }
}