using System.Web.Mvc;
using Cliquemix.Models;

namespace Cliquemix.Controllers
{
    //Somente usuários com a permissão Administrador podem acessar essa página
    [PermissoesFiltro(Roles = "Administrador")]
    public class BannerController : Controller
    {        
        // GET: /Banner/
        public ActionResult CreateBanner()
        {
            return View();
        }

        public ActionResult UpdateBanner()
        {
            return View();
        }

        public ActionResult DeleteBanner()
        {
            return View();
        }

        public ActionResult ListBanner()
        {
            return View();
        }
	}
}