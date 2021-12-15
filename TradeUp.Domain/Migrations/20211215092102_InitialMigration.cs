using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TradeUp.Domain.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Contributors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContributionFactor = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contributors", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CountAvailable = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ResourceContributors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ContributorId = table.Column<int>(type: "int", nullable: false),
                    ResourceId = table.Column<int>(type: "int", nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    MinContributionRange = table.Column<int>(type: "int", nullable: false),
                    MaxContributionRange = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceContributors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceContributors_Contributors_ContributorId",
                        column: x => x.ContributorId,
                        principalTable: "Contributors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceContributors_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Contributors",
                columns: new[] { "Id", "ContributionFactor", "Name", "Size" },
                values: new object[,]
                {
                    { 1, 1, "London", 100 },
                    { 2, 1, "Manchester", 50 }
                });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "CountAvailable", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 100L, "Iron", 10.5 },
                    { 2, 50L, "Copper", 4.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContributors_ContributorId",
                table: "ResourceContributors",
                column: "ContributorId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceContributors_ResourceId",
                table: "ResourceContributors",
                column: "ResourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResourceContributors");

            migrationBuilder.DropTable(
                name: "Contributors");

            migrationBuilder.DropTable(
                name: "Resources");
        }
    }
}
