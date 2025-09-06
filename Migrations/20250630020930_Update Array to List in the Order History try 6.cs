using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateArraytoListintheOrderHistorytry6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Item_ItemId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderHistory_OrderHistoryId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistory_MineDetails_MineDetailsId",
                table: "OrderHistory");

            migrationBuilder.DropIndex(
                name: "IX_OrderHistory_MineDetailsId",
                table: "OrderHistory");

            migrationBuilder.DropIndex(
                name: "IX_Order_ItemId",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "MineDetailsId",
                table: "OrderHistory",
                newName: "MineId");

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

            migrationBuilder.RenameColumn(
                name: "MineId",
                table: "OrderHistory",
                newName: "MineDetailsId");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderHistoryId",
                table: "Order",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_MineDetailsId",
                table: "OrderHistory",
                column: "MineDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ItemId",
                table: "Order",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Item_ItemId",
                table: "Order",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderHistory_OrderHistoryId",
                table: "Order",
                column: "OrderHistoryId",
                principalTable: "OrderHistory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistory_MineDetails_MineDetailsId",
                table: "OrderHistory",
                column: "MineDetailsId",
                principalTable: "MineDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
