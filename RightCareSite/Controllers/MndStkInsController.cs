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
    public class MndStkInsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MndStkIns
        public ActionResult Index()
        {
            return View(db.mndStkIns.ToList());
        }

        // GET: MndStkIns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var query = (from s in db.mndStkIns
                         join cs in db.MND_TBLs on s.MndId equals cs.Id
                         join os in db.mainStores on s.Id equals os.MndStkInId
                         where s.Id== id
             select new
             {
                 mnd_nam = cs.MND_NAME,
                 mnd_id = cs.Id,
                 date=s.OrderDate,
                 no=s.Id,
                 prod_id=os.Product_TbleId,
                 prod_Nam=os.Product_Tble.Name,
                 qty=os.QtyOut,
                 price=os.Price,
                 amount=os.Amount
             }).ToList();
            var stoc = db.mainStores.Where(x => x.MndStkInId == id);
            return View(stoc.ToList());
        }

        // GET: MndStkIns/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MndStkIns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MndId,MndName,OrderDate")] MndStkIn mndStkIn)
        {
            if (ModelState.IsValid)
            {
                db.mndStkIns.Add(mndStkIn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mndStkIn);
        }

        // GET: MndStkIns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MndStkIn mndStkIn = db.mndStkIns.Find(id);
            if (mndStkIn == null)
            {
                return HttpNotFound();
            }
            return View(mndStkIn);
        }

        // POST: MndStkIns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MndId,MndName,OrderDate")] MndStkIn mndStkIn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mndStkIn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mndStkIn);
        }

        // GET: MndStkIns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MndStkIn mndStkIn = db.mndStkIns.Find(id);
            if (mndStkIn == null)
            {
                return HttpNotFound();
            }
            return View(mndStkIn);
        }

        // POST: MndStkIns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MndStkIn mndStkIn = db.mndStkIns.Find(id);
            db.mndStkIns.Remove(mndStkIn);
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
                                MndStkInId = orderID,
                                Product_TbleId = item.ProductID,
                                Price = 0,
                                QtyIn = Convert.ToInt32(item.Price),
                                QtyOut = 0,
                                Amount = 0


                            };
                            Stock stock = new Stock()
                            {
                                Prod_Id = item.ProductID,
                                StQty = Convert.ToInt32(item.Price),
                                Case = "صرف لمندوب",
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
