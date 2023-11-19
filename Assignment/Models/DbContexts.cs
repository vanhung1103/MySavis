using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Assignment.Models
{
    public class DbContexts : DbContext
    {
        public DbContexts()
        {

        }
        public DbContexts(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CouponCode> CouponCode { get; set; }
        public DbSet<CouponCodeDetails> CouponCodeDetails { get; set; }
        public DbSet<Post> posts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=CSharp4_Assignment;Integrated Security=True;Connection Timeout=36000");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
