using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RightCareSite.Models.DataBase
{
    public class Rbuy_tbl
    {
        public int Id { get; set; }
        public float Value { get; set; }
        public int Suply_TblId { get; set; }
        public ICollection<Stor_tbl> Stor_Tbls { get; set; }

        public virtual Suply_tbl Suply_Tbl { get; set; }
    }
}