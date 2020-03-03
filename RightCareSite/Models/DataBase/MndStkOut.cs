using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RightCareSite.Models.DataBase
{
    public class MndStkOut
    {
        public int Id { get; set; }
        public int MndId { get; set; }
        public string MndName { get; set; }
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
    }
}