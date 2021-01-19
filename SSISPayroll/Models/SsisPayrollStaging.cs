using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSISPayroll.Models
{
    public partial class SsisPayrollStaging
    {
        [Key]
        public int id { get; set; }
        public DateTime? ActDate { get; set; }
        public string NumberPeople { get; set; }
        public string DirectTime { get; set; }
        public decimal? TimeSpent { get; set; }
        public string CaseNumber { get; set; }
        public string Program { get; set; }
        public string Dept { get; set; }
        public string Hcpc { get; set; }
        public string Service { get; set; }
        public string Activity { get; set; }
        public string Worker { get; set; }
        public string Mod { get; set; }
        public string Meth { get; set; }
        public string BadCase { get; set; }
        public string CoaError { get; set; }
        public string Fund { get; set; }
        public string Brass { get; set; }
        public int SsisKey { get; set; }
        public int? CoaErrCnt { get; set; }
        public int? BadCaseCnt { get; set; }
        public string Objt { get; set; }
        public string ClientName { get; set; }
        public string HourCode { get; set; }
        public string PayDesc { get; set; }
        public DateTime? Dob { get; set; }
        public string Age { get; set; }
        public int? ClientId { get; set; }
        public string PayCode { get; set; }
        public string OrgCode { get; set; }
        public string BrassError { get; set; }
        public string OrgError { get; set; }
        public string Fdpso { get; set; }
        public decimal? CDate { get; set; }
        public string NDate { get; set; }
        public string RecId { get; set; }
        public string Location { get; set; }
        public int? OrgErrCnt { get; set; }
        public decimal? HolidayHours { get; set; }
        public decimal? OtherTime { get; set; }
        public string WgId { get; set; }
        public DateTime? Newdate { get; set; }
        public decimal? Hours { get; set; }
        public string Ssisid { get; set; }
        public string Trid { get; set; }
        public string ConStat { get; set; }
        public int? SubProg { get; set; }
        public int? CntySubSvc { get; set; }
        public decimal? Comp { get; set; }
        public decimal? Ot { get; set; }
        public decimal? Regular { get; set; }
        public decimal? Sick { get; set; }
        public decimal? Vac { get; set; }
        public decimal? Furlough { get; set; }
        public decimal? Cused { get; set; }
        public decimal? TimeRecordId { get; set; }
        public decimal? BrassSvcId { get; set; }
        public decimal? PersonId { get; set; }
        public decimal? Pmi { get; set; }
        public decimal? EligId { get; set; }
        public string WaveCode { get; set; }
        public string WaveStart { get; set; }
        public string WaveEnd { get; set; }
        public DateTime? TheDate { get; set; }
        public string BillWarn { get; set; }
        public string AnewDate { get; set; }
    }
}
