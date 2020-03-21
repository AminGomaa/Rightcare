using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RightCareSite.Models.DataBase
{
    public class Product_Tble
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float SelPrice { get; set; }
        public int SQty { get; set; }
        public int Category_tblId { get; set; }
        public virtual Category_tbl Category_Tbl { get; set; }
        public virtual List<SaleDetails> SaleDetails { get; set; }

    }
}