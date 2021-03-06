﻿using System;
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
    public class Suply_tblController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Suply_tbl
        public ActionResult Index()
        {
            var suply_Tbls = db.suply_Tbls.Include(s => s.Governorate);
            return View(suply_Tbls.ToList());
        }

        // GET: Suply_tbl/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suply_tbl suply_tbl = db.suply_Tbls.Find(id);
            if (suply_tbl == null)
            {
                return HttpNotFound();
            }
            return View(suply_tbl);
        }

        // GET: Suply_tbl/Create
        public ActionResult Create()
        {
            ViewBag.GovernorateId = new SelectList(db.Governorates, "Id", "Gov_Name");
            return View();
        }

        // POST: Suply_tbl/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Phone,Address,GovernorateId")] Suply_tbl suply_tbl)
        {
            if (ModelState.IsValid)
            {
                db.suply_Tbls.Add(suply_tbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GovernorateId = new SelectList(db.Governorates, "Id", "Gov_Name", suply_tbl.GovernorateId);
            return View(suply_tbl);
        }

        // GET: Suply_tbl/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suply_tbl suply_tbl = db.suply_Tbls.Find(id);
            if (suply_tbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.GovernorateId = new SelectList(db.Governorates, "Id", "Gov_Name", suply_tbl.GovernorateId);
            return View(suply_tbl);
        }

        // POST: Suply_tbl/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Phone,Address,GovernorateId")] Suply_tbl suply_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(suply_tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GovernorateId = new SelectList(db.Governorates, "Id", "Gov_Name", suply_tbl.GovernorateId);
            return View(suply_tbl);
        }

        // GET: Suply_tbl/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Suply_tbl suply_tbl = db.suply_Tbls.Find(id);
            if (suply_tbl == null)
            {
                return HttpNotFound();
            }
            return View(suply_tbl);
        }

        // POST: Suply_tbl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Suply_tbl suply_tbl = db.suply_Tbls.Find(id);
            db.suply_Tbls.Remove(suply_tbl);
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
        public ActionResult Sub_Account(int? id)
        {
            var query = (from s in db.CUST_TBLs
                         join sd in db.cust_Acounts on s.Id equals sd.CUST_TBLId
                         where s.Id == id
                         select new
                         {
                             Id = s.Id,
                             CUST_NAME = s.CUST_NAME,
                             SalNo = sd.SalNo,
                             RslNo = sd.RslNo,
                             Amount = sd.Amount,
                             EslNo = sd.EslNo,
                             Date = sd.Date
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
