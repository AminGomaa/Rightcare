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
    public class Stor_tblController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Stor_tbl
        public ActionResult Index()
        {
            var stor_Tbls = db.stor_Tbls.Include(s => s.Product_Tble);
            return View(stor_Tbls.ToList());
        }

        // GET: Stor_tbl/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stor_tbl stor_tbl = db.stor_Tbls.Find(id);
            if (stor_tbl == null)
            {
                return HttpNotFound();
            }
            return View(stor_tbl);
        }

        // GET: Stor_tbl/Create
        public ActionResult Create()
        {
            ViewBag.Product_TbleId = new SelectList(db.product_Tbles, "Id", "Name");
            return View();
        }

        // POST: Stor_tbl/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Product_TbleId,Input,Output,Price,desc,OpNo,OpKind,Date,sal,Rsal,Buy,Rbuy")] Stor_tbl stor_tbl)
        {
            if (ModelState.IsValid)
            {
                db.stor_Tbls.Add(stor_tbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Product_TbleId = new SelectList(db.product_Tbles, "Id", "Name", stor_tbl.Product_TbleId);
            return View(stor_tbl);
        }

        // GET: Stor_tbl/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stor_tbl stor_tbl = db.stor_Tbls.Find(id);
            if (stor_tbl == null)
            {
                return HttpNotFound();
            }
            ViewBag.Product_TbleId = new SelectList(db.product_Tbles, "Id", "Name", stor_tbl.Product_TbleId);
            return View(stor_tbl);
        }

        // POST: Stor_tbl/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Product_TbleId,Input,Output,Price,desc,OpNo,OpKind,Date,sal,Rsal,Buy,Rbuy")] Stor_tbl stor_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stor_tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Product_TbleId = new SelectList(db.product_Tbles, "Id", "Name", stor_tbl.Product_TbleId);
            return View(stor_tbl);
        }

        // GET: Stor_tbl/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stor_tbl stor_tbl = db.stor_Tbls.Find(id);
            if (stor_tbl == null)
            {
                return HttpNotFound();
            }
            return View(stor_tbl);
        }

        // POST: Stor_tbl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stor_tbl stor_tbl = db.stor_Tbls.Find(id);
            db.stor_Tbls.Remove(stor_tbl);
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
