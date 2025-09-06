using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.API.Migrations
{
    /// <inheritdoc />
    public partial class updateOrdersCS4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MineItemRate_ItemId",
                table: "MineItemRate",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MineItemRate_Item_ItemId",
                table: "MineItemRate",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MineItemRate_Item_ItemId",
                table: "MineItemRate");

            migrationBuilder.DropIndex(
                name: "IX_MineItemRate_ItemId",
                table: "MineItemRate");
        }
    }
}
