using System;
using System.Collections.Generic;

namespace stock_api.Models
{
    public partial class product
    {
        public product()
        {
            transactionDetails = new HashSet<transactionDetail>();
        }

        public string code { get; set; } = null!;
        public string name { get; set; } = null!;
        public double price { get; set; }

        public virtual stock stock { get; set; } = null!;
        public virtual ICollection<transactionDetail> transactionDetails { get; set; }
    }
}
