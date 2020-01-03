using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RightCareSite.Models.DataBase
{
    public class CUST_TBL
    {
        public int Id { get; set; }
        public string CUST_NAME { get; set; }
        public string CUST_TEL { get; set; }
        public string CUST_ADD { get; set; }
        public int MND_TBLId { get; set; }
        public int GovernorateId { get; set; }
        public virtual Governorate Governorate { get; set; }
        public virtual MND_TBL MND_TBL { get; set; }
        public virtual ICollection<Sal_tbl> Sal_Tbls { get; set; }
        public virtual ICollection<Rsal_tbl> Rsal_Tbls { get; set; }

      

    }
}