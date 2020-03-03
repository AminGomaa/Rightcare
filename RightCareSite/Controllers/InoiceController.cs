using RightCareSite.Models;
using RightCareSite.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RightCareSite.Controllers
{
    public class InoiceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Inoice
        public ActionResult SalInvoice(int? id)
        {
            OrderViewModel model = new OrderViewModel();

            var query = (from s in db.Sal_Tbls
                         join sd in db.SaleDetails on s.Id equals sd.Sal_TblId
                         join p in db.product_Tbles on sd.Product_TbleId equals p.Id
                         where s.Id == 1
                         select new
                         {
                             
                         }).ToList();
            return View("SalInvoice",query);
           
        }
    }
}