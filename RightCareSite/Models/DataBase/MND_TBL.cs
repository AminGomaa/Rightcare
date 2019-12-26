using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RightCareSite.Models.DataBase
{
    public class MND_TBL
    {
        public int Id { get; set; }
        public string MND_NAME { get; set; }
        public string MND_ID { get; set; }

        public string MND_TEL { get; set; }

        public string MND_ADD { get; set; }

        public string MND_REGON { get; set; }

        public string START_DATE { get; set; }
        public int MND_CATId { get; set; }
        public ICollection<CUST_TBL> CUST_TBLs { get; set; }
        public virtual MND_CAT MND_CAT { get; set; }
    }
}