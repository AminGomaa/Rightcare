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
    public class Product_TbleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Product_Tble
        public ActionResult Index()
        {
            var product_Tbles = db.product_Tbles.Include(p => p.Category_Tbl);
            return View(product_Tbles.ToList());
        }

        // GET: Product_Tble/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Tble product_Tble = db.product_Tbles.Find(id);
            if (product_Tble == null)
            {
                return HttpNotFound();
            }
            return View(product_Tble);
        }

        // GET: Product_Tble/Create
        public ActionResult Create()
        {
            ViewBag.Category_tblId = new SelectList(db.Category_Tbls, "Id", "Category");
            return View();
        }

        // POST: Product_Tble/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,SelPrice,Category_tblId")] Product_Tble product_Tble)
        {
            if (ModelState.IsValid)
            {
                db.product_Tbles.Add(product_Tble);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Category_tblId = new SelectList(db.Category_Tbls, "Id", "Category", product_Tble.Category_tblId);
            return View(product_Tble);
        }

        // GET: Product_Tble/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Tble product_Tble = db.product_Tbles.Find(id);
            if (product_Tble == null)
            {
                return HttpNotFound();
            }
            ViewBag.Category_tblId = new SelectList(db.Category_Tbls, "Id", "Category", product_Tble.Category_tblId);
            return View(product_Tble);
        }

        // POST: Product_Tble/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,SelPrice,Category_tblId")] Product_Tble product_Tble)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product_Tble).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Category_tblId = new SelectList(db.Category_Tbls, "Id", "Category", product_Tble.Category_tblId);
            return View(product_Tble);
        }

        // GET: Product_Tble/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product_Tble product_Tble = db.product_Tbles.Find(id);
            if (product_Tble == null)
            {
                return HttpNotFound();
            }
            return View(product_Tble);
        }

        // POST: Product_Tble/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product_Tble product_Tble = db.product_Tbles.Find(id);
            db.product_Tbles.Remove(product_Tble);
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
