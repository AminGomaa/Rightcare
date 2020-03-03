using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RightCareSite.Models.DataBase
{
    public class MND_TBL
    {
        public int Id { get; set; }
        public string MND_NAME { get; set; }
        public double MND_ID { get; set; }

        public string MND_TEL { get; set; }

        public string MND_ADD { get; set; }

        public string MND_REGON { get; set; }
        [DataType(DataType.Date)]
        public DateTime START_DATE { get; set; }
        public int MND_CATId { get; set; }
        public int Gov_tblId { get; set; }
        public virtual ICollection<CUST_TBL> CUST_TBLs { get; set; }
        public virtual MND_CAT MND_CAT { get; set; }
        public virtual Gov_tbl Gov_tbl { get; set; }
    

    }
}