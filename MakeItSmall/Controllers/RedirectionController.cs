using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MakeItSmall.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MakeItSmall.Controllers
{
    public static class Utils
    {
        public static string Redirection(string url_return)
        {
            string final = null;
        
            SqlConnection conn = new SqlConnection("Server=DESKTOP-28DDBR5\\SQLEXPRESS;Database=MakeItSmall;User Id=renan;Password=24025564;");
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT [BIG_URL] FROM [dbo].[URL_STORE] WHERE [SMALL_URL] = @URL_RETURN", conn);
            cmd.Parameters.AddWithValue("@URL_RETURN", url_return);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                final = (String.Format("{0}", reader[0]));
            }
            else
            {
                return "URL of page not found";
            }

            reader.Close();
            conn.Close();

        return final;
        }
    }
}
