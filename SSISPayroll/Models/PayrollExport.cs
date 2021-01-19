using System;
using System.Collections.Generic;

namespace SSISPayroll.Models
{
    public partial class PayrollExport
    {
        public int PayrollExportId { get; set; }
        public string Label { get; set; }
        public string Path { get; set; }
    }
}
