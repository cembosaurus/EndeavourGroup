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
                name: "TrolleyPromotion",
                columns: table => new
                {
                    TrolleyPromotionTypeId = table.Column<int>(type: "int", nullable: false),
                    IsOn = table.Column<bool>(type: "bit", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SpendLevel = table.Column<int>(type: "int", nullable: false),
                    DiscountPercent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrolleyPromotion", x => x.TrolleyPromotionTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Trolleys",
                columns: table => new
                {
                    TrolleyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trolleys", x => x.TrolleyId);
                });

            migrationBuilder.CreateTable(
                name: "TrolleyPromotionType",
                columns: table => new
                {
                    TrolleyPromotionTypeId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrolleyPromotionType", x => x.TrolleyPromotionTypeId);
                    table.ForeignKey(
                        name: "FK_TrolleyPromotionType_TrolleyPromotion_TrolleyPromotionTypeId",
                        column: x => x.TrolleyPromotionTypeId,
                        principalTable: "TrolleyPromotion",
                        principalColumn: "TrolleyPromotionTypeId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "TrolleyPromotionType");

            migrationBuilder.DropTable(
                name: "Trolleys");

            migrationBuilder.DropTable(
                name: "TrolleyPromotion");
        }
    }
}
