using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RightCareSite.Models.DataBase
{
    public class Cust_Acount
    {
        public int Id { get; set; }
        public int CUST_TBLId { get; set; }
        public int SalNo { get; set; }
        public int RslNo { get; set; }
        public decimal Amount { get; set; }
        public int EslNo { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public virtual CUST_TBL CUST_TBL{get;set;}
    }
}