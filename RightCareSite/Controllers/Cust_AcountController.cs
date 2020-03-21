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
    public class Cust_AcountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cust_Acount
        public ActionResult Index()
        {
            var cust_Acounts = db.cust_Acounts.Include(c => c.CUST_TBL).Where(c=>c.EslNo!=0);
            return View(cust_Acounts.ToList());
        }
        public ActionResult CustAccount(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cust_Acounts = db.cust_Acounts.Include(c => c.CUST_TBL).Where(c => c.CUST_TBLId == id);
            ViewBag.Name = cust_Acounts;
            return View(cust_Acounts.ToList());
        }
        

        // GET: Cust_Acount/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cust_Acount cust_Acount = db.cust_Acounts.Find(id);
            if (cust_Acount == null)
            {
                return HttpNotFound();
            }
            return View(cust_Acount);
        }

        // GET: Cust_Acount/Create
        public ActionResult Create()
        {
            ViewBag.CUST_TBLId = new SelectList(db.CUST_TBLs, "Id", "CUST_NAME");
            return View();
        }

        // POST: Cust_Acount/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CUST_TBLId,Amount,Date")] Cust_Acount cust_Acount)
        {
            if (ModelState.IsValid)
            {
                cust_Acount.RslNo = 0;
                cust_Acount.RslNo = 0;
                cust_Acount.Date = DateTime.Now.Date;
               cust_Acount.EslNo =Convert.ToInt32(db.cust_Acounts.Max(c => c.EslNo + 1));
                cust_Acount.Amount = -cust_Acount.Amount;
                db.cust_Acounts.Add(cust_Acount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CUST_TBLId = new SelectList(db.CUST_TBLs, "Id", "CUST_NAME", cust_Acount.CUST_TBLId);
            return View(cust_Acount);
        }

        // GET: Cust_Acount/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cust_Acount cust_Acount = db.cust_Acounts.Find(id);
            if (cust_Acount == null)
            {
                return HttpNotFound();
            }
            ViewBag.CUST_TBLId = new SelectList(db.CUST_TBLs, "Id", "CUST_NAME", cust_Acount.CUST_TBLId);
            return View(cust_Acount);
        }

        // POST: Cust_Acount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CUST_TBLId,SalNo,RslNo,Amount,EslNo,Date")] Cust_Acount cust_Acount)
        {
            if (ModelState.IsValid)
            {
                cust_Acount.EslNo = cust_Acount.EslNo;
                db.Entry(cust_Acount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CUST_TBLId = new SelectList(db.CUST_TBLs, "Id", "CUST_NAME", cust_Acount.CUST_TBLId);
            return View(cust_Acount);
        }

        // GET: Cust_Acount/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cust_Acount cust_Acount = db.cust_Acounts.Find(id);
            if (cust_Acount == null)
            {
                return HttpNotFound();
            }
            return View(cust_Acount);
        }

        // POST: Cust_Acount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cust_Acount cust_Acount = db.cust_Acounts.Find(id);
            db.cust_Acounts.Remove(cust_Acount);
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
