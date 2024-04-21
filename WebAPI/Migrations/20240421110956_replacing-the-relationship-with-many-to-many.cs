using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class replacingtherelationshipwithmanytomany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_RenovationWorks_RenovationWorkId",
                table: "Repairs");

            migrationBuilder.DropIndex(
                name: "IX_Repairs_RenovationWorkId",
                table: "Repairs");

            migrationBuilder.CreateTable(
                name: "RenovationWorkRepairRequest",
                columns: table => new
                {
                    RenovationWorksId = table.Column<int>(type: "integer", nullable: false),
                    RepairsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenovationWorkRepairRequest", x => new { x.RenovationWorksId, x.RepairsId });
                    table.ForeignKey(
                        name: "FK_RenovationWorkRepairRequest_RenovationWorks_RenovationWorks~",
                        column: x => x.RenovationWorksId,
                        principalTable: "RenovationWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RenovationWorkRepairRequest_Repairs_RepairsId",
                        column: x => x.RepairsId,
                        principalTable: "Repairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RenovationWorkRepairRequest_RepairsId",
                table: "RenovationWorkRepairRequest",
                column: "RepairsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RenovationWorkRepairRequest");

            migrationBuilder.CreateIndex(
                name: "IX_Repairs_RenovationWorkId",
                table: "Repairs",
                column: "RenovationWorkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_RenovationWorks_RenovationWorkId",
                table: "Repairs",
                column: "RenovationWorkId",
                principalTable: "RenovationWorks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
