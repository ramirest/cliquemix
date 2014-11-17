using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cliquemix.Models;
using Microsoft.AspNet.Identity;

namespace Cliquemix.Controllers.Consumidor
{
    [Authorize]
    public class PrincipalConsumidorController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public AnuncioViewModel AnuncioDetalhe = new AnuncioViewModel();
        tbCampanhaAnuncio campanhaAnuncio = new tbCampanhaAnuncio();

        //Somente usuários com a permissão Consumidor podem acessar essa página
        [PermissoesFiltro(Roles = "Consumidor")]
        // GET: /PrincipalConsumidor/
        public ActionResult PrincipalConsumidor()
        {
            var cdUsu = ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName());
            ViewBag.QtdeAnunPontuados = ProcFunc.RetornarQtdeAnunciosPontuadosDashboardConsumidor(cdUsu);
            ViewBag.QtdeAnunVisualizados = ProcFunc.RetornarQtdeAnunciosVisualizadosDashboardAnunciante(cdUsu);
            ViewBag.QtdeAnunPontosAdquiridos = ProcFunc.RetornarQtdePontosAdquiridosDashboardAnunciante(cdUsu);
            return View();
        }

        //Somente usuários com a permissão Consumidor podem acessar essa página
        [PermissoesFiltro(Roles = "Consumidor")]
        // Página principal do site, onde serão exibidos os anúncios para pontuação
        // GET: /PrincipalConsumidor/
        public ActionResult PrincipalAnuncios()
        {
            return RedirectToAction("Index", "Inicial");
        }


        public ActionResult CarregaAnuncioHotSite(int cdCampanhaAnuncio)
        {
            campanhaAnuncio = db.tbCampanhaAnuncio.First(x => x.caid == cdCampanhaAnuncio);
            return RedirectToAction("DetalhesAnuncio", "PrincipalConsumidor", new { @cdCampanhaAnuncio = cdCampanhaAnuncio });
        }
        

        //Somente usuários com a permissão Consumidor podem acessar essa página
        [PermissoesFiltro(Roles = "Consumidor")]
        public ActionResult DetalhesAnuncio(int cdCampanhaAnuncio)
        {
            campanhaAnuncio = db.tbCampanhaAnuncio.First(x => x.caid == cdCampanhaAnuncio);
            var cdAnunciante = (int) campanhaAnuncio.tbAnuncio.pid;
            int cdUsuarioAnunciante = ProcFunc.RetornarCodigoUsuario(User.Identity.GetUserName());
            bool pontuado = campanhaAnuncio.tbCampanhaAnuncioConsumidor.Where(x => x.uid == cdUsuarioAnunciante).ToList().Count > 0;

            AnuncioDetalhe = new AnuncioViewModel()
            {
                Aid = campanhaAnuncio.aid,
                CdAnunciante = (int)campanhaAnuncio.tbCampanha.pid,
                CdCategoria = (int)campanhaAnuncio.tbAnuncio.acid,
                CdCampanhaAnuncio = campanhaAnuncio.caid,
                CdStatus = campanhaAnuncio.casid,
                Comentar = campanhaAnuncio.tbAnuncio.comentar,
                Compartilhar = campanhaAnuncio.tbAnuncio.compartilhar,
                Curtir = campanhaAnuncio.tbAnuncio.curtir,
                DsAnuncio = campanhaAnuncio.tbAnuncio.dsAnuncio,
                ListaImagensAnuncio = campanhaAnuncio.tbAnuncio.tbAnuncioImg.ToList(),
                TituloAnuncio = campanhaAnuncio.tbAnuncio.tituloAnuncio,
                VideoAnuncio = campanhaAnuncio.tbAnuncio.videoAnuncio,
                Anunciante = campanhaAnuncio.tbAnuncio.tbAnunciante,
                Pontuado = pontuado,
                EnderecoAnunciante = campanhaAnuncio.tbAnuncio.tbAnunciante.tbAnuncianteEndereco.First(x => x.pid == cdAnunciante)
            };
            return View(AnuncioDetalhe);
        }


        [HttpGet]
        public ActionResult CarregaDestaque2()
        {
            int cont = 0;
            return null;
        }
        
	}
}