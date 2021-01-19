using System;
using System.Collections.Generic;

namespace SSISPayroll.Models
{
    public partial class PageAdministrator
    {
        public int PageAdministratorId { get; set; }
        public string Page { get; set; }
        public string GroupName { get; set; }
        public int? StaffId { get; set; }
        public string UserTitle { get; set; }
        public string UserName { get; set; }
        public string WorkstationName { get; set; }
    }
}
