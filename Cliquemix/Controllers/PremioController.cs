using System.Web.Mvc;
using Cliquemix.Models;

namespace Cliquemix.Controllers
{
    //Somente usuários com a permissão Administrador podem acessar essa página
    [PermissoesFiltro(Roles = "Administrador")]
    public class PremioController : Controller
    {
        //
        // GET: /Premio/
        public ActionResult NivelPremioCriar()
        {
            return View();
        }

        public ActionResult NivelPremioGerir()
        {
            return View();
        }

        public ActionResult NivelPremioEditar()
        {
            return View();
        }

        public ActionResult NivelPremioDeletar()
        {
            return View();
        }

        public ActionResult PremiacaoCriar()
        {
            return View();
        }

        public ActionResult PremiacaoGerir()
        {
            return View();
        }

        public ActionResult PremiacaoEditar()
        {
            return View();
        }

        public ActionResult PremiacaoDeletar()
        {
            return View();
        }

        public ActionResult PremiacaoVincularCriar()
        {
            return View();
        }

        public ActionResult PremiacaoVincularEditar()
        {
            return View();
        }

        public ActionResult PremiacaoVincularDeletar()
        {
            return View();
        }

        public ActionResult PremiacaoVincularGerir()
        {
            return View();
        }
	}
}