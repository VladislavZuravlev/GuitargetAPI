using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class addlinktable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RenovationWorkRepairRequest");

            migrationBuilder.CreateTable(
                name: "RenovationWorkRepairRequests",
                columns: table => new
                {
                    RepairRequestId = table.Column<int>(type: "integer", nullable: false),
                    RenovationWorkId = table.Column<int>(type: "integer", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RenovationWorkRepairRequests", x => new { x.RepairRequestId, x.RenovationWorkId });
                    table.ForeignKey(
                        name: "FK_RenovationWorkRepairRequests_RenovationWorks_RenovationWork~",
                        column: x => x.RenovationWorkId,
                        principalTable: "RenovationWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RenovationWorkRepairRequests_RepairRequests_RepairRequestId",
                        column: x => x.RepairRequestId,
                        principalTable: "RepairRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RenovationWorkRepairRequests_RenovationWorkId",
                table: "RenovationWorkRepairRequests",
                column: "RenovationWorkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RenovationWorkRepairRequests");

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
                        name: "FK_RenovationWorkRepairRequest_RepairRequests_RepairsId",
                        column: x => x.RepairsId,
                        principalTable: "RepairRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RenovationWorkRepairRequest_RepairsId",
                table: "RenovationWorkRepairRequest",
                column: "RepairsId");
        }
    }
}
