using System;
using System.Collections.Generic;

namespace SSISPayroll.Models
{
    public partial class PayPeriodTable
    {
        public int PayPerId { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PayYear { get; set; }
        public int HolHours { get; set; }
        public DateTime DatePaid { get; set; }
        public string Active { get; set; }
        public string PayPeriod { get; set; }

    }
}
