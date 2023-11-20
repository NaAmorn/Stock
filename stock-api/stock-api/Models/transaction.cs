using System;
using System.Collections.Generic;

namespace stock_api.Models
{
    public partial class transaction
    {
        public transaction()
        {
            transactionDetails = new HashSet<transactionDetail>();
        }

        public int id { get; set; }
        public DateTime transactionDate { get; set; }

        public virtual ICollection<transactionDetail> transactionDetails { get; set; }
    }
}
