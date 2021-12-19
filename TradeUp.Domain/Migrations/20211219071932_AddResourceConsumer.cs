using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TradeUp.Domain.Migrations
{
    public partial class AddResourceConsumer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consumers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConsumptionFactor = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ResourceConsumers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ConsumerId = table.Column<int>(type: "int", nullable: false),
                    ResourceId = table.Column<int>(type: "int", nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    MinConsumptionRange = table.Column<int>(type: "int", nullable: false),
                    MaxConsumptionRange = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceConsumers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceConsumers_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalTable: "Consumers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResourceConsumers_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ResourceConsumptionHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ResourceConsumerId = table.Column<int>(type: "int", nullable: false),
                    ResourceCountAfterConsumption = table.Column<long>(type: "bigint", nullable: false),
                    ResourcesConsumed = table.Column<long>(type: "bigint", nullable: false),
                    ResourcePriceAtTimeOfConsumption = table.Column<double>(type: "double", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceConsumptionHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResourceConsumptionHistory_ResourceConsumers_ResourceConsume~",
                        column: x => x.ResourceConsumerId,
                        principalTable: "ResourceConsumers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceConsumers_ConsumerId",
                table: "ResourceConsumers",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceConsumers_ResourceId",
                table: "ResourceConsumers",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceConsumptionHistory_ResourceConsumerId",
                table: "ResourceConsumptionHistory",
                column: "ResourceConsumerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResourceConsumptionHistory");

            migrationBuilder.DropTable(
                name: "ResourceConsumers");

            migrationBuilder.DropTable(
                name: "Consumers");
        }
    }
}
