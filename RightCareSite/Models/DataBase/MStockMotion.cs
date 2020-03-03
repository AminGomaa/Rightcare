using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RightCareSite.Models.DataBase
{
    public class MStockMotion
    {
        public int Id { get; set; }
        public int Prod_Id { get; set; }
        public int MndId { get; set; }
        public int SubId { get; set; }
        public float StQty { get; set; }
        public string Case { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}