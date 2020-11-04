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
using System.Reflection.Metadata.Ecma335;

namespace MakeItSmall.Controllers
{

    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Result(URLModel urlm)        
        {
            List<string> lst = CheckSMALL();

            foreach(string lista in lst)
            {
                if(lista == urlm.SMALL_URL)
                {
                    ModelState.AddModelError("Error", "URL Personalizada já existe.");
                    return View("Index");
                }
            }

            SqlConnection conn = new SqlConnection("Server=DESKTOP-28DDBR5\\SQLEXPRESS;Database=MakeItSmall;User Id=renan;Password=24025564;");
            conn.Open();


            string query = "INSERT INTO [dbo].[URL_STORE]([BIG_URL],[SMALL_URL])VALUES(@BIG_URL, @SMALL_URL)";
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@BIG_URL", urlm.BIG_URL);
            cmd.Parameters.AddWithValue("@SMALL_URL", urlm.SMALL_URL);
            

            cmd.ExecuteNonQuery();
            
            conn.Close();

            return View(urlm);  
        }
        
        public List<string> CheckSMALL()
        {

            List<string> lst = new List<string>();

            SqlConnection conn = new SqlConnection("Server=DESKTOP-28DDBR5\\SQLEXPRESS;Database=MakeItSmall;User Id=renan;Password=24025564;");
            conn.Open();

            SqlCommand cmdRead = new SqlCommand("SELECT [SMALL_URL] FROM [dbo].[URL_STORE]", conn);
            SqlDataReader reader = cmdRead.ExecuteReader();

            while (reader.Read())
            {
                lst.Add(Convert.ToString(reader["SMALL_URL"]));
            }                     

            conn.Close();

            return lst;            
        }

        
        
    }
    
    

}
