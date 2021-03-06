﻿using System;
using System.Collections.Generic;

namespace SSISPayroll.Models
{
    public partial class Staff
    {
        public int StaffId { get; set; }
        public string AdObjectGuid { get; set; }
        public string AdSamAccountName { get; set; }
        public string AdCommonName { get; set; }
        public string AdLastName { get; set; }
        public string AdFirstName { get; set; }
        public string AdTitle { get; set; }
        public string AdDepartment { get; set; }
        public string AdEmail { get; set; }
        public string AdTelephone { get; set; }
        public string AdOffice { get; set; }
        public string AdManager { get; set; }
        public string AdManagerCommonName { get; set; }
        public int? ManagerStaffId { get; set; }
        public int? MunisEmployeeNumber { get; set; }
        public bool? MissingFromAd { get; set; }
        public bool? SoftDelete { get; set; }
        public string MunisLastName { get; set; }
        public string MunisFirstName { get; set; }
        public string MunisStatus { get; set; }
        public string MunisPrimaryOrganization { get; set; }
        public string MunisLocation { get; set; }
        public string MunisSupervisor { get; set; }
        public string MunisEmail { get; set; }
        public string MunisPrimaryObject { get; set; }
        public string CarelogicEmployeeId { get; set; }
        public string CarelogicStaffId { get; set; }
        public string CarelogicFirstName { get; set; }
        public string CarelogicLastName { get; set; }
        public string CarelogicFullName { get; set; }
        public string CarelogicActive { get; set; }
        public string CarelogicNpi { get; set; }
        public decimal? SsisStaffId { get; set; }
        public string SsisFirstName { get; set; }
        public string SsisLastName { get; set; }
        public string SsisPhone { get; set; }
        public string SsisStaffNumber { get; set; }
        public string SsisTitle { get; set; }
        public string SsisActive { get; set; }
        public string SsisEmail { get; set; }
        public decimal? PhdocEmployeeKey { get; set; }
        public string PhdocEmployeeCode { get; set; }
        public string PhdocFullName { get; set; }
        public string PhdocLastName { get; set; }
        public string PhdocFirstName { get; set; }
        public string PhdocActive { get; set; }
        public string MunisEmployeeNumberFormatted { get; set; }
        public string PreferredFirstName { get; set; }
        public string PreferredLastName { get; set; }
        public bool? PreferredNameSet { get; set; }
        public string PrimaryUnit { get; set; }
        public bool? Administration { get; set; }
        public bool? AdministrationSupervisor { get; set; }
        public bool? Accounting { get; set; }
        public bool? AccountingSupervisor { get; set; }
        public bool? AdultServices { get; set; }
        public bool? AdultServicesSupervisor { get; set; }
        public bool? BehavioralHealth { get; set; }
        public bool? BehavioralHealthSupervisor { get; set; }
        public bool? Billing { get; set; }
        public bool? BillingSupervisor { get; set; }
        public bool? ChildrensMentalHealth { get; set; }
        public bool? ChildrensMentalHealthSupervisor { get; set; }
        public bool? ChildSupport { get; set; }
        public bool? ChildSupportSupervisor { get; set; }
        public bool? FinancialAssistance { get; set; }
        public bool? FinancialAssistanceSupervisor { get; set; }
        public bool? Licensing { get; set; }
        public bool? LicensingSupervisor { get; set; }
        public bool? MentalHealthCenter { get; set; }
        public bool? MentalHealthCenterSupervisor { get; set; }
        public bool? OfficeSupport { get; set; }
        public bool? OfficeSupportSupervisor { get; set; }
        public bool? PublicHealth { get; set; }
        public bool? PublicHealthSupervisor { get; set; }
        public bool? Sccbi { get; set; }
        public bool? SccbiSupervisor { get; set; }
        public bool? Sud { get; set; }
        public bool? SudSupervisor { get; set; }
        public bool? SupportiveHousing { get; set; }
        public bool? SupportiveHousingSupervisor { get; set; }
        public bool? Ylp { get; set; }
        public bool? YlpSupervisor { get; set; }
        public string SmiWorkerId { get; set; }
        public string SmiPhone { get; set; }
        public string SmiSupervisorId { get; set; }
        public string SmiSupervisorName { get; set; }
        public string SmiSupervisorPhone { get; set; }
        public bool? ChildAndFamilyServices { get; set; }
        public bool? ChildAndFamilyServicesSupervisor { get; set; }
        public string SmiName { get; set; }
        public DateTime? CreatedDateTime { get; set; }
    }
}
