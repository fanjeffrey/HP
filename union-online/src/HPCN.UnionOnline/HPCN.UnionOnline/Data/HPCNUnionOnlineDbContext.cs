using HPCN.UnionOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace HPCN.UnionOnline.Data
{
    public class HPCNUnionOnlineDbContext : DbContext
    {
        public HPCNUnionOnlineDbContext(DbContextOptions<HPCNUnionOnlineDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityProduct> ActivityProducts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
