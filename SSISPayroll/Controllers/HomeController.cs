using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SSISPayroll.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;
using SSISPayroll.Logic;

namespace SSISPayroll.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _context;
        DataTable dt3 = new DataTable();
        public HomeController(ILogger<HomeController> logger, IConfiguration context)
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

        public IActionResult Detail()
        {
            SSISPayrollLogic logic = new SSISPayrollLogic(_context);

            DataTable dt1 = logic.ssisPayroll_getPayroll_staging();

            return View(dt1);

        }

        [HttpPost]
        public IActionResult Detail(DateTime date1, DateTime date2)
        {
            SSISPayrollLogic logic = new SSISPayrollLogic(_context);

           // logic.ssisPayroll_Loading_Record(date1, date2);
            
            logic.ssisPayroll_Coding();
            
            logic.ssisPayroll_Validation();

            DataTable dt1 = logic.ssisPayroll_getPayroll_staging();


            return View(dt1);

        }

        public IActionResult SSISPayrollFullDetail(string employeenbr)
        {
            SSISPayrollLogic logic = new SSISPayrollLogic(_context);
            DataTable dt0 = logic.ssisPayroll_getPayroll_staging(employeenbr);




            DataTable dt1;
            dt1 = logic.ssisPayroll_getDetail(employeenbr);


            ViewData["empl_name"] = dt0.Rows[0][dt0.Columns["empl_name"]].ToString();
            ViewData["empl_nbr"] = dt0.Rows[0][dt0.Columns["empl_nbr"]].ToString();
            ViewData["reg_hrs"] = dt0.Rows[0][dt0.Columns["regular"]].ToString();
            ViewData["hol_hrs"] = dt0.Rows[0][dt0.Columns["hol_hrs"]].ToString();
            ViewData["comp"] = dt0.Rows[0][dt0.Columns["comp"]].ToString();
            ViewData["ot"] = dt0.Rows[0][dt0.Columns["overtime"]].ToString();




            return PartialView("_DetailPartial", dt1);
        }

        [HttpPost]
        public IActionResult SSIStoMunis()
        {
            SSISPayrollLogic logic = new SSISPayrollLogic(_context);
            ViewData["result1"] = logic.ssisPayroll_CreateFile();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
