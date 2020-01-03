using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
            var mND_TBLs = db.MND_TBLs.Include(m => m.Gov_tbl).Include(m => m.MND_CAT);
            return View(mND_TBLs.ToList());
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
            ViewBag.Gov_tblId = new SelectList(db.Gov_tbls, "Id", "Gov_Name");
            ViewBag.MND_CATId = new SelectList(db.MND_CATs, "Id", "Category");
            return View();
        }

        // POST: MND_TBL/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MND_TBL mND_TBL, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                upload.SaveAs(path);
                mND_TBL.MND_REGON = upload.FileName;
                db.MND_TBLs.Add(mND_TBL);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Gov_tblId = new SelectList(db.Gov_tbls, "Id", "Gov_Name", mND_TBL.Gov_tblId);
            ViewBag.MND_CATId = new SelectList(db.MND_CATs, "Id", "Category", mND_TBL.MND_CATId);
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
            ViewBag.Gov_tblId = new SelectList(db.Gov_tbls, "Id", "Gov_Name", mND_TBL.Gov_tblId);
            ViewBag.MND_CATId = new SelectList(db.MND_CATs, "Id", "Category", mND_TBL.MND_CATId);
            return View(mND_TBL);
        }

        // POST: MND_TBL/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MND_TBL mND_TBL, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string oldPath = Path.Combine(Server.MapPath("~/Uploads"), mND_TBL.MND_REGON);
                if (upload != null)
                {
                    System.IO.File.Delete(oldPath);
                    string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                    upload.SaveAs(path);
                    mND_TBL.MND_REGON = upload.FileName;
                }
                db.Entry(mND_TBL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Gov_tblId = new SelectList(db.Gov_tbls, "Id", "Gov_Name", mND_TBL.Gov_tblId);
            ViewBag.MND_CATId = new SelectList(db.MND_CATs, "Id", "Category", mND_TBL.MND_CATId);
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
