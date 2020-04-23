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
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(
                new { Id = "1", Name = "admin", NormalizedName = "ADMIN" },
                new { Id = "2", Name = "user", NormalizedName = "USER" }
            );

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

            builder.Entity<Order>().HasMany(o => o.OrderDetails).WithOne();

            builder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.TotalPrice)
                    .HasColumnName("totalPrice")
                    .HasColumnType("money");

                entity.Property(e => e.UserId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Order_User1");
            });

            builder.Entity<OrderDetails>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.OrderId });

                entity.Property(e => e.ProductId).HasColumnName("product_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.NumberOfItems).HasDefaultValue(1);


                //entity.HasOne(d => d.Order)
                //    .WithMany(p => p.OrderDetails)
                //    .HasForeignKey(d => d.OrderId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_OrderDetails_Order1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Product1");
            });

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
    }
}
