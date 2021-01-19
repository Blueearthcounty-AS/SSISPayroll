using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SSISPayroll.Controllers
{
    public class PayPeriodController : Controller
    {

        private readonly ILogger<PayPeriodController> _logger;
        private readonly IConfiguration _context;
        DataTable dt3 = new DataTable();
        public PayPeriodController(ILogger<PayPeriodController> logger, IConfiguration context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            string connectionString = _context.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(connectionString);

            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand
            {
                Connection = connection,
                CommandType = System.Data.CommandType.StoredProcedure,
                CommandText = "dbo.int_get_pay_periods"
            };
            using (SqlDataAdapter da = new SqlDataAdapter(command))
            {
                da.Fill(dt);
                da.Fill(dt3);
            }
            connection.Close();

            return View(dt);
        }
        public IActionResult AddPayPeriod()
        {
            string connectionString = _context.GetConnectionString("DefaultConnection");
            SqlConnection connection = new SqlConnection(connectionString);

            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand
            {
                Connection = connection,
                CommandType = System.Data.CommandType.StoredProcedure,
                CommandText = "dbo.myproc"

            };
            command.ExecuteNonQuery();            
            connection.Close();

            return RedirectToAction("Index");
        }
    }
}