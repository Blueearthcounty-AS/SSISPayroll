using System;
using System.Collections.Generic;

namespace SSISPayroll.Models
{
    public partial class SsisTimeRecord
    {
        public decimal? TimeRecordId { get; set; }
        public decimal? StaffId { get; set; }
        public decimal? WgId { get; set; }
        public decimal? TrActivityId { get; set; }
        public decimal? SubprogId { get; set; }
        public decimal? BrassSvcId { get; set; }
        public decimal? CntySubsvcId { get; set; }
        public decimal? DocumentId { get; set; }
        public decimal? Duration { get; set; }
        public DateTime? ActivityDt { get; set; }
        public string HcpcCd { get; set; }
        public string NumPersRecSvc { get; set; }
        public string CntyOfSvcCd { get; set; }
        public string TrStatCd { get; set; }
        public string CntyAcctgCode { get; set; }
        public string ContactMethodCd { get; set; }
        public string ContactLocCd { get; set; }
        public string ContactStatusCd { get; set; }
        public string Purpose { get; set; }
        public decimal? LastChgdBy { get; set; }
        public DateTime? LastChgdDt { get; set; }
    }
}
