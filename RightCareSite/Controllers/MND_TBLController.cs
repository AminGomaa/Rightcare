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
    public class MND_TBLController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MND_TBL
        public ActionResult Index()
        {
            return View(db.MND_TBLs.ToList());
        }

        // GET: MND_TBL/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MND_TBL mND_TBL = db.MND_TBLs.Find(id);
            if (mND_TBL == null)
            {
                return HttpNotFound();
            }
            return View(mND_TBL);
        }

        // GET: MND_TBL/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MND_TBL/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MND_NAME,MND_ID,MND_TEL,MND_ADD,MND_REGON,START_DATE")] MND_TBL mND_TBL)
        {
            if (ModelState.IsValid)
            {
                db.MND_TBLs.Add(mND_TBL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mND_TBL);
        }

        // GET: MND_TBL/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MND_TBL mND_TBL = db.MND_TBLs.Find(id);
            if (mND_TBL == null)
            {
                return HttpNotFound();
            }
            return View(mND_TBL);
        }

        // POST: MND_TBL/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MND_NAME,MND_ID,MND_TEL,MND_ADD,MND_REGON,START_DATE")] MND_TBL mND_TBL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mND_TBL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mND_TBL);
        }

        // GET: MND_TBL/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MND_TBL mND_TBL = db.MND_TBLs.Find(id);
            if (mND_TBL == null)
            {
                return HttpNotFound();
            }
            return View(mND_TBL);
        }

        // POST: MND_TBL/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MND_TBL mND_TBL = db.MND_TBLs.Find(id);
            db.MND_TBLs.Remove(mND_TBL);
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
