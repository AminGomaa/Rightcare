using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RightCareSite.Models.DataBase
{
    public class SaleDetails
    {
        public int Id { get; set; }
        public int QtyIn { get; set; }
        public int QtyOut { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public int Rsal_TblId { get; set; }
        public int Sal_TblId { get; set; }
        public int Product_TbleId { get; set; }
        [ForeignKey("Product_TbleId")]
        public virtual Product_Tble Product_Tble { get; set; }

    }
}