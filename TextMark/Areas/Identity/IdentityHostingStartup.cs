using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TextMark.Areas.Identity.Data;
using TextMark.Data;

[assembly: HostingStartup(typeof(TextMark.Areas.Identity.IdentityHostingStartup))]
namespace TextMark.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<IdentityContext>(options =>
                    options.UseSqlServer(
                        //   context.Configuration.GetConnectionString("IdentityContextConnection")));
                        context.Configuration.GetConnectionString("TextMarkConnectionSTR")));
                services.AddDefaultIdentity<TextMarkUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<IdentityContext>();
            });
        }
    }
}