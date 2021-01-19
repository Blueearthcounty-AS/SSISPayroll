using System;
using System.Collections.Generic;

namespace SSISPayroll.Models
{
    public partial class DepartmentSetting
    {
        public int DepartmentSettingId { get; set; }
        public string Context { get; set; }
        public string DepartmentCode { get; set; }
        public string Label { get; set; }
    }
}
