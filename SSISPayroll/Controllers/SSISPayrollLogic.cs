using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SSISPayroll.Controllers
{
    
    public class SSISPayrollLogic
    {
        private readonly IConfiguration _context;
        SqlConnection connection = new SqlConnection();
        public SSISPayrollLogic( IConfiguration context)
        {
            _context = context;
            var connectionString = _context.GetConnectionString("DefaultConnection");
            connection = new SqlConnection(connectionString);
        }

        public DataTable ssisPayroll_getCalendar(DateTime beginDate, DateTime endDate)
        {
            using (DataTable dt1 = new DataTable())
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand
                { Connection = connection, CommandType = System.Data.CommandType.StoredProcedure, CommandText = "dbo.int_get_pay_periods" };
                command1.Parameters.Add("@empl_nbr", SqlDbType.Date).Value = beginDate;
                command1.Parameters.Add("@empl_nbr", SqlDbType.Date).Value = endDate;
                using (SqlDataAdapter da1 = new SqlDataAdapter(command1)) { da1.Fill(dt1); }
                connection.Close();
                return dt1;
            }
        }
        public DataTable ssisPayroll_getPayroll_staging()
        {
            using (DataTable dt1 = new DataTable())
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand { Connection = connection, CommandType = System.Data.CommandType.StoredProcedure, CommandText = "dbo.ssis_payroll_get_from_ssis_payroll_staging" };
                using (SqlDataAdapter da1 = new SqlDataAdapter(command1)) { da1.Fill(dt1); }
                connection.Close();
                return dt1;
            }
        }
        public DataTable ssisPayroll_getPayroll_staging(string employeenumber)
        {
            using (DataTable dt1 = new DataTable())
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand { Connection = connection, CommandType = System.Data.CommandType.StoredProcedure, CommandText = "dbo.ssis_payroll_get_from_ssis_payroll_staging_1" };
                command1.Parameters.Add("@empl_nbr", SqlDbType.VarChar).Value = employeenumber;
                using (SqlDataAdapter da1 = new SqlDataAdapter(command1)) { da1.Fill(dt1); }
                connection.Close();
                return dt1;
            }
        }
        public DataTable ssisPayroll_getDetail(string employeenumber)
        {
            using (DataTable dt1 = new DataTable())
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand { Connection = connection, CommandType = System.Data.CommandType.StoredProcedure, CommandText = "dbo.ssis_payroll_getbyemployee" };
                command1.Parameters.Add("@empl_nbr", SqlDbType.VarChar).Value = employeenumber;
                using (SqlDataAdapter da1 = new SqlDataAdapter(command1)) { da1.Fill(dt1); }
                connection.Close();
                return dt1;
            }
        }


        public void ssisPayroll_Loading_Record(DateTime sdate, DateTime edate)
        {
            connection.Open();
            SqlCommand command1 = new SqlCommand { Connection = connection, CommandType = System.Data.CommandType.StoredProcedure, CommandText = "dbo.ssis_payroll_1_loading_record", CommandTimeout = 1000 };
            command1.Parameters.Add("@bdate", SqlDbType.Date).Value = sdate;
            command1.Parameters.Add("@Edate", SqlDbType.Date).Value = edate;
            command1.ExecuteNonQuery();
            connection.Close();           
        }
        
        public void ssisPayroll_Coding()
        {           
            connection.Open();
            SqlCommand command1 = new SqlCommand { Connection = connection, CommandType = System.Data.CommandType.StoredProcedure, CommandText = "dbo.ssis_payroll_2_coding"};
            command1.ExecuteNonQuery();            
            connection.Close();
        }

        public void ssisPayroll_Validation()
        {
            connection.Open();
            SqlCommand command1 = new SqlCommand { Connection = connection, CommandType = System.Data.CommandType.StoredProcedure, CommandText = "dbo.ssis_payroll_3_validations" };
            command1.ExecuteNonQuery();           
            connection.Close();
        }

        public int ssisPayroll_CreateFile()
        {
            connection.Open();
            SqlCommand command1 = new SqlCommand { Connection = connection, CommandType = System.Data.CommandType.StoredProcedure, CommandText = "dbo.run_ssis_payroll_package", CommandTimeout=600 };
            _ = command1.Parameters.Add("@result", SqlDbType.Char, 1).Direction = ParameterDirection.Output;
            int i = command1.ExecuteNonQuery();
            int res = Convert.ToInt32(command1.Parameters["@result"].Value);
            connection.Close();
            return res;
        }
    }

    
    
}
