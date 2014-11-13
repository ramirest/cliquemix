using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cliquemix.Models;

namespace Cliquemix.Controllers.Administrador
{
    //Somente usuários com a permissão Administrador podem acessar essa página
    [PermissoesFiltro(Roles = "Administrador")]
    public class ConfigAdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult ControleAcesso()
        {
            var tbusers = db.tbUsers.Include(t => t.tbUsersTipo);
            return View(tbusers.ToList());
        }


        [HttpPost]
        public ActionResult LiberarAcesso(int id)
        {
            //var tbusers = db.tbUsers.Include(t => t.tbUsersTipo);
            return RedirectToAction("ControleAcesso");
        }


        [HttpPost]
        public ActionResult BloquearAcesso(int id)
        {
            //var tbusers = db.tbUsers.Include(t => t.tbUsersTipo);
            return RedirectToAction("ControleAcesso");
        }
    }
}