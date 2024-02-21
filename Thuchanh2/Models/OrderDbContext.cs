using Microsoft.EntityFrameworkCore;

namespace Thuchanh2.Models
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ProductOrders> ProductOrders { get; set; }


    }
}
