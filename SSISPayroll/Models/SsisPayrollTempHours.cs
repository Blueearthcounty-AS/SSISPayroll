using System;
using System.Collections.Generic;

namespace SSISPayroll.Models
{
    public partial class SsisPayrollTempHours
    {
        public string Empl { get; set; }
        public decimal? Regular { get; set; }
        public decimal? Comp { get; set; }
        public decimal? Ot { get; set; }
        public decimal? Holiday { get; set; }
    }
}
