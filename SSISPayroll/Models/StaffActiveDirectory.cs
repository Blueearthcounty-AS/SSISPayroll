using System;
using System.Collections.Generic;

namespace SSISPayroll.Models
{
    public partial class StaffActiveDirectory
    {
        public byte[] AdObjectGuid { get; set; }
        public string AdSamAccountName { get; set; }
        public string AdCommonName { get; set; }
        public string AdEmail { get; set; }
        public string AdLastName { get; set; }
        public string AdFirstName { get; set; }
        public string AdTitle { get; set; }
        public string AdDepartment { get; set; }
        public string AdManager { get; set; }
        public string AdManagerCommonName { get; set; }
        public string AdTelephone { get; set; }
        public string AdOffice { get; set; }
    }
}
