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
    public class Category_tblController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Category_tbl
        public ActionResult Index()
        {
            return View(db.Category_Tbls.ToList());
        }

        // GET: Category_tbl/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category_tbl category_tbl = db.Category_Tbls.Find(id);
            if (category_tbl == null)
            {
                return HttpNotFound();
            }
            return View(category_tbl);
        }

        // GET: Category_tbl/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Category_tbl/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Category")] Category_tbl category_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Category_Tbls.Add(category_tbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category_tbl);
        }

        // GET: Category_tbl/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category_tbl category_tbl = db.Category_Tbls.Find(id);
            if (category_tbl == null)
            {
                return HttpNotFound();
            }
            return View(category_tbl);
        }

        // POST: Category_tbl/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Category")] Category_tbl category_tbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category_tbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category_tbl);
        }

        // GET: Category_tbl/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category_tbl category_tbl = db.Category_Tbls.Find(id);
            if (category_tbl == null)
            {
                return HttpNotFound();
            }
            return View(category_tbl);
        }

        // POST: Category_tbl/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category_tbl category_tbl = db.Category_Tbls.Find(id);
            db.Category_Tbls.Remove(category_tbl);
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
