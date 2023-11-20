using System;
using System.Collections.Generic;

namespace stock_api.Models
{
    public partial class stock
    {
        public string code { get; set; } = null!;
        public int qty { get; set; }

        public virtual product codeNavigation { get; set; } = null!;
    }
}
