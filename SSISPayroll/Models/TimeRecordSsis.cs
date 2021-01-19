using System;
using System.Collections.Generic;

namespace SSISPayroll.Models
{
    public partial class TimeRecordSsis
    {
        public decimal? Staff { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Workgroup { get; set; }
        public decimal? Program { get; set; }
        public string Brass { get; set; }
        public decimal? CountySubService { get; set; }
        public decimal? Activity { get; set; }
        public decimal? Duration { get; set; }
        public decimal? OboDuration { get; set; }
        public decimal? Regarding { get; set; }
        public string Status { get; set; }
        public string Method { get; set; }
        public string Location { get; set; }
        public string CountyCaseNumber { get; set; }
    }
}
