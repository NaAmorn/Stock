using System;
using System.Collections.Generic;

namespace stock_api.Models
{
    public partial class transactionDetail
    {
        public int id { get; set; }
        public int tranId { get; set; }
        public string code { get; set; } = null!;
        public int qty { get; set; }

        public virtual product codeNavigation { get; set; } = null!;
        public virtual transaction tran { get; set; } = null!;
    }
}
