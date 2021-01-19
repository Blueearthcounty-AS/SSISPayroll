using System;
using System.Collections.Generic;

namespace SSISPayroll.Models
{
    public partial class StaffMunis
    {
        public int MunisEmployeeNumber { get; set; }
        public string MunisLastName { get; set; }
        public string MunisFirstName { get; set; }
        public string MunisStatus { get; set; }
        public string MunisPrimaryOrganization { get; set; }
        public string MunisLocation { get; set; }
        public int? MunisSupervisor { get; set; }
        public string MunisEmail { get; set; }
        public string MunisPrimaryObject { get; set; }
    }
}
