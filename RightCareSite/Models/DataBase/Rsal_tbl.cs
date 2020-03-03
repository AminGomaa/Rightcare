using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RightCareSite.Models.DataBase
{
    public class Rsal_tbl
    {
        public int Id { get; set; }
        public int Cust_Id { get; set; }
        public string Cust_Name { get; set; }
      
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
    }
}