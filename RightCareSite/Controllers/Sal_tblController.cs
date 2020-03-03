using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using RightCareSite.Models;
using RightCareSite.Models.DataBase;

namespace RightCareSite.Controllers
{
    public class Sal_tblController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sal_tbl
        public ActionResult Index()
        {
            return View(db.Sal_Tbls.ToList());
        }

        // GET: Sal_tbl/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sal_tbl sal_tbl = db.Sal_Tbls.Find(id);
            if (sal_tbl == null)
            {
                return HttpNotFound();
            }
            return View(sal_tbl);
        }
        public ActionResult Invoice(int? id)
        {
            var query = (from s in db.Sal_Tbls
                         join sd in db.SaleDetails on s.Id equals sd.Sal_TblId
                         join p in db.product_Tbles on sd.Product_TbleId equals p.Id
                         where s.Id == id
                         select new
                         {
                             Id = s.Id,
                             Expr1 = sd.Product_TbleId,
                             Name = sd.Product_Tble.Name,
                             Cust_Name = s.Cust_Name,
                             OrderDate = s.OrderDate,
                             QtyOut = sd.QtyOut,
                             Price = sd.Price,
                             Amount = sd.Amount
                         }).ToList();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "SalInvoice.rpt"));
            rd.SetDataSource(query);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "فاتورة مبيعات.pdf");
            }
            catch
            {
                throw;
            }
        }

        // GET: Sal_tbl/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sal_tbl/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cust_Id,Cust_Name,OrderDate")] Sal_tbl sal_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Sal_Tbls.Add(sal_tbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sal_tbl);
        }

        // GET: Sal_tbl/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sal_tbl sal_tbl = db.Sal_Tbls.Find(id);
            if (sal_tbl == null)
            {
                return HttpNotFound();
            }
            return View(sal_tbl);
        }

        // POST: Sal_tbl/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cust_Id,Cust_Name,OrderDate")] Sal_tbl sal_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sal_tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sal_tbl);
        }

        // GET: Sal_tbl/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sal_tbl sal_tbl = db.Sal_Tbls.Find(id);
            if (sal_tbl == null)
            {
                return HttpNotFound();
            }
            return View(sal_tbl);
        }

        // POST: Sal_tbl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sal_tbl sal_tbl = db.Sal_Tbls.Find(id);
            db.Sal_Tbls.Remove(sal_tbl);
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
