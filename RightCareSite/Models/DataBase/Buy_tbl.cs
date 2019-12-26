using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RightCareSite.Models.DataBase
{
    public class Buy_tbl
    {
        public int Id { get; set; }
        public float Value { get; set; }
        public int Suply_TblId { get; set; }
        public DateTime DateTime { get; set; }
        public virtual Suply_tbl Suply_Tbl { get; set; }
    }
}