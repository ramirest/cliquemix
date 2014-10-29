using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Cliquemix.Models;

namespace Cliquemix.Controllers
{
    public class tbCreditoComprasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: tbCreditoCompras
        public ActionResult Index()
        {
            var tbCreditoCompra = db.tbCreditoCompra.Include(t => t.tbAnunciante).Include(t => t.tbCredito).Include(t => t.tbCreditoStatus).Include(t => t.tbTransacaoXml);
            return View(tbCreditoCompra.ToList());
        }

        // GET: tbCreditoCompras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCreditoCompra tbCreditoCompra = db.tbCreditoCompra.Find(id);
            if (tbCreditoCompra == null)
            {
                return HttpNotFound();
            }
            return View(tbCreditoCompra);
        }

        // GET: tbCreditoCompras/Create
        public ActionResult Create()
        {
            ViewBag.pid = new SelectList(db.tbAnunciante, "pid", "cnpj");
            ViewBag.crid = new SelectList(db.tbCredito, "crid", "tituloPacote");
            ViewBag.crsid = new SelectList(db.tbCreditoStatus, "crsid", "dsCreditoStatus");
            ViewBag.txid = new SelectList(db.tbTransacaoXml, "txid", "transacao");
            return View();
        }

        // POST: tbCreditoCompras/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ccid,pid,crid,dtCompra,crsid,promocional,txid,dtVencimento")] tbCreditoCompra tbCreditoCompra)
        {
            if (ModelState.IsValid)
            {
                db.tbCreditoCompra.Add(tbCreditoCompra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pid = new SelectList(db.tbAnunciante, "pid", "cnpj", tbCreditoCompra.pid);
            ViewBag.crid = new SelectList(db.tbCredito, "crid", "tituloPacote", tbCreditoCompra.crid);
            ViewBag.crsid = new SelectList(db.tbCreditoStatus, "crsid", "dsCreditoStatus", tbCreditoCompra.crsid);
            ViewBag.txid = new SelectList(db.tbTransacaoXml, "txid", "transacao", tbCreditoCompra.txid);
            return View(tbCreditoCompra);
        }

        // GET: tbCreditoCompras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCreditoCompra tbCreditoCompra = db.tbCreditoCompra.Find(id);
            if (tbCreditoCompra == null)
            {
                return HttpNotFound();
            }
            ViewBag.pid = new SelectList(db.tbAnunciante, "pid", "cnpj", tbCreditoCompra.pid);
            ViewBag.crid = new SelectList(db.tbCredito, "crid", "tituloPacote", tbCreditoCompra.crid);
            ViewBag.crsid = new SelectList(db.tbCreditoStatus, "crsid", "dsCreditoStatus", tbCreditoCompra.crsid);
            ViewBag.txid = new SelectList(db.tbTransacaoXml, "txid", "transacao", tbCreditoCompra.txid);
            return View(tbCreditoCompra);
        }

        // POST: tbCreditoCompras/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ccid,pid,crid,dtCompra,crsid,promocional,txid,dtVencimento")] tbCreditoCompra tbCreditoCompra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbCreditoCompra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pid = new SelectList(db.tbAnunciante, "pid", "cnpj", tbCreditoCompra.pid);
            ViewBag.crid = new SelectList(db.tbCredito, "crid", "tituloPacote", tbCreditoCompra.crid);
            ViewBag.crsid = new SelectList(db.tbCreditoStatus, "crsid", "dsCreditoStatus", tbCreditoCompra.crsid);
            ViewBag.txid = new SelectList(db.tbTransacaoXml, "txid", "transacao", tbCreditoCompra.txid);
            return View(tbCreditoCompra);
        }

        // GET: tbCreditoCompras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbCreditoCompra tbCreditoCompra = db.tbCreditoCompra.Find(id);
            if (tbCreditoCompra == null)
            {
                return HttpNotFound();
            }
            return View(tbCreditoCompra);
        }

        // POST: tbCreditoCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbCreditoCompra tbCreditoCompra = db.tbCreditoCompra.Find(id);
            db.tbCreditoCompra.Remove(tbCreditoCompra);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
