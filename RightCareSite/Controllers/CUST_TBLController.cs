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
    public class CUST_TBLController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CUST_TBL
        public ActionResult Index()
        {
            var cUST_TBLs = db.CUST_TBLs.Include(c => c.Governorate).Include(c => c.MND_TBL);
            return View(cUST_TBLs.ToList());
        }

        // GET: CUST_TBL/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUST_TBL cUST_TBL = db.CUST_TBLs.Find(id);
            if (cUST_TBL == null)
            {
                return HttpNotFound();
            }
            return View(cUST_TBL);
        }

        // GET: CUST_TBL/Create
        public ActionResult Create()
        {
            ViewBag.GovernorateId = new SelectList(db.Governorates, "Id", "Gov_Name");
            ViewBag.MND_TBLId = new SelectList(db.MND_TBLs, "Id", "MND_NAME");
            return View();
        }

        // POST: CUST_TBL/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CUST_NAME,CUST_TEL,CUST_ADD,MND_TBLId,GovernorateId")] CUST_TBL cUST_TBL)
        {
            if (ModelState.IsValid)
            {
                db.CUST_TBLs.Add(cUST_TBL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GovernorateId = new SelectList(db.Governorates, "Id", "Gov_Name", cUST_TBL.GovernorateId);
            ViewBag.MND_TBLId = new SelectList(db.MND_TBLs, "Id", "MND_NAME", cUST_TBL.MND_TBLId);
            return View(cUST_TBL);
        }

        // GET: CUST_TBL/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUST_TBL cUST_TBL = db.CUST_TBLs.Find(id);
            if (cUST_TBL == null)
            {
                return HttpNotFound();
            }
            ViewBag.GovernorateId = new SelectList(db.Governorates, "Id", "Gov_Name", cUST_TBL.GovernorateId);
            ViewBag.MND_TBLId = new SelectList(db.MND_TBLs, "Id", "MND_NAME", cUST_TBL.MND_TBLId);
            return View(cUST_TBL);
        }

        // POST: CUST_TBL/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CUST_NAME,CUST_TEL,CUST_ADD,MND_TBLId,GovernorateId")] CUST_TBL cUST_TBL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cUST_TBL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GovernorateId = new SelectList(db.Governorates, "Id", "Gov_Name", cUST_TBL.GovernorateId);
            ViewBag.MND_TBLId = new SelectList(db.MND_TBLs, "Id", "MND_NAME", cUST_TBL.MND_TBLId);
            return View(cUST_TBL);
        }

        // GET: CUST_TBL/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUST_TBL cUST_TBL = db.CUST_TBLs.Find(id);
            if (cUST_TBL == null)
            {
                return HttpNotFound();
            }
            return View(cUST_TBL);
        }

        // POST: CUST_TBL/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CUST_TBL cUST_TBL = db.CUST_TBLs.Find(id);
            db.CUST_TBLs.Remove(cUST_TBL);
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
        public ActionResult Cust_Account(int? id)
        {
            var query = (from s in db.CUST_TBLs
                         join sd in db.cust_Acounts on s.Id equals sd.CUST_TBLId
                         where s.Id == id
                         select new
                         {Id=s.Id,
                             CUST_NAME = s.CUST_NAME,
                             SalNo=sd.SalNo,
                             RslNo=sd.RslNo,
                             Amount=sd.Amount,
                             EslNo=sd.EslNo,
                             Date=sd.Date
                         }).ToList();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "CustomerAccountRpt.rpt"));
            rd.SetDataSource(query);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "كشف حساب عميل.pdf");
            }
            catch
            {
                throw;
            }
        }
    }
}
