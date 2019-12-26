using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RightCareSite.Models.DataBase
{
    public class MND_CAT
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public ICollection<MND_TBL> MND_TBLs { get; set; }

    }
}