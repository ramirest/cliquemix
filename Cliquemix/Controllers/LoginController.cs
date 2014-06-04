using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cliquemix.Models;
using Cliquemix.Repositories;

namespace Cliquemix.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Login/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Login/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //Esse método abaixo será invocado quando o usuário tentar fazer login
        [HttpPost]
        public ActionResult Logar(tbUsers model)
        {
            if (UsuarioRepositorio.AutenticarUsuario(model.username, model.pwd) == false)
            {
                ViewBag.msg_Error = "O nome de usuário ou a senha informada estão incorretas!";
                return View(model);
            }

            //Usuário foi autenticado com sucesso, redirecinamento ele para a página especial dos usuários.
            return RedirectToAction("Principal", "Login");
        }

        //Essa página será a página principal do usuário, só poderá ser acessada se o usuário estiver logado
        [Authorize]
        public ActionResult Principal()
        {
            return View();
        }
    }
}
