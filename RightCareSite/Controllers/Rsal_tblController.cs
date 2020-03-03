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
    public class Rsal_tblController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rsal_tbl
        public ActionResult Index()
        {
            return View(db.rsal_Tbls.ToList());
        }

        // GET: Rsal_tbl/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rsal_tbl rsal_tbl = db.rsal_Tbls.Find(id);
            if (rsal_tbl == null)
            {
                return HttpNotFound();
            }
            return View(rsal_tbl);
        }

        // GET: Rsal_tbl/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rsal_tbl/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cust_Id,Cust_Name,OrderDate")] Rsal_tbl rsal_tbl)
        {
            if (ModelState.IsValid)
            {
                db.rsal_Tbls.Add(rsal_tbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rsal_tbl);
        }

        // GET: Rsal_tbl/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rsal_tbl rsal_tbl = db.rsal_Tbls.Find(id);
            if (rsal_tbl == null)
            {
                return HttpNotFound();
            }
            return View(rsal_tbl);
        }

        // POST: Rsal_tbl/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cust_Id,Cust_Name,OrderDate")] Rsal_tbl rsal_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rsal_tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rsal_tbl);
        }

        // GET: Rsal_tbl/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rsal_tbl rsal_tbl = db.rsal_Tbls.Find(id);
            if (rsal_tbl == null)
            {
                return HttpNotFound();
            }
            return View(rsal_tbl);
        }

        // POST: Rsal_tbl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rsal_tbl rsal_tbl = db.rsal_Tbls.Find(id);
            db.rsal_Tbls.Remove(rsal_tbl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ReInvoice(int? id)
        {
            var query = (from s in db.rsal_Tbls
                         join sd in db.SaleDetails on s.Id equals sd.Rsal_TblId
                         join p in db.product_Tbles on sd.Product_TbleId equals p.Id
                         where s.Id == id
                         select new
                         {
                             Id = s.Id,
                             Cust_Id=s.Cust_Id,
                             Product_TbleId = sd.Product_TbleId,
                             Name = sd.Product_Tble.Name,
                             Cust_Name = s.Cust_Name,
                             OrderDate = s.OrderDate,
                             QtyIn = sd.QtyIn,
                             Price = sd.Price,
                             Amount = sd.Amount
                         }).ToList();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "RinvoiceRpt.rpt"));
            rd.SetDataSource(query);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "مرتجع عميل.pdf");
            }
            catch
            {
                throw;
            }
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
