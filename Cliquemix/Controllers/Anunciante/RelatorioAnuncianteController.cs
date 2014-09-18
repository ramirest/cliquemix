using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cliquemix.Models;
using Microsoft.AspNet.Identity;

namespace Cliquemix.Controllers.Anunciante
{
    //Somente usuários com a permissão Anunciante podem acessar essa página
    [PermissoesFiltro(Roles = "Anunciante")]
    public class RelatorioAnuncianteController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        //
        // GET: /RelatorioAnunciante/

        public ActionResult RelAnunPrincipal()
        {
            return View("_RelAnunPrincipal");
        }


        #region "Relatórios de Anúncios"
        public ActionResult RelAnuncio()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RelAnuncio(int opcao, int tipo)
        {
            if (opcao == 1) //Todos os Anúncios
                return RedirectToAction("RelAnuncioVisualizarTodos");

            else if (opcao == 5) //Cliques por Anúncio
                return RedirectToAction("RelAnuncioVisualizarCliques");

            else if (opcao == 7) //Top 10 mais clicados
                return RedirectToAction("RelAnuncioVisualizarTop10Mais");

            else if (opcao == 8) //Top 10 menos clicados
                return RedirectToAction("RelAnuncioVisualizarTop10Menos");

            else
                return View();
        }

        #region "Partial View - Relatórios de Anúncios"
        public ActionResult RelAnuncioVisualizarTodos()
        {
            var cdAnun = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
            var VwRelatorioAnuncianteAnuncios_Todos =
                db.VwRelatorioAnuncianteAnuncios_Todos.Where(a => a.pid == cdAnun).ToList();
            return PartialView(VwRelatorioAnuncianteAnuncios_Todos);
        }

        public ActionResult RelAnuncioVisualizarCliques()
        {
            return PartialView();
        }

        public ActionResult RelAnuncioVisualizarTop10Mais()
        {
            return PartialView();
        }

        public ActionResult RelAnuncioVisualizarTop10Menos()
        {
            return PartialView();
        }
        #endregion

        #endregion


        #region "Relatórios de Campanhas"
        public ActionResult RelCampanha()
        {
            return View();
        }
        #endregion


        #region "Relatórios de Destaques"
        public ActionResult RelDestaque()
        {
            return View();
        }
        #endregion


        #region "Relatórios de Movimentação"
        public ActionResult RelMovimento()
        {
            return View();
        }
        #endregion


        #region "Relatórios de Prêmios"
        public ActionResult RelPremio()
        {
            return View();
        }
        #endregion
    }
}