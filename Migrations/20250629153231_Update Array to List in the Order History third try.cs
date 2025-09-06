using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateArraytoListintheOrderHistorythirdtry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderHistory_OrderHistoryId",
                table: "Order");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderHistoryId",
                table: "Order",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderHistory_OrderHistoryId",
                table: "Order",
                column: "OrderHistoryId",
                principalTable: "OrderHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderHistory_OrderHistoryId",
                table: "Order");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderHistoryId",
                table: "Order",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderHistory_OrderHistoryId",
                table: "Order",
                column: "OrderHistoryId",
                principalTable: "OrderHistory",
                principalColumn: "Id");
        }
    }
}
