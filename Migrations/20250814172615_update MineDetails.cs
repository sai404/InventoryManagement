using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.API.Migrations
{
    /// <inheritdoc />
    public partial class updateMineDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MineItemRate",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PricePerUnit = table.Column<int>(type: "int", nullable: false),
                    MineDetailsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MineItemRate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MineItemRate_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MineItemRate_MineDetails_MineDetailsId",
                        column: x => x.MineDetailsId,
                        principalTable: "MineDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MineItemRate_ItemId",
                table: "MineItemRate",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MineItemRate_MineDetailsId",
                table: "MineItemRate",
                column: "MineDetailsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MineItemRate");
        }
    }
}
