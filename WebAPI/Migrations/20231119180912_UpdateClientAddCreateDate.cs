using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClientAddCreateDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeMaster_Employees_EmployeeId",
                table: "EmployeeMaster");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_EmployeeMaster_MasterId",
                table: "Repairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_RenovationWork_RenovationWorkId",
                table: "Repairs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RenovationWork",
                table: "RenovationWork");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeMaster",
                table: "EmployeeMaster");

            migrationBuilder.RenameTable(
                name: "RenovationWork",
                newName: "RenovationWorks");

            migrationBuilder.RenameTable(
                name: "EmployeeMaster",
                newName: "Masters");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDateTime",
                table: "Clients",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RenovationWorks",
                table: "RenovationWorks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Masters",
                table: "Masters",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Masters_Employees_EmployeeId",
                table: "Masters",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_RenovationWorks_RenovationWorkId",
                table: "Repairs",
                column: "RenovationWorkId",
                principalTable: "RenovationWorks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Masters_Employees_EmployeeId",
                table: "Masters");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_Masters_MasterId",
                table: "Repairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_RenovationWorks_RenovationWorkId",
                table: "Repairs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RenovationWorks",
                table: "RenovationWorks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Masters",
                table: "Masters");

            migrationBuilder.DropColumn(
                name: "CreateDateTime",
                table: "Clients");

            migrationBuilder.RenameTable(
                name: "RenovationWorks",
                newName: "RenovationWork");

            migrationBuilder.RenameTable(
                name: "Masters",
                newName: "EmployeeMaster");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RenovationWork",
                table: "RenovationWork",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeMaster",
                table: "EmployeeMaster",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeMaster_Employees_EmployeeId",
                table: "EmployeeMaster",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_EmployeeMaster_MasterId",
                table: "Repairs",
                column: "MasterId",
                principalTable: "EmployeeMaster",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_RenovationWork_RenovationWorkId",
                table: "Repairs",
                column: "RenovationWorkId",
                principalTable: "RenovationWork",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
