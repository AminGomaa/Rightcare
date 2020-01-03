using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }
      
        public int sal { get; set; }
        public int Rsal { get; set; }
        public int Buy { get; set; }
        public int Rbuy { get; set; }
        public ICollection<CUST_TBL> CUST_TBLs { get; set; }
        public ICollection<Suply_tbl> Suply_Tbls { get; set; }
        public virtual MND_TBL MND_TBL { get; set; }
        public virtual Product_Tble Product_Tble { get; set; }

    }
}