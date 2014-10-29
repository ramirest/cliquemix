using System.Web.Mvc;
using Cliquemix.Models;

namespace Cliquemix.Controllers
{
    //Somente usuários com a permissão Administrador podem acessar essa página
    [PermissoesFiltro(Roles = "Administrador")]
    public class BanController : Controller
    {
        //
        // GET: /Ban/
        public ActionResult BanAnunciante()
        {
            return View();
        }

        public ActionResult ListBanAnunciante()
        {
            return View();
        }

        public ActionResult EditarBanAnunciante()
        {
            return View();
        }

        public ActionResult DeletarBanAnunciante()
        {
            return View();
        }

        public ActionResult BanConsumidor()
        {
            return View();
        }

        public ActionResult ListBanConsumidor()
        {
            return View();
        }

        public ActionResult EditarBanConsumidor()
        {
            return View();
        }

        public ActionResult DeletarBanConsumidor()
        {
            return View();
        }
        
	}
}