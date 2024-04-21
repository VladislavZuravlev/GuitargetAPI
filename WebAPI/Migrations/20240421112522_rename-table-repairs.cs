using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class renametablerepairs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RenovationWorkRepairRequest_Repairs_RepairsId",
                table: "RenovationWorkRepairRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_Clients_ClientId",
                table: "Repairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_Employees_EmployeeId",
                table: "Repairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_Masters_MasterId",
                table: "Repairs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Repairs",
                table: "Repairs");

            migrationBuilder.RenameTable(
                name: "Repairs",
                newName: "RepairRequests");

            migrationBuilder.RenameIndex(
                name: "IX_Repairs_MasterId",
                table: "RepairRequests",
                newName: "IX_RepairRequests_MasterId");

            migrationBuilder.RenameIndex(
                name: "IX_Repairs_EmployeeId",
                table: "RepairRequests",
                newName: "IX_RepairRequests_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Repairs_ClientId",
                table: "RepairRequests",
                newName: "IX_RepairRequests_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RepairRequests",
                table: "RepairRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RenovationWorkRepairRequest_RepairRequests_RepairsId",
                table: "RenovationWorkRepairRequest",
                column: "RepairsId",
                principalTable: "RepairRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairRequests_Clients_ClientId",
                table: "RepairRequests",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairRequests_Employees_EmployeeId",
                table: "RepairRequests",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RepairRequests_Masters_MasterId",
                table: "RepairRequests",
                column: "MasterId",
                principalTable: "Masters",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RenovationWorkRepairRequest_RepairRequests_RepairsId",
                table: "RenovationWorkRepairRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairRequests_Clients_ClientId",
                table: "RepairRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairRequests_Employees_EmployeeId",
                table: "RepairRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairRequests_Masters_MasterId",
                table: "RepairRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RepairRequests",
                table: "RepairRequests");

            migrationBuilder.RenameTable(
                name: "RepairRequests",
                newName: "Repairs");

            migrationBuilder.RenameIndex(
                name: "IX_RepairRequests_MasterId",
                table: "Repairs",
                newName: "IX_Repairs_MasterId");

            migrationBuilder.RenameIndex(
                name: "IX_RepairRequests_EmployeeId",
                table: "Repairs",
                newName: "IX_Repairs_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_RepairRequests_ClientId",
                table: "Repairs",
                newName: "IX_Repairs_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Repairs",
                table: "Repairs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RenovationWorkRepairRequest_Repairs_RepairsId",
                table: "RenovationWorkRepairRequest",
                column: "RepairsId",
                principalTable: "Repairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_Clients_ClientId",
                table: "Repairs",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_Employees_EmployeeId",
                table: "Repairs",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_Masters_MasterId",
                table: "Repairs",
                column: "MasterId",
                principalTable: "Masters",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
