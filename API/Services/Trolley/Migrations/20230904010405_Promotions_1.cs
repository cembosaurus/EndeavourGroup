using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trolley.Migrations
{
    /// <inheritdoc />
    public partial class Promotions_1 : Migration
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
                    PriceLevel = table.Column<int>(type: "int", nullable: false),
                    DiscountPercent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrolleyPromotion", x => x.TrolleyPromotionTypeId);
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrolleyPromotionType");

            migrationBuilder.DropTable(
                name: "TrolleyPromotion");
        }
    }
}
