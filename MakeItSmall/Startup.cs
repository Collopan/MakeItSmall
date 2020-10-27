using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Linq;
using System.Net;
using System.Web;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MakeItSmall.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MakeItSmall.Models;
using MakeItSmall.Controllers;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace MakeItSmall
{
    public class Startup
    {
        private readonly ILogger<HomeController> _logger;


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/{url_return}", async context =>
                {
                    var url_return = context.Request.RouteValues["url_return"];
                    SqlConnection conn = null;
                    SqlDataReader reader = null;

                    string connStr = Configuration.GetConnectionString("MyConnString");
                    conn = new SqlConnection(connStr);
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT [BIG_URL] FROM [dbo].[URL_STORE] WHERE [SMALL_URL] = @URL_RETURN", conn);

                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@URL_RETURN";
                    param.Value = url_return;

                    cmd.Parameters.Add(param);

                    reader = cmd.ExecuteReader();

                    while(reader.Read()){


                        RedirectionController rc = new RedirectionController();
                        rc.Redirection(String.Format("{0}", reader[0]));

                    }
                    reader.Close();
                    conn.Close();      

                });
            });

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        
    }
}
