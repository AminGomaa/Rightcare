using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RightCareSite.Models.DataBase
{
    public class Gov_tbl
    {
        public int Id { get; set; }
        public string Gov_Name { get; set; }
        public virtual ICollection<MND_TBL> MND_TBLs { get; set; }

    }
}