using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SlippAPI.Migrations
{
    public partial class AddingOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_AppUsers_BuyerId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Sales_SaleId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Sales_BuyerId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "IsPayed",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "PaymentDeadline",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "SaleId",
                table: "Tickets",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_SaleId",
                table: "Tickets",
                newName: "IX_Tickets_OrderId");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Sales",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IssuedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSaved = table.Column<bool>(type: "bit", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_AppUserId",
                table: "Sales",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_OrderId",
                table: "Sales",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AppUserId",
                table: "Orders",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_AppUsers_AppUserId",
                table: "Sales",
                column: "AppUserId",
                principalTable: "AppUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Orders_OrderId",
                table: "Sales",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Orders_OrderId",
                table: "Tickets",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_AppUsers_AppUserId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Orders_OrderId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Orders_OrderId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Sales_AppUserId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_OrderId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Tickets",
                newName: "SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_OrderId",
                table: "Tickets",
                newName: "IX_Tickets_SaleId");

            migrationBuilder.AddColumn<string>(
                name: "BuyerId",
                table: "Sales",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsPayed",
                table: "Sales",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDeadline",
                table: "Sales",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Sales_BuyerId",
                table: "Sales",
                column: "BuyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_AppUsers_BuyerId",
                table: "Sales",
                column: "BuyerId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Sales_SaleId",
                table: "Tickets",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id");
        }
    }
}
