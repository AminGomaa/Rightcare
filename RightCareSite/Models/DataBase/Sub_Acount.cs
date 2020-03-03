using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RightCareSite.Models.DataBase
{
    public class Sub_Acount
    {
        public int Id { get; set; }
        public int SupId { get; set; }
        public int ByNo { get; set; }
        public int RbyNo { get; set; }
        public decimal Amount { get; set; }
        public int EslNo { get; set; }
        public DateTime Date { get; set; }
    }
}