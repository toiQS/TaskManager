﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231012140758_InitDb1")]
    partial class InitDb1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ENTITY.Brand", b =>
                {
                    b.Property<string>("BrandId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BrandInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrandName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BrandId");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("ENTITY.Category", b =>
                {
                    b.Property<string>("CategoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ENTITY.Image", b =>
                {
                    b.Property<string>("ImageId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ImageId");

                    b.HasIndex("ProductId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("ENTITY.ImportBill", b =>
                {
                    b.Property<string>("ImportBillId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("SupplierId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("WarehouseId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ImportBillId");

                    b.HasIndex("SupplierId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("ImportBills");
                });

            modelBuilder.Entity("ENTITY.ItemOrder", b =>
                {
                    b.Property<long>("ItemOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ItemOrderId"));

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<double>("SellPrice")
                        .HasColumnType("float");

                    b.HasKey("ItemOrderId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("ItemOrders");
                });

            modelBuilder.Entity("ENTITY.Order", b =>
                {
                    b.Property<string>("OrderId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime2");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ENTITY.Product", b =>
                {
                    b.Property<string>("ProductId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BrandId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProductInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ENTITY.ProductWarehouse", b =>
                {
                    b.Property<long>("ProductWarehouseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ProductWarehouseId"));

                    b.Property<double>("ImportPriceOfEachProduct")
                        .HasColumnType("float");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("WarehouseId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ProductWarehouseId");

                    b.HasIndex("ProductId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("ProductWarehouse");
                });

            modelBuilder.Entity("ENTITY.Supplier", b =>
                {
                    b.Property<string>("SupplierId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SupplierAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplierPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SupplierId");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("ENTITY.Warehouse", b =>
                {
                    b.Property<string>("WarehouseId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("WarehouseAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WarehouseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WarehouseId");

                    b.ToTable("Warehouse");
                });

            modelBuilder.Entity("Entity.ProductImport", b =>
                {
                    b.Property<long>("ProductImportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ProductImportId"));

                    b.Property<string>("ImportBillId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("PriceOfEachProduct")
                        .HasColumnType("float");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("ProductImportId");

                    b.HasIndex("ImportBillId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImports");
                });

            modelBuilder.Entity("ENTITY.Image", b =>
                {
                    b.HasOne("ENTITY.Product", null)
                        .WithMany("ProductImage")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ENTITY.ImportBill", b =>
                {
                    b.HasOne("ENTITY.Supplier", null)
                        .WithMany("BillList")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ENTITY.Warehouse", null)
                        .WithMany("ImportBillItems")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ENTITY.ItemOrder", b =>
                {
                    b.HasOne("ENTITY.Order", null)
                        .WithMany("ItemOrders")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ENTITY.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ENTITY.Product", b =>
                {
                    b.HasOne("ENTITY.Brand", null)
                        .WithMany("Products")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ENTITY.Category", null)
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ENTITY.ProductWarehouse", b =>
                {
                    b.HasOne("ENTITY.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ENTITY.Warehouse", null)
                        .WithMany("WarehouseItems")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Entity.ProductImport", b =>
                {
                    b.HasOne("ENTITY.ImportBill", null)
                        .WithMany("ListProduct")
                        .HasForeignKey("ImportBillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ENTITY.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ENTITY.Brand", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ENTITY.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ENTITY.ImportBill", b =>
                {
                    b.Navigation("ListProduct");
                });

            modelBuilder.Entity("ENTITY.Order", b =>
                {
                    b.Navigation("ItemOrders");
                });

            modelBuilder.Entity("ENTITY.Product", b =>
                {
                    b.Navigation("ProductImage");
                });

            modelBuilder.Entity("ENTITY.Supplier", b =>
                {
                    b.Navigation("BillList");
                });

            modelBuilder.Entity("ENTITY.Warehouse", b =>
                {
                    b.Navigation("ImportBillItems");

                    b.Navigation("WarehouseItems");
                });
#pragma warning restore 612, 618
        }
    }
}
