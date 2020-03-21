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
    public class OrderController : Controller
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
                    var cusname = db.CUST_TBLs.Find(orderViewModel.Cust_TBLId);
                    Sal_tbl order = new Sal_tbl()
                    {
                        OrderDate = System.DateTime.Now,
                        Cust_Id = orderViewModel.Cust_TBLId,
                        Cust_Name = cusname.CUST_NAME
                       
                       
                    };
                    db.Sal_Tbls.Add(order);

                    if (db.SaveChanges() > 0)
                    {
                        int orderID = db.Sal_Tbls.Max(o => o.Id);
                        
                        foreach (var item in orderViewModel.Items)
                        {


                            SaleDetails orderDetails = new SaleDetails()
                            {
                                Sal_TblId = orderID,
                                Product_TbleId = item.ProductID,
                                Price = item.Price,
                                QtyOut = item.Quantity,
                                QtyIn = 0,
                                Amount = item.TotalPrice
                               
                               
                            };
                            Stock stock = new Stock() {
                                Prod_Id = item.ProductID,
                                StQty=-item.Quantity,
                                Case= "مبيعات",
                                Date=System.DateTime.Now,
                                MndId=cusname.MND_TBLId,
                                Cust_Id = orderViewModel.Cust_TBLId

                            };
                            Cust_Acount cust = new Cust_Acount()
                            {
                                CUST_TBLId= orderViewModel.Cust_TBLId,
                                SalNo=orderID,
                                RslNo=0,
                                EslNo=0,
                                Amount=item.TotalPrice,
                                Date=System.DateTime.Now
                            };
                            
                            
                            db.SaleDetails.Add(orderDetails);
                            db.stocks.Add(stock);
                            db.cust_Acounts.Add(cust);
                            
                            
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
                    var cusname = db.CUST_TBLs.Find(orderViewModel.Cust_TBLId);
                 Rsal_tbl order = new Rsal_tbl()
                    {
                        OrderDate = System.DateTime.Now,
                        Cust_Id = orderViewModel.Cust_TBLId,
                        Cust_Name = cusname.CUST_NAME


                    };
                    db.rsal_Tbls.Add(order);

                    if (db.SaveChanges() > 0)
                    {
                        int orderID = db.rsal_Tbls.Max(o => o.Id);

                        foreach (var item in orderViewModel.Items)
                        {
                            SaleDetails orderDetails = new SaleDetails()
                            {
                               Rsal_TblId = orderID,
                                Product_TbleId = item.ProductID,
                                Price = item.Price,
                                QtyIn = item.Quantity,
                                QtyOut = 0,
                                Amount = item.TotalPrice
                            };
                            Stock stock = new Stock()
                            {
                                Prod_Id = item.ProductID,
                                StQty = item.Quantity,
                                Case = "مردودات مبيعات",
                                Date = System.DateTime.Now,
                                MndId = cusname.MND_TBLId,
                                Cust_Id=orderViewModel.Cust_TBLId
                            };
                            Cust_Acount cust = new Cust_Acount()
                            {
                                CUST_TBLId= orderViewModel.Cust_TBLId,
                                SalNo = 0,
                                RslNo = orderID,
                                EslNo = 0,
                                Amount = -item.TotalPrice,
                                Date = System.DateTime.Now
                            };


                            db.SaleDetails.Add(orderDetails);
                            db.stocks.Add(stock);
                            db.cust_Acounts.Add(cust);


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