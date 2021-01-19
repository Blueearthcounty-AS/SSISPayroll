using System;
using System.Collections.Generic;

namespace SSISPayroll.Models
{
    public partial class SsisPayrollOutput
    {
        public int RecordId { get; set; }
        public string EmplNum { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string PayCode { get; set; }
        public decimal? Quantity { get; set; }
        public string UnitType { get; set; }
        public string Organization { get; set; }
        public string Object { get; set; }
        public string Project { get; set; }
        public string HsDataFlag { get; set; }
        public string ClientNum { get; set; }
        public string Hcpc { get; set; }
        public string ActivityCode { get; set; }
        public string AttendeeCount { get; set; }
        public string ServiceLocation { get; set; }
        public string CaseDesc { get; set; }
        public string UnitCode { get; set; }
        public string Amount { get; set; }
        public string ProcessOperator { get; set; }
        public DateTime? ProcessDate { get; set; }
        public string ProcessFile { get; set; }
    }
}
