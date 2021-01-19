using System;
using System.Collections.Generic;

namespace SSISPayroll.Models
{
    public partial class StaffSsis
    {
        public decimal SsisStaffId { get; set; }
        public string SsisFirstName { get; set; }
        public string SsisLastName { get; set; }
        public string SsisPhone { get; set; }
        public string SsisStaffNumber { get; set; }
        public string SsisTitle { get; set; }
        public string SsisActive { get; set; }
        public string SsisEmail { get; set; }
    }
}
