using System;
using System.Collections.Generic;

namespace SSISPayroll.Models
{
    public partial class PageLayout
    {
        public int PageLayoutId { get; set; }
        public string Page { get; set; }
        public int? LayoutId { get; set; }
    }
}
