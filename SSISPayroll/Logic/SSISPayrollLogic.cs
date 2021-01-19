using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SSISPayroll.Logic
{
    
    public class SSISPayrollLogic
    {
        private readonly IConfiguration _context;
        SqlConnection connection = new SqlConnection();
        public SSISPayrollLogic( IConfiguration context)
        {
 
            _context = context;
            string connectionString = _context.GetConnectionString("DefaultConnection");
            connection = new SqlConnection(connectionString);
        }

        public DataTable ssisPayroll_getCalendar(DateTime beginDate, DateTime endDate)
        {
            DataTable dt1 = new DataTable();
            string connectionString = _context.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command1 = new SqlCommand();
            command1.Connection = connection;
            command1.CommandType = System.Data.CommandType.StoredProcedure;
            command1.Parameters.Add("@empl_nbr", SqlDbType.Date).Value = beginDate;
            command1.Parameters.Add("@empl_nbr", SqlDbType.Date).Value = endDate;
            command1.CommandText = "dbo.int_get_pay_periods";
            using (SqlDataAdapter da1 = new SqlDataAdapter(command1))
            {
                da1.Fill(dt1);
            }
            connection.Close();

            return dt1;
        }
        public DataTable ssisPayroll_getPayroll_staging()
        {
            DataTable dt1 = new DataTable();

            connection.Open();
            SqlCommand command1 = new SqlCommand();
            command1.Connection = connection;
            command1.CommandType = System.Data.CommandType.StoredProcedure;
            command1.CommandText = "dbo.ssis_payroll_get_from_ssis_payroll_staging_test";


            using (SqlDataAdapter da1 = new SqlDataAdapter(command1))
            {
                da1.Fill(dt1);
            }
            connection.Close();

            return dt1;
        }
        public DataTable ssisPayroll_getDetail(string employeenumber)
        {
            DataTable dt1 = new DataTable();
            connection.Open();
            SqlCommand command1 = new SqlCommand();
            command1.Connection = connection;
            command1.CommandType = System.Data.CommandType.StoredProcedure;
            command1.CommandText = "dbo.ssis_payroll_getbyemployee";
            command1.Parameters.Add("@empl_nbr", SqlDbType.VarChar).Value = employeenumber;
            using (SqlDataAdapter da1 = new SqlDataAdapter(command1))
            {
                da1.Fill(dt1);
            }
            connection.Close();

            return dt1;
        }


        public DataTable ssisPayroll_Loading_Record()
        {
            DataTable dt1 = new DataTable();
            connection.Open();
            SqlCommand command1 = new SqlCommand();
            command1.Connection = connection;
            command1.CommandType = System.Data.CommandType.StoredProcedure;
            command1.CommandText = "dbo.ssis_payroll_1_loading_record";
            using (SqlDataAdapter da1 = new SqlDataAdapter(command1))
            {
                da1.Fill(dt1);
            }
            connection.Close();

            return dt1;
        }
        public DataTable ssisPayroll_Coding()
        {
            DataTable dt1 = new DataTable();
            connection.Open();
            SqlCommand command1 = new SqlCommand();
            command1.Connection = connection;
            command1.CommandType = System.Data.CommandType.StoredProcedure;
            command1.CommandText = "dbo.ssis_payroll_2_coding";
            using (SqlDataAdapter da1 = new SqlDataAdapter(command1))
            {
                da1.Fill(dt1);
            }
            connection.Close();

            return dt1;
        }
        public DataTable ssisPayroll_Validation()
        {
            DataTable dt1 = new DataTable();
            connection.Open();
            SqlCommand command1 = new SqlCommand();
            command1.Connection = connection;
            command1.CommandType = System.Data.CommandType.StoredProcedure;
            command1.CommandText = "dbo.ssis_payroll_3_validations";
            using (SqlDataAdapter da1 = new SqlDataAdapter(command1))
            {
                da1.Fill(dt1);
            }
            connection.Close();

            return dt1;
        }
    }

    
    
}
