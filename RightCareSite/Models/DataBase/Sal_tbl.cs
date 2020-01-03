using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RightCareSite.Models.DataBase
{
    public class Sal_tbl
    {
        public int Id { get; set; }
        public int Cust_TBLId { get; set; }
        public float Value { get; set; }

        public virtual CUST_TBL CUST_TBL { get; set; }
    }
}