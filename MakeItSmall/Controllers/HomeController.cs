using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MakeItSmall.Models;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MakeItSmall.Controllers
{

    public class HomeController : Controller
    {
       

        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _configuration = config;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Result(URLModel urlm)
        {

            string connStr = _configuration.GetConnectionString("MyConnString");
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            string query = "INSERT INTO [dbo].[URL_STORE]([BIG_URL],[SMALL_URL])VALUES(@BIG_URL, @SMALL_URL)";
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@BIG_URL", urlm.BIG_URL);
            cmd.Parameters.AddWithValue("@SMALL_URL", urlm.SMALL_URL);
            

            cmd.ExecuteNonQuery();
            
            conn.Close();

            return View(urlm);  
        }
        
        public IActionResult Teste()
        {

            string final = "http://www.google.com.br";
            return Redirect(final);

           
        }
        public IActionResult Redirection(string tempURL)
        {
            Response.Redirect(tempURL);
            return View();
        }
    }
    

}
