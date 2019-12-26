using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RightCareSite.Models.DataBase
{
    public class Stor_tbl
    {
        public int Id { get; set; }
        public int Product_TbleId { get; set; }
        public int Input { get; set; }
        public int Output { get; set; }
        public float Price { get; set; }
        public float desc { get; set; }
        public int OpNo { get; set; }
        public string OpKind { get; set; }
        public int Suply_TblId { get; set; }
        public int CUST_TBLId { get; set; }
        public virtual CUST_TBL CUST_TBL { get; set; }
        public virtual Suply_tbl Suply_Tbl { get; set; }
        public virtual MND_TBL MND_TBL { get; set; }
        public virtual Product_Tble Product_Tble { get; set; }

    }
}