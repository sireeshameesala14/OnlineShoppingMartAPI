using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.DBContext
{
    public class OSMDBContext : DbContext
    {  
        public OSMDBContext(DbContextOptions<OSMDBContext> options) : base(options)
        {

        }
        public  DbSet<LoginDetails> LoginDetails { get; set; } 
        public  DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ProductDetails> ProductDetails { get; set; }

        public DbSet<ProductImages> ProductImages { get; set; }

        public DbSet<ProductReview> ProductReview { get; set; }

        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<ProductOrderRel> ProductOrderRel { get; set; }
        public DbSet<TransactionDetails> TransactionDetails { get; set; }
        public DbSet<AddressDetail> AddressDetail { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductOrderRel>()
                  .HasKey(m => new { m.ProductId, m.OrderId });
        }


    }
}