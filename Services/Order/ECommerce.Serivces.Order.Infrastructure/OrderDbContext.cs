using ECommerce.Services.Order.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Serivces.Order.Infrastructure
{
    public class OrderDbContext:DbContext
    {
        public const string DEFAULT_SCHEMA = "ordering";

        public OrderDbContext(DbContextOptions<OrderDbContext>options):base(options) 
        {

        }

        public DbSet<ECommerce.Services.Order.Domain.OrderAggregate.Order> Orders { get; set; }
        public DbSet<ECommerce.Services.Order.Domain.OrderAggregate.OrderItem> orderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ECommerce.Services.Order.Domain.OrderAggregate.Order>().ToTable("Orders",DEFAULT_SCHEMA);
            modelBuilder.Entity<ECommerce.Services.Order.Domain.OrderAggregate.OrderItem>().ToTable("OrderItems", DEFAULT_SCHEMA);
            modelBuilder.Entity<ECommerce.Services.Order.Domain.OrderAggregate.OrderItem>().Property(x => x.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<ECommerce.Services.Order.Domain.OrderAggregate.Order>().OwnsOne(X => X.Address).WithOwner();
            base.OnModelCreating(modelBuilder);
        }
    }
}
