using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RightCareSite.Models.DataBase
{
    public class Sub_Acount
    {
        public int Id { get; set; }
        public int Suply_TblId { get; set; }

        public int ByNo { get; set; }
        public int RbyNo { get; set; }
        public decimal Amount { get; set; }
        public int EslNo { get; set; }
        [DataType(DataType.Date)]

        public DateTime Date { get; set; }
        public virtual Suply_tbl Suply_Tbl { get; set; }
    }
}