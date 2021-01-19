using System;
using System.Collections.Generic;

namespace SSISPayroll.Models
{
    public partial class PageWebpart
    {
        public int PageWebpartId { get; set; }
        public string Page { get; set; }
        public int? WebpartId { get; set; }
        public int? Position { get; set; }
        public int? Instance { get; set; }
        public int? EligibilityTypeId { get; set; }
    }
}
