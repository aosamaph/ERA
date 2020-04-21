using System;
using ERA_WebAPI.Data;
using ERA_WebAPI.ERA.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(ERA_WebAPI.Areas.Identity.IdentityHostingStartup))]
namespace ERA_WebAPI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                #region Old service moved to startup
                //services.AddDbContext<ERAContext>(options =>
                //    options.UseSqlServer(
                //        context.Configuration.GetConnectionString("ERAContextConnection")));



                //services.AddIdentity<AppUser, IdentityRole>(options =>
                //{
                //    options.Password.RequireDigit = true;
                //    options.Password.RequireLowercase = true;
                //    options.Password.RequiredLength = 5;
                //}).AddEntityFrameworkStores<ERAContext>(); 
                #endregion

            });
        }
    }
}