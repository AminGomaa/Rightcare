using RightCareSite.Models;
using RightCareSite.Models.DataBase;
using RightCareSite.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RightCareSite.Controllers
{
    public class PurchController : Controller
    {
        // GET: Order
        // GET: Orders
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
                    var subname = db.suply_Tbls.Find(orderViewModel.Cust_TBLId);

                    Buy_tbl order = new Buy_tbl()
                    {
                        OrderDate = System.DateTime.Now,
                        Sup_Id = orderViewModel.Cust_TBLId,
                        Sup_Name = subname.Name
                    };
                    
                    db.buy_Tbls.Add(order);

                    if (db.SaveChanges() > 0)
                    {
                        int orderID = db.buy_Tbls.Max(o => o.Id);

                        foreach (var item in orderViewModel.Items)
                        {


                            MainStore orderDetails = new MainStore()
                            {
                                Buy_tblId = orderID,
                                Product_TbleId = item.ProductID,
                                Price = Convert.ToInt32(item.Price),
                                QtyIn = item.Quantity,
                                QtyOut = 0,
                                Amount = item.TotalPrice,
                                Date = DateTime.Now

                            };
                            var stck = db.product_Tbles.Where(p => p.Id == item.ProductID).FirstOrDefault();
                            stck.SQty = stck.SQty + item.Quantity;
                            Sub_Acount sub = new Sub_Acount()
                            {
                             Suply_TblId= orderViewModel.Cust_TBLId,
                             ByNo = orderID,
                              
                                Amount = item.TotalPrice,
                                Date = System.DateTime.Now
                            };


                            db.mainStores.Add(orderDetails);
                            db.sub_Acounts.Add(sub);


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
        public ActionResult AddReturn()
        {
            return View();
        }
        public JsonResult AddReturnDetials(OrderViewModel orderViewModel)
        {
            bool status = true;

            var isValidModel = TryUpdateModel(orderViewModel);

            if (isValidModel)
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    var subname = db.suply_Tbls.Find(orderViewModel.Cust_TBLId);
                 ReBUy_tbl  order = new ReBUy_tbl()
                    {
                        OrderDate = System.DateTime.Now,
                      Sup_Id = orderViewModel.Cust_TBLId,
                     Sup_Name = subname.Name


                    };
                    db.rbuy_Tbls.Add(order);

                    if (db.SaveChanges() > 0)
                    {
                        int orderID = db.rsal_Tbls.Max(o => o.Id);

                        foreach (var item in orderViewModel.Items)
                        {
                        MainStore orderDetails = new MainStore()
                            {
                               ReBUy_tblId = orderID,
                                Product_TbleId = item.ProductID,
                                Price = item.Price,
                                QtyOut = item.Quantity,
                                QtyIn = 0,
                                Amount = item.TotalPrice,
                            Date = DateTime.Now

                        };
                            var stck = db.product_Tbles.Where(p => p.Id == item.ProductID).FirstOrDefault();
                            stck.SQty = stck.SQty + item.Quantity;
                            Sub_Acount sub = new Sub_Acount()
                            {
                          Suply_TblId = orderViewModel.Cust_TBLId,
                                RbyNo=orderID,
                                ByNo=0,
                                EslNo = 0,
                                Amount = -item.TotalPrice,
                                Date = System.DateTime.Now
                            };

                           
                            db.mainStores.Add(orderDetails);
                            db.sub_Acounts.Add(sub);


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
        public ActionResult AddMndStock()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddMndStockDetials(OrderViewModel orderViewModel)
        {
            bool status = true;

            var isValidModel = TryUpdateModel(orderViewModel);

            if (isValidModel)
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    var mndname = db.MND_TBLs.Find(orderViewModel.Cust_TBLId);

                    MndStkIn order = new MndStkIn()
                    {
                        OrderDate = System.DateTime.Now,
                      MndId = orderViewModel.Cust_TBLId,
                      MndName = mndname.MND_NAME
                    };

                    db.mndStkIns.Add(order);

                    if (db.SaveChanges() > 0)
                    {
                        int orderID = db.mndStkIns.Max(o => o.Id);

                        foreach (var item in orderViewModel.Items)
                        {


                            MainStore orderDetails = new MainStore()
                            {
                                MndStkInId = orderID,
                                Product_TbleId = item.ProductID,
                                Price = item.Price,
                                QtyOut = item.Quantity,
                                QtyIn = 0,
                                Date = DateTime.Now,
                                Amount = item.TotalPrice

                            };
                            Stock stock = new Stock()
                            {
                                Prod_Id=item.ProductID,
                                MndId=mndname.Id,
                                StQty=item.Quantity,
                                Date=DateTime.Now,
                                Case="منصرف لمندوب",
                                Cust_Id=0

                            };

                            var stck = db.product_Tbles.Where(p => p.Id == item.ProductID).FirstOrDefault();
                            stck.SQty = stck.SQty - item.Quantity;
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
        public ActionResult OutMndStock()
        {
            return View();
        }

        [HttpPost]
        public JsonResult OutMndStockDetials(OrderViewModel orderViewModel)
        {
            bool status = true;

            var isValidModel = TryUpdateModel(orderViewModel);

            if (isValidModel)
            {
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    var mndname = db.MND_TBLs.Find(orderViewModel.Cust_TBLId);

               MndStkOut  order = new MndStkOut()
                    {
                        OrderDate = System.DateTime.Now,
                        MndId = orderViewModel.Cust_TBLId,
                        MndName = mndname.MND_NAME
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
                                Price = item.Price,
                                QtyIn = item.Quantity,
                                QtyOut = 0,
                                Date = DateTime.Now,

                                Amount = item.TotalPrice


                            };
                            Stock stock = new Stock()
                            {
                                Prod_Id = item.ProductID,
                                MndId = mndname.Id,
                                StQty = -item.Quantity,
                                Date = DateTime.Now,
                                Case = "مرتجع مندوب",
                                Cust_Id = 0

                            };
                            var stk = db.product_Tbles.Where(x => x.Id == item.ProductID).FirstOrDefault();
                            stk.SQty = stk.SQty + item.Quantity;
                            db.stocks.Add(stock);
                            db.mainStores.Add(orderDetails);


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

    }
}