using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SSISPayroll.Models
{
    public partial class HSDBContext : DbContext
    {
        public HSDBContext()
        {
        }

        public HSDBContext(DbContextOptions<HSDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PayPeriodTable> PayPeriodTable { get; set; }
        public virtual DbSet<SsisPayrollOutput> SsisPayrollOutput { get; set; }
        public virtual DbSet<SsisPayrollStaging> SsisPayrollStaging { get; set; }
        public virtual DbSet<SsisPayrollTempHours> SsisPayrollTempHours { get; set; }
        public virtual DbSet<SsisRoleBrassConversion> SsisRoleBrassConversion { get; set; }
        public virtual DbSet<SsisTimeRecord> SsisTimeRecord { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<StaffActiveDirectory> StaffActiveDirectory { get; set; }
       
        public virtual DbSet<StaffMunis> StaffMunis { get; set; }
        
        public virtual DbSet<StaffSsis> StaffSsis { get; set; }
        
        public virtual DbSet<TimeRecordSsis> TimeRecordSsis { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("DefaultConnection");
                //Server = BECMUNISAPP; Database = HSDB; user id = HSISGeneralUser; password = HSIS;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            modelBuilder.Entity<PayPeriodTable>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("pay_period_table");

                entity.Property(e => e.PayPerId)
                    .HasColumnName("pay_per_id");

                entity.Property(e => e.BeginDate)
                    .HasColumnName("begin_date")
                    .HasColumnType("date");

                entity.Property(e => e.EndDate)
                    .HasColumnName("end_date")
                    .HasColumnType("date");

                entity.Property(e => e.PayYear)
                    .HasColumnName("pay_year")
                    .HasMaxLength(4);

                entity.Property(e => e.HolHours)
                    .HasColumnName("hol_hours")
                    .HasColumnType("numeric(2, 0)");

                entity.Property(e => e.DatePaid)
                   .HasColumnName("date_paid")
                   .HasColumnType("date");

                entity.Property(e => e.Active)
                   .HasColumnName("active")
                   .HasMaxLength(1);

                entity.Property(e => e.PayPeriod)
                   .HasColumnName("pay_period")
                   .HasMaxLength(50);
            });

            modelBuilder.Entity<SsisPayrollOutput>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ssis_payroll_output");

                entity.Property(e => e.ActivityCode)
                    .HasColumnName("activity_code")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.AttendeeCount)
                    .HasColumnName("attendee_count")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.CaseDesc)
                    .HasColumnName("case_desc")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClientNum)
                    .HasColumnName("client_num")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.EmplNum)
                    .HasColumnName("empl_num")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.FromDate)
                    .HasColumnName("from_date")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Hcpc)
                    .HasColumnName("hcpc")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.HsDataFlag)
                    .HasColumnName("hs_data_flag")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('N')");

                entity.Property(e => e.Object)
                    .HasColumnName("object")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.Organization)
                    .HasColumnName("organization")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.PayCode)
                    .HasColumnName("pay_code")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.ProcessDate)
                    .HasColumnName("process_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProcessFile)
                    .HasColumnName("process_file")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProcessOperator)
                    .HasColumnName("process_operator")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Project)
                    .HasColumnName("project")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .HasColumnType("decimal(5, 2)");

                entity.Property(e => e.RecordId)
                    .HasColumnName("record_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ServiceLocation)
                    .HasColumnName("service_location")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ToDate)
                    .HasColumnName("to_date")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UnitCode)
                    .HasColumnName("unit_code")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.UnitType)
                    .HasColumnName("unit_type")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SsisPayrollStaging>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ssis_payroll_staging");

                entity.Property(e => e.ActDate)
                    .HasColumnName("act_date")
                    .HasColumnType("date");

                entity.Property(e => e.Activity)
                    .IsRequired()
                    .HasColumnName("activity")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Age)
                    .HasColumnName("age")
                    .HasMaxLength(3);

                entity.Property(e => e.AnewDate)
                    .HasColumnName("anew_date")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BadCase)
                    .HasColumnName("bad_case")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BadCaseCnt).HasColumnName("bad_case_cnt");

                entity.Property(e => e.BillWarn)
                    .HasColumnName("bill_warn")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Brass)
                    .HasColumnName("brass")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.BrassError)
                    .HasColumnName("brass_Error")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BrassSvcId)
                    .HasColumnName("brass_svc_id")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.CDate)
                    .HasColumnName("c_date")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.CaseNumber)
                    .HasColumnName("case_number")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ClientId).HasColumnName("client_id");

                entity.Property(e => e.ClientName)
                    .HasColumnName("client_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CntySubSvc).HasColumnName("cnty_sub_svc");

                entity.Property(e => e.CoaErrCnt).HasColumnName("coa_err_cnt");

                entity.Property(e => e.CoaError)
                    .HasColumnName("coa_error")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Comp)
                    .HasColumnName("comp")
                    .HasColumnType("numeric(5, 2)");

                entity.Property(e => e.ConStat)
                    .HasColumnName("con_stat")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Cused)
                    .HasColumnName("cused")
                    .HasColumnType("numeric(5, 2)");

                entity.Property(e => e.Dept)
                    .HasColumnName("dept")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DirectTime)
                    .HasColumnName("direct_time")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Dob)
                    .HasColumnName("dob")
                    .HasColumnType("datetime");

                entity.Property(e => e.EligId)
                    .HasColumnName("elig_id")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.Fdpso)
                    .HasColumnName("fdpso")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Fund)
                    .HasColumnName("fund")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Furlough)
                    .HasColumnName("furlough")
                    .HasColumnType("numeric(5, 2)");

                entity.Property(e => e.Hcpc)
                    .HasColumnName("hcpc")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.HolidayHours)
                    .HasColumnName("holiday_hours")
                    .HasColumnType("numeric(5, 2)");

                entity.Property(e => e.HourCode)
                    .HasColumnName("hour_code")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Hours)
                    .HasColumnName("hours")
                    .HasColumnType("numeric(5, 2)");

                entity.Property(e => e.Location)
                    .HasColumnName("location")
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Meth)
                    .HasColumnName("meth")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Mod)
                    .HasColumnName("mod")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.NDate)
                    .HasColumnName("n_date")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Newdate)
                    .HasColumnName("newdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.NumberPeople)
                    .HasColumnName("number_people")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Objt)
                    .HasColumnName("objt")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.OrgCode)
                    .HasColumnName("org_code")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.OrgErrCnt).HasColumnName("org_err_cnt");

                entity.Property(e => e.OrgError)
                    .HasColumnName("org_error")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ot)
                    .HasColumnName("ot")
                    .HasColumnType("numeric(5, 2)");

                entity.Property(e => e.OtherTime)
                    .HasColumnName("other_time")
                    .HasColumnType("numeric(5, 2)");

                entity.Property(e => e.PayCode)
                    .HasColumnName("pay_code")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.PayDesc)
                    .HasColumnName("pay_desc")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PersonId)
                    .HasColumnName("person_id")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.Pmi)
                    .HasColumnName("pmi")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.Program)
                    .HasColumnName("program")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.RecId)
                    .HasColumnName("rec_id")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Regular)
                    .HasColumnName("regular")
                    .HasColumnType("numeric(5, 2)");

                entity.Property(e => e.Service)
                    .HasColumnName("service")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Sick)
                    .HasColumnName("sick")
                    .HasColumnType("numeric(5, 2)");

                entity.Property(e => e.SsisKey)
                    .HasColumnName("ssis_key")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Ssisid)
                    .HasColumnName("ssisid")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SubProg).HasColumnName("sub_prog");

                entity.Property(e => e.TheDate)
                    .HasColumnName("the_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.TimeRecordId)
                    .HasColumnName("time_record_id")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.TimeSpent)
                    .HasColumnName("time_spent")
                    .HasColumnType("numeric(5, 2)");

                entity.Property(e => e.Trid)
                    .HasColumnName("trid")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Vac)
                    .HasColumnName("vac")
                    .HasColumnType("numeric(5, 2)");

                entity.Property(e => e.WaveCode)
                    .HasColumnName("wave_code")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.WaveEnd)
                    .HasColumnName("wave_end")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WaveStart)
                    .HasColumnName("wave_start")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WgId)
                    .HasColumnName("wg_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Worker)
                    .HasColumnName("worker")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SsisPayrollTempHours>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ssis_payroll_temp_hours");

                entity.Property(e => e.Comp)
                    .HasColumnName("comp")
                    .HasColumnType("numeric(5, 2)");

                entity.Property(e => e.Empl)
                    .HasColumnName("empl")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Holiday)
                    .HasColumnName("holiday")
                    .HasColumnType("numeric(5, 2)");

                entity.Property(e => e.Ot)
                    .HasColumnName("ot")
                    .HasColumnType("numeric(5, 2)");

                entity.Property(e => e.Regular)
                    .HasColumnName("regular")
                    .HasColumnType("numeric(5, 2)");
            });

            modelBuilder.Entity<SsisRoleBrassConversion>(entity =>
            {

                entity.ToTable("ssis_role_brass_conversion");

                entity.Property(e => e.SsisRoleBrassConversionId).HasColumnName("ssis_role_brass_conversion_id");

                entity.Property(e => e.Brass)
                    .HasColumnName("brass")
                    .HasColumnType("numeric(4, 0)");

                entity.Property(e => e.OrgCode)
                    .HasColumnName("org_code")
                    .HasColumnType("numeric(8, 0)");

                entity.Property(e => e.SsisRoleId)
                    .HasColumnName("ssis_role_id")
                    .HasColumnType("numeric(12, 0)");
            });

            modelBuilder.Entity<SsisTimeRecord>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ssis_time_record");

                entity.Property(e => e.ActivityDt)
                    .HasColumnName("activity_dt")
                    .HasColumnType("datetime");

                entity.Property(e => e.BrassSvcId)
                    .HasColumnName("brass_svc_id")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.CntyAcctgCode)
                    .HasColumnName("cnty_acctg_code")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CntyOfSvcCd)
                    .HasColumnName("cnty_of_svc_cd")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.CntySubsvcId)
                    .HasColumnName("cnty_subsvc_id")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.ContactLocCd)
                    .HasColumnName("contact_loc_cd")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.ContactMethodCd)
                    .HasColumnName("contact_method_cd")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.ContactStatusCd)
                    .HasColumnName("contact_status_cd")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.DocumentId)
                    .HasColumnName("document_id")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.Duration)
                    .HasColumnName("duration")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.HcpcCd)
                    .HasColumnName("hcpc_cd")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.LastChgdBy)
                    .HasColumnName("last_chgd_by")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.LastChgdDt)
                    .HasColumnName("last_chgd_dt")
                    .HasColumnType("datetime");

                entity.Property(e => e.NumPersRecSvc)
                    .HasColumnName("num_pers_rec_svc")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Purpose)
                    .HasColumnName("purpose")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.StaffId)
                    .HasColumnName("staff_id")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.SubprogId)
                    .HasColumnName("subprog_id")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.TimeRecordId)
                    .HasColumnName("time_record_id")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.TrActivityId)
                    .HasColumnName("tr_activity_id")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.TrStatCd)
                    .HasColumnName("tr_stat_cd")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.WgId)
                    .HasColumnName("wg_id")
                    .HasColumnType("numeric(10, 0)");
            });

            modelBuilder.Entity<Staff>(entity =>
            {
                entity.ToTable("staff");

                entity.Property(e => e.StaffId).HasColumnName("staff_id");

                entity.Property(e => e.Accounting).HasColumnName("accounting");

                entity.Property(e => e.AccountingSupervisor).HasColumnName("accounting_supervisor");

                entity.Property(e => e.AdCommonName).HasColumnName("ad_common_name");

                entity.Property(e => e.AdDepartment).HasColumnName("ad_department");

                entity.Property(e => e.AdEmail).HasColumnName("ad_email");

                entity.Property(e => e.AdFirstName).HasColumnName("ad_first_name");

                entity.Property(e => e.AdLastName).HasColumnName("ad_last_name");

                entity.Property(e => e.AdManager).HasColumnName("ad_manager");

                entity.Property(e => e.AdManagerCommonName).HasColumnName("ad_manager_common_name");

                entity.Property(e => e.AdObjectGuid).HasColumnName("ad_object_guid");

                entity.Property(e => e.AdOffice).HasColumnName("ad_office");

                entity.Property(e => e.AdSamAccountName).HasColumnName("ad_sam_account_name");

                entity.Property(e => e.AdTelephone).HasColumnName("ad_telephone");

                entity.Property(e => e.AdTitle).HasColumnName("ad_title");

                entity.Property(e => e.Administration).HasColumnName("administration");

                entity.Property(e => e.AdministrationSupervisor).HasColumnName("administration_supervisor");

                entity.Property(e => e.AdultServices).HasColumnName("adult_services");

                entity.Property(e => e.AdultServicesSupervisor).HasColumnName("adult_services_supervisor");

                entity.Property(e => e.BehavioralHealth).HasColumnName("behavioral_health");

                entity.Property(e => e.BehavioralHealthSupervisor).HasColumnName("behavioral_health_supervisor");

                entity.Property(e => e.Billing).HasColumnName("billing");

                entity.Property(e => e.BillingSupervisor).HasColumnName("billing_supervisor");

                entity.Property(e => e.CarelogicActive).HasColumnName("carelogic_active");

                entity.Property(e => e.CarelogicEmployeeId).HasColumnName("carelogic_employee_id");

                entity.Property(e => e.CarelogicFirstName).HasColumnName("carelogic_first_name");

                entity.Property(e => e.CarelogicFullName).HasColumnName("carelogic_full_name");

                entity.Property(e => e.CarelogicLastName).HasColumnName("carelogic_last_name");

                entity.Property(e => e.CarelogicNpi).HasColumnName("carelogic_npi");

                entity.Property(e => e.CarelogicStaffId).HasColumnName("carelogic_staff_id");

                entity.Property(e => e.ChildAndFamilyServices).HasColumnName("child_and_family_services");

                entity.Property(e => e.ChildAndFamilyServicesSupervisor).HasColumnName("child_and_family_services_supervisor");

                entity.Property(e => e.ChildSupport).HasColumnName("child_support");

                entity.Property(e => e.ChildSupportSupervisor).HasColumnName("child_support_supervisor");

                entity.Property(e => e.ChildrensMentalHealth).HasColumnName("childrens_mental_health");

                entity.Property(e => e.ChildrensMentalHealthSupervisor).HasColumnName("childrens_mental_health_supervisor");

                entity.Property(e => e.CreatedDateTime)
                    .HasColumnName("created_date_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.FinancialAssistance).HasColumnName("financial_assistance");

                entity.Property(e => e.FinancialAssistanceSupervisor).HasColumnName("financial_assistance_supervisor");

                entity.Property(e => e.Licensing).HasColumnName("licensing");

                entity.Property(e => e.LicensingSupervisor).HasColumnName("licensing_supervisor");

                entity.Property(e => e.ManagerStaffId).HasColumnName("manager_staff_id");

                entity.Property(e => e.MentalHealthCenter).HasColumnName("mental_health_center");

                entity.Property(e => e.MentalHealthCenterSupervisor).HasColumnName("mental_health_center_supervisor");

                entity.Property(e => e.MissingFromAd).HasColumnName("missing_from_ad");

                entity.Property(e => e.MunisEmail).HasColumnName("munis_email");

                entity.Property(e => e.MunisEmployeeNumber).HasColumnName("munis_employee_number");

                entity.Property(e => e.MunisEmployeeNumberFormatted).HasColumnName("munis_employee_number_formatted");

                entity.Property(e => e.MunisFirstName).HasColumnName("munis_first_name");

                entity.Property(e => e.MunisLastName).HasColumnName("munis_last_name");

                entity.Property(e => e.MunisLocation).HasColumnName("munis_location");

                entity.Property(e => e.MunisPrimaryObject).HasColumnName("munis_primary_object");

                entity.Property(e => e.MunisPrimaryOrganization).HasColumnName("munis_primary_organization");

                entity.Property(e => e.MunisStatus).HasColumnName("munis_status");

                entity.Property(e => e.MunisSupervisor).HasColumnName("munis_supervisor");

                entity.Property(e => e.OfficeSupport).HasColumnName("office_support");

                entity.Property(e => e.OfficeSupportSupervisor).HasColumnName("office_support_supervisor");

                entity.Property(e => e.PhdocActive)
                    .HasColumnName("phdoc_active")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PhdocEmployeeCode)
                    .HasColumnName("phdoc_employee_code")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PhdocEmployeeKey)
                    .HasColumnName("phdoc_employee_key")
                    .HasColumnType("numeric(5, 0)");

                entity.Property(e => e.PhdocFirstName).HasColumnName("phdoc_first_name");

                entity.Property(e => e.PhdocFullName).HasColumnName("phdoc_full_name");

                entity.Property(e => e.PhdocLastName).HasColumnName("phdoc_last_name");

                entity.Property(e => e.PreferredFirstName).HasColumnName("preferred_first_name");

                entity.Property(e => e.PreferredLastName).HasColumnName("preferred_last_name");

                entity.Property(e => e.PreferredNameSet).HasColumnName("preferred_name_set");

                entity.Property(e => e.PrimaryUnit).HasColumnName("primary_unit");

                entity.Property(e => e.PublicHealth).HasColumnName("public_health");

                entity.Property(e => e.PublicHealthSupervisor).HasColumnName("public_health_supervisor");

                entity.Property(e => e.Sccbi).HasColumnName("sccbi");

                entity.Property(e => e.SccbiSupervisor).HasColumnName("sccbi_supervisor");

                entity.Property(e => e.SmiName).HasColumnName("smi_name");

                entity.Property(e => e.SmiPhone).HasColumnName("smi_phone");

                entity.Property(e => e.SmiSupervisorId).HasColumnName("smi_supervisor_id");

                entity.Property(e => e.SmiSupervisorName).HasColumnName("smi_supervisor_name");

                entity.Property(e => e.SmiSupervisorPhone).HasColumnName("smi_supervisor_phone");

                entity.Property(e => e.SmiWorkerId).HasColumnName("smi_worker_id");

                entity.Property(e => e.SoftDelete).HasColumnName("soft_delete");

                entity.Property(e => e.SsisActive)
                    .HasColumnName("ssis_active")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SsisEmail).HasColumnName("ssis_email");

                entity.Property(e => e.SsisFirstName).HasColumnName("ssis_first_name");

                entity.Property(e => e.SsisLastName).HasColumnName("ssis_last_name");

                entity.Property(e => e.SsisPhone).HasColumnName("ssis_phone");

                entity.Property(e => e.SsisStaffId)
                    .HasColumnName("ssis_staff_id")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.SsisStaffNumber).HasColumnName("ssis_staff_number");

                entity.Property(e => e.SsisTitle).HasColumnName("ssis_title");

                entity.Property(e => e.Sud).HasColumnName("sud");

                entity.Property(e => e.SudSupervisor).HasColumnName("sud_supervisor");

                entity.Property(e => e.SupportiveHousing).HasColumnName("supportive_housing");

                entity.Property(e => e.SupportiveHousingSupervisor).HasColumnName("supportive_housing_supervisor");

                entity.Property(e => e.Ylp).HasColumnName("ylp");

                entity.Property(e => e.YlpSupervisor).HasColumnName("ylp_supervisor");
            });

            modelBuilder.Entity<StaffActiveDirectory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("staff_active_directory");

                entity.Property(e => e.AdCommonName)
                    .HasColumnName("ad_common_name")
                    .HasMaxLength(4000);

                entity.Property(e => e.AdDepartment)
                    .HasColumnName("ad_department")
                    .HasMaxLength(4000);

                entity.Property(e => e.AdEmail)
                    .HasColumnName("ad_email")
                    .HasMaxLength(4000);

                entity.Property(e => e.AdFirstName)
                    .HasColumnName("ad_first_name")
                    .HasMaxLength(4000);

                entity.Property(e => e.AdLastName)
                    .HasColumnName("ad_last_name")
                    .HasMaxLength(4000);

                entity.Property(e => e.AdManager)
                    .HasColumnName("ad_manager")
                    .HasMaxLength(4000);

                entity.Property(e => e.AdManagerCommonName)
                    .HasColumnName("ad_manager_common_name")
                    .HasMaxLength(4000);

                entity.Property(e => e.AdObjectGuid)
                    .HasColumnName("ad_object_guid")
                    .HasMaxLength(4000);

                entity.Property(e => e.AdOffice)
                    .HasColumnName("ad_office")
                    .HasMaxLength(4000);

                entity.Property(e => e.AdSamAccountName)
                    .HasColumnName("ad_sam_account_name")
                    .HasMaxLength(4000);

                entity.Property(e => e.AdTelephone)
                    .HasColumnName("ad_telephone")
                    .HasMaxLength(4000);

                entity.Property(e => e.AdTitle)
                    .HasColumnName("ad_title")
                    .HasMaxLength(4000);
            });
           
            modelBuilder.Entity<StaffMunis>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("staff_munis");

                entity.Property(e => e.MunisEmail)
                    .HasColumnName("munis_email")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.MunisEmployeeNumber).HasColumnName("munis_employee_number");

                entity.Property(e => e.MunisFirstName)
                    .HasColumnName("munis_first_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MunisLastName)
                    .HasColumnName("munis_last_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MunisLocation)
                    .HasColumnName("munis_location")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MunisPrimaryObject)
                    .IsRequired()
                    .HasColumnName("munis_primary_object")
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.MunisPrimaryOrganization)
                    .IsRequired()
                    .HasColumnName("munis_primary_organization")
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.MunisStatus)
                    .IsRequired()
                    .HasColumnName("munis_status")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.MunisSupervisor).HasColumnName("munis_supervisor");
            });

            modelBuilder.Entity<StaffSsis>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("staff_ssis");

                entity.Property(e => e.SsisActive)
                    .HasColumnName("ssis_active")
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SsisEmail)
                    .HasColumnName("ssis_email")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SsisFirstName)
                    .IsRequired()
                    .HasColumnName("ssis_first_name")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SsisLastName)
                    .IsRequired()
                    .HasColumnName("ssis_last_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.SsisPhone)
                    .HasColumnName("ssis_phone")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.SsisStaffId)
                    .HasColumnName("ssis_staff_id")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.SsisStaffNumber)
                    .HasColumnName("ssis_staff_number")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SsisTitle)
                    .HasColumnName("ssis_title")
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });
           
            modelBuilder.Entity<TimeRecordSsis>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("time_record_ssis");

                entity.Property(e => e.Activity).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.Brass)
                    .HasMaxLength(21)
                    .IsUnicode(false);

                entity.Property(e => e.CountyCaseNumber)
                    .HasColumnName("County Case Number")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CountySubService)
                    .HasColumnName("County sub-service")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Duration).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.Location)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Method)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.OboDuration)
                    .HasColumnName("OBO duration")
                    .HasColumnType("numeric(10, 0)");

                entity.Property(e => e.Program).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.Regarding).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.Staff).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Workgroup).HasColumnType("numeric(10, 0)");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
