using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RightCareSite.Models.DataBase
{
    public class MainStore
    {
        public int Id { get; set; }
        public int QtyIn { get; set; }
        public int QtyOut { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public int MndStkOutId { get; set; }
        public int MndStkInId { get; set; }
        public int ReBUy_tblId { get; set; }
        public int Buy_tblId { get; set; }
        public int Product_TbleId { get; set; }
        [ForeignKey("Product_TbleId")]
        public virtual Product_Tble Product_Tble { get; set; }
    }
}