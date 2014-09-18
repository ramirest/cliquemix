using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using Cliquemix.Models;
using Microsoft.AspNet.Identity;

namespace Cliquemix.Controllers.Anunciante
{
    //Somente usuários com a permissão Anunciante podem acessar essa página
    [PermissoesFiltro(Roles = "Anunciante")]
    public class PrincipalAnuncianteController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: PrincipalAnunciante
        public ActionResult PrincipalAnunciante()
        {

            var a = ProcFunc.RetornarCodigoAnuncianteUsuario(User.Identity.GetUserName());
            //Declarações de mostras estatísticas da Dashboard - Anúncio

            //1 -   spna	=> Status Padrão Anuncio Disponível [Azul Escuro]
            ViewBag.DashAnuncioDisponiveis = ProcFunc.RetornarQtdeAnunciosDashboardAnunciante(1, a);
            //2 -   spaec	=> Status Padrão Anuncio Em Campanha [Verde]
            ViewBag.DashAnuncioEmCampanha = ProcFunc.RetornarQtdeAnunciosDashboardAnunciante(2, a);
            //3 -   spaaa	=> Status Padrão Anuncio Em Aprovação [Amarelo]
            ViewBag.DashAnuncioEmAprovacao = ProcFunc.RetornarQtdeAnunciosDashboardAnunciante(3, a);
            //4 -   spap	=> Status Padrão Anuncio Publicado [Azul Claro]
            ViewBag.DashAnuncioPublicados = ProcFunc.RetornarQtdeAnunciosDashboardAnunciante(4, a);
            //5 -   spab	=> Status Padrão Anuncio Bloqueado [Vermelho]
            ViewBag.DashAnuncioBloqueados = ProcFunc.RetornarQtdeAnunciosDashboardAnunciante(5, a);


            //Declarações de mostras estatísticas da Dashboard - Campanha

            //1 -   Spcp	=> Status Padrão para Campanhas Programada		Programadas [Azul Escuro]
            ViewBag.DashCampanhaProgramadas = ProcFunc.RetornarQtdeCampanhasDashboardAnunciante(1, a);
            //2 -   Spcec	=> Status Padrão para Campanhas Em Campanha		Em Campanha [Verde]
            ViewBag.DashCampanhaEmCampanha = ProcFunc.RetornarQtdeCampanhasDashboardAnunciante(2, a);
            //3 -   Spcea	=> Status Padrão para Campanhas Em Aprovação	Em Aprovação [Amarelo]
            ViewBag.DashCampanhaEmAprovacao = ProcFunc.RetornarQtdeCampanhasDashboardAnunciante(3, a);
            //4 -   Spcep	=> Status Padrão para Campanhas Em Pausa		Em Pausa [Azul Claro]
            ViewBag.DashCampanhaEmPausa = ProcFunc.RetornarQtdeCampanhasDashboardAnunciante(4, a);
            //5 -   Spcb	=> Status Padrão para Campanhas Bloqueada		Bloqueadas [Vermelho]
            ViewBag.DashCampanhaBloqueadas = ProcFunc.RetornarQtdeCampanhasDashboardAnunciante(5, a);
            //6 -   Spcf	=> Status Padrão para Campanhas Finalizada		Finalizadas [Lilás]
            ViewBag.DashCampanhaFinalizadas = ProcFunc.RetornarQtdeCampanhasDashboardAnunciante(6, a);

            ViewBag.Title = "CliqueMix";
            return View();
        }


        // GET: PrincipalAnunciante
        //Somente usuários com a permissão Anunciante Avançado podem acessar essa página
        [PermissoesFiltro(Roles = "Anunciante")]
        public ActionResult PrincipalAnuncianteAvancado()
        {
            return View();
        }
    }
}