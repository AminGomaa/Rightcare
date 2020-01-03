using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RightCareSite.Models.DataBase
{
    public class Governorate
    {
        public int Id { get; set; }
        public string Gov_Name { get; set; }
        public ICollection<Suply_tbl> suply_Tbls { get; set; }
        public ICollection<CUST_TBL> CUST_TBLs { get; set; }
    }
}