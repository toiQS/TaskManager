﻿using Entity;
using ENTITY;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ImportBill> ImportBills { get; set; }
        public DbSet<ItemOrder> ItemOrders { get; set; }
        public DbSet<Order> OrderOrders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImport> ProductImports { get; set; }
        public DbSet<ProductWarehouse> ProductWarehouse { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Warehouse> Warehouse { get; set; }
    }
}