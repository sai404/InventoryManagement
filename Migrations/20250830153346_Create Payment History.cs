using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.API.Migrations
{
    /// <inheritdoc />
    public partial class CreatePaymentHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MineItemRate_MineDetails_MineDetailsId",
                table: "MineItemRate");

            migrationBuilder.AlterColumn<Guid>(
                name: "MineDetailsId",
                table: "MineItemRate",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentHistory_MineDetails_MineId",
                        column: x => x.MineId,
                        principalTable: "MineDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentHistory_MineId",
                table: "PaymentHistory",
                column: "MineId");

            migrationBuilder.AddForeignKey(
                name: "FK_MineItemRate_MineDetails_MineDetailsId",
                table: "MineItemRate",
                column: "MineDetailsId",
                principalTable: "MineDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MineItemRate_MineDetails_MineDetailsId",
                table: "MineItemRate");

            migrationBuilder.DropTable(
                name: "PaymentHistory");

            migrationBuilder.AlterColumn<Guid>(
                name: "MineDetailsId",
                table: "MineItemRate",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_MineItemRate_MineDetails_MineDetailsId",
                table: "MineItemRate",
                column: "MineDetailsId",
                principalTable: "MineDetails",
                principalColumn: "Id");
        }
    }
}
