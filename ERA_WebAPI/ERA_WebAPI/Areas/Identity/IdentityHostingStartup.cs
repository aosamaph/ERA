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
            //builder.ConfigureServices((context, services) => {
            //    services.AddDbContext<ERAContext>(options =>
            //        options.UseSqlServer(
            //            context.Configuration.GetConnectionString("ERAContextConnection")));

            //    services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
            //        .AddEntityFrameworkStores<ERAContext>();
            //});
        }
    }
}