using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RightCareSite.Models;
using RightCareSite.Models.DataBase;

namespace RightCareSite.Controllers
{
    public class Sub_AcountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Sub_Acount
        public ActionResult Index()
        {
            var sub_Acounts = db.sub_Acounts.Include(s => s.Suply_Tbl);
            return View(sub_Acounts.ToList());
        }
        public ActionResult SubAcount(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sub_Acounts = db.sub_Acounts.Include(c => c.Suply_Tbl).Where(c => c.Suply_TblId == id);
            ViewBag.Name = sub_Acounts;
            return View(sub_Acounts.ToList());
        }
        // GET: Sub_Acount/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_Acount sub_Acount = db.sub_Acounts.Find(id);
            if (sub_Acount == null)
            {
                return HttpNotFound();
            }
            return View(sub_Acount);
        }

        // GET: Sub_Acount/Create
        public ActionResult Create()
        {
            ViewBag.Suply_TblId = new SelectList(db.suply_Tbls, "Id", "Name");
            return View();
        }

        // POST: Sub_Acount/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SupId,ByNo,RbyNo,Amount,EslNo,Date,Suply_TblId")] Sub_Acount sub_Acount)
        {
            if (ModelState.IsValid)
            {
                sub_Acount.ByNo = 0;
                sub_Acount.RbyNo = 0;
                sub_Acount.Date = DateTime.Now.Date;
                sub_Acount.EslNo = Convert.ToInt32(db.sub_Acounts.Max(c => c.EslNo + 1));
                sub_Acount.Amount = -sub_Acount.Amount;
                db.sub_Acounts.Add(sub_Acount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Suply_TblId = new SelectList(db.suply_Tbls, "Id", "Name", sub_Acount.Suply_TblId);
            return View(sub_Acount);
        }

        // GET: Sub_Acount/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_Acount sub_Acount = db.sub_Acounts.Find(id);
            if (sub_Acount == null)
            {
                return HttpNotFound();
            }
            ViewBag.Suply_TblId = new SelectList(db.suply_Tbls, "Id", "Name", sub_Acount.Suply_TblId);
            return View(sub_Acount);
        }

        // POST: Sub_Acount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SupId,ByNo,RbyNo,Amount,EslNo,Date,Suply_TblId")] Sub_Acount sub_Acount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sub_Acount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Suply_TblId = new SelectList(db.suply_Tbls, "Id", "Name", sub_Acount.Suply_TblId);
            return View(sub_Acount);
        }

        // GET: Sub_Acount/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sub_Acount sub_Acount = db.sub_Acounts.Find(id);
            if (sub_Acount == null)
            {
                return HttpNotFound();
            }
            return View(sub_Acount);
        }

        // POST: Sub_Acount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sub_Acount sub_Acount = db.sub_Acounts.Find(id);
            db.sub_Acounts.Remove(sub_Acount);
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
