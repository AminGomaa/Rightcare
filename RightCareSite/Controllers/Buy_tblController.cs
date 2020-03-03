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
    public class Buy_tblController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Buy_tbl
        public ActionResult Index()
        {
            return View(db.buy_Tbls.ToList());
        }

        // GET: Buy_tbl/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Buy_tbl buy_tbl = db.buy_Tbls.Find(id);
            if (buy_tbl == null)
            {
                return HttpNotFound();
            }
            return View(buy_tbl);
        }

        // GET: Buy_tbl/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Buy_tbl/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderDate,Sup_Id,Sup_Name")] Buy_tbl buy_tbl)
        {
            if (ModelState.IsValid)
            {
                db.buy_Tbls.Add(buy_tbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(buy_tbl);
        }

        // GET: Buy_tbl/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Buy_tbl buy_tbl = db.buy_Tbls.Find(id);
            if (buy_tbl == null)
            {
                return HttpNotFound();
            }
            return View(buy_tbl);
        }

        // POST: Buy_tbl/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderDate,Sup_Id,Sup_Name")] Buy_tbl buy_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(buy_tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(buy_tbl);
        }

        // GET: Buy_tbl/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Buy_tbl buy_tbl = db.buy_Tbls.Find(id);
            if (buy_tbl == null)
            {
                return HttpNotFound();
            }
            return View(buy_tbl);
        }

        // POST: Buy_tbl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Buy_tbl buy_tbl = db.buy_Tbls.Find(id);
            db.buy_Tbls.Remove(buy_tbl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PInvoice(int? id)
        {
            var query = (from s in db.buy_Tbls
                         join sd in db.mainStores on s.Id equals sd.Buy_tblId
                         join p in db.product_Tbles on sd.Product_TbleId equals p.Id
                         where s.Id == id
                         select new
                         {
                             Id = s.Id,
                             Expr1 = sd.Product_TbleId,
                             Name = sd.Product_Tble.Name,
                             Sub_Name = s.Sup_Name,
                             OrderDate = s.OrderDate,
                             QtyIn = sd.QtyIn,
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
