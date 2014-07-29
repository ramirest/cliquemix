using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cliquemix.Models;

namespace Cliquemix.Controllers
{
    public class DialogController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Dialog
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateAlbumPartial(string txt)
        {
            if (txt == null)
            {
                var tbAnuncio = db.tbAnuncio.ToList();
                return PartialView(tbAnuncio);
            }
            else
            {
                var tbAnuncio = from a in db.tbAnuncio
                    where (a.tituloAnuncio.Contains(txt))
                    select a;
                return PartialView(tbAnuncio.ToList());
            }
        }
    }
}