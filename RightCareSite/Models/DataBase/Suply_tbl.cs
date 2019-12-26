using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RightCareSite.Models.DataBase
{
    public class Suply_tbl
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Buy_tbl> Buy_Tbls { get; set; }
        public virtual ICollection<Stor_tbl> Stor_Tbls { get; set; }
    }
}