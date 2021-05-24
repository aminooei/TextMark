using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Data.Entity;

namespace TextMark
{

    //class Users_TB
    //{
    //    public int ID { get; set; }
    //    public string Username { get; set; }
    //    public string Password { get; set; }
    //    public string ConfirmPassword { get; set; }
    //    public int UserType { get; set; }

    //}

    //class LoginsContext : DbContext
    //{
    //    public DbSet<Users_TB> Logins_List { get; set; }

    //}

    public class Program
    {
        public static void Main(string[] args)
        { 
            //var loginsContext = new LoginsContext();
            CreateHostBuilder(args).Build().Run();
           
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

   
}
