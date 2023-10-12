using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitDb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemOrders_OrderOrders_OrderId",
                table: "ItemOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderOrders",
                table: "OrderOrders");

            migrationBuilder.RenameTable(
                name: "OrderOrders",
                newName: "Orders");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemOrders_Orders_OrderId",
                table: "ItemOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemOrders_Orders_OrderId",
                table: "ItemOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "OrderOrders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderOrders",
                table: "OrderOrders",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemOrders_OrderOrders_OrderId",
                table: "ItemOrders",
                column: "OrderId",
                principalTable: "OrderOrders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
