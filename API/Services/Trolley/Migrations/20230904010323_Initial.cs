using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trolley.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trolleys",
                columns: table => new
                {
                    TrolleyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trolleys", x => x.TrolleyId);
                });

            migrationBuilder.CreateTable(
                name: "TrolleyProducts",
                columns: table => new
                {
                    TrolleyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrolleyProducts", x => new { x.TrolleyId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_TrolleyProducts_Trolleys_TrolleyId",
                        column: x => x.TrolleyId,
                        principalTable: "Trolleys",
                        principalColumn: "TrolleyId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrolleyProducts");

            migrationBuilder.DropTable(
                name: "Trolleys");
        }
    }
}
