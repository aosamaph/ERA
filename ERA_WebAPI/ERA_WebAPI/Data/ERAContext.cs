using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERA_WebAPI.ERA.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ERA_WebAPI.Data
{
    public class ERAContext : IdentityDbContext<AppUser>
    {
        public ERAContext(DbContextOptions<ERAContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<AppUser>(table =>
            {
                table.OwnsOne(
                    u => u.FullName,
                    fullname =>
                    {
                        fullname.Property(f => f.FirstName).HasColumnName("FirstName");
                        fullname.Property(f => f.LastName).HasColumnName("LastName");
                    });
            });

            builder.Entity<Product>(t =>
            {
                t.Property(p => p.UnitPrice).HasColumnType("money");
                t.Property(p => p.Discount).HasColumnType("money");
            });

            builder.Entity<ProductImage>().HasKey(i => new { i.ProductID, i.ImagePath });

        }

        public DbSet<Product> Products { get; set; }
    }
}
