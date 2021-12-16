using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TradeUp.Domain.Migrations
{
    public partial class AddResourceContributionHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResourceContributionHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ResourceContributorId = table.Column<int>(type: "int", nullable: false),
                    ResourceCount = table.Column<long>(type: "bigint", nullable: false),
                    ResourcePriceAtTimeOfContribution = table.Column<double>(type: "double", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceContributionHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceContributionHistory_ResourceContributors_ResourceCon~",
                        column: x => x.ResourceContributorId,
                        principalTable: "ResourceContributors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContributionHistory_ResourceContributorId",
                table: "ResourceContributionHistory",
                column: "ResourceContributorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResourceContributionHistory");
        }
    }
}
