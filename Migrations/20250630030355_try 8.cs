using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.API.Migrations
{
    /// <inheritdoc />
    public partial class try8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_MineId",
                table: "OrderHistory",
                column: "MineId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistory_MineDetails_MineId",
                table: "OrderHistory",
                column: "MineId",
                principalTable: "MineDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistory_MineDetails_MineId",
                table: "OrderHistory");

            migrationBuilder.DropIndex(
                name: "IX_OrderHistory_MineId",
                table: "OrderHistory");
        }
    }
}
