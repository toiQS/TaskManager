using Entity;
using ENTITY;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Data
{
    //public class ApplicationDbContext : DbContext
    //{
    //    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    //    public DbSet<Brand> Brands { get; set; }
    //    public DbSet<Category> Categories { get; set; }
    //    public DbSet<Image> Images { get; set; }
    //    public DbSet<ImportBill> ImportBills { get; set; }
    //    public DbSet<ItemOrder> ItemOrders { get; set; }
    //    public DbSet<Order> Orders { get; set; }
    //    public DbSet<Product> Products { get; set; }
    //    public DbSet<ProductImport> ProductImports { get; set; }
    //    public DbSet<ProductWarehouse> ProductWarehouse { get; set; }
    //    public DbSet<Supplier> Suppliers { get; set; }
    //    public DbSet<Warehouse> Warehouse { get; set; }
    //}

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ImportBill> ImportBills { get; set; }
        public DbSet<ItemOrder> ItemOrders { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImport> ProductImports { get; set; }
        public DbSet<ProductWarehouse> ProductWarehouse { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUserRole<string>>().HasKey(x => new { x.UserId, x.RoleId });

            builder.Entity<IdentityRole>().HasData(
           new IdentityRole
           {
               Id = "1",
               Name = "Admin",
               NormalizedName = "ADMIN"
           },
        new IdentityRole
        {
            Id = "2",
            Name = "User",
            NormalizedName = "USER"
        }
            );
            var hasher = new PasswordHasher<IdentityUser>();
            builder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "1",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    PasswordHash = hasher.HashPassword(null, "Admin@123")
                },
                new IdentityUser
                {
                    Id = "2",
                    UserName = "user",
                    NormalizedUserName = "USER",
                    Email = "user@example.com",
                    NormalizedEmail = "USER@EXAMPLE.COM",
                    PasswordHash = hasher.HashPassword(null, "User@123")
                }
            );

            builder.Entity<IdentityUserRole<string>>().HasData(

                new IdentityUserRole<string>
                {
                    UserId = "1",
                    RoleId = "1"
                },
                new IdentityUserRole<string>
                {
                    UserId = "2",
                    RoleId = "2"
                }


            );
        }
    }
    
}   
