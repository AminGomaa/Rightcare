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
using RightCareSite.Models.ViewModel;

namespace RightCareSite.Controllers
{
    public class MndStkOutsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MndStkOuts
        public ActionResult Index()
        {
            return View(db.mndStkOuts.ToList());
        }

        // GET: MndStkOuts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var query = (from s in db.mndStkIns
                         join cs in db.MND_TBLs on s.MndId equals cs.Id
                         join os in db.mainStores on s.Id equals os.MndStkInId
                         where s.Id == id
                         select new
                         {
                             mnd_nam = cs.MND_NAME,
                             mnd_id = cs.Id,
                             date = s.OrderDate,
                             no = s.Id,
                             prod_id = os.Product_TbleId,
                             prod_Nam = os.Product_Tble.Name,
                             qty = os.QtyOut,
                             price = os.Price,
                             amount = os.Amount
                         }).ToList();
            var stoc = db.mainStores.Where(x => x.MndStkOutId == id);
            return View(stoc.ToList());
        }


        // GET: MndStkOuts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MndStkOuts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MndId,MndName,OrderDate")] MndStkOut mndStkOut)
        {
            if (ModelState.IsValid)
            {
                db.mndStkOuts.Add(mndStkOut);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mndStkOut);
        }

        // GET: MndStkOuts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MndStkOut mndStkOut = db.mndStkOuts.Find(id);
            if (mndStkOut == null)
            {
                return HttpNotFound();
            }
            return View(mndStkOut);
        }

        // POST: MndStkOuts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MndId,MndName,OrderDate")] MndStkOut mndStkOut)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mndStkOut).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mndStkOut);
        }

        // GET: MndStkOuts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MndStkOut mndStkOut = db.mndStkOuts.Find(id);
            if (mndStkOut == null)
            {
                return HttpNotFound();
            }
            return View(mndStkOut);
        }

        // POST: MndStkOuts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MndStkOut mndStkOut = db.mndStkOuts.Find(id);
            db.mndStkOuts.Remove(mndStkOut);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AddOrder()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddOrderAndOrderDetials(OrderViewModel orderViewModel)
        {
            bool status = true;

            var isValidModel = TryUpdateModel(orderViewModel);

            if (isValidModel)
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    var cusname = db.CUST_TBLs.Find(orderViewModel.Cust_TBLId);
                    MndStkOut order = new MndStkOut()
                    {
                        OrderDate = System.DateTime.Now,
                       MndId = orderViewModel.Cust_TBLId,
                       MndName = cusname.CUST_NAME

                    };
                    db.mndStkOuts.Add(order);

                    if (db.SaveChanges() > 0)
                    {
                        int orderID = db.mndStkOuts.Max(o => o.Id);

                        foreach (var item in orderViewModel.Items)
                        {


                            MainStore orderDetails = new MainStore()
                            {
                                MndStkOutId = orderID,
                                Product_TbleId = item.ProductID,
                                Price = item.Quantity,
                                QtyIn = Convert.ToInt32(item.Price),
                                QtyOut = 0,
                                Amount = item.TotalPrice


                            };
                            Stock stock = new Stock()
                            {
                                Prod_Id = item.ProductID,
                                StQty = -Convert.ToInt32(item.Price),
                                Case = "مرتجع مندوب",
                                Date = System.DateTime.Now,
                                MndId = cusname.MND_TBLId,
                                Cust_Id = 0

                            };
                           

                            db.mainStores.Add(orderDetails);
                            db.stocks.Add(stock);


                        }

                        if (db.SaveChanges() > 0)
                        {
                            return new JsonResult { Data = new { status = status, message = "Order Added Successfully" } };
                        }
                    }
                }
            }

            status = false;
            return new JsonResult { Data = new { status = status, message = "Error !" } };
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
