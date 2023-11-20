using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTag.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class employeeTokenAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_EmployeeLogOuts_Company_Employees_rlt_Employee_Id",
                table: "Company_EmployeeLogOuts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Company_EmployeeLogOuts",
                table: "Company_EmployeeLogOuts");

            migrationBuilder.RenameTable(
                name: "Company_EmployeeLogOuts",
                newName: "Company_EmployeeLogOutJobs");

            migrationBuilder.RenameIndex(
                name: "IX_Company_EmployeeLogOuts_rlt_Employee_Id",
                table: "Company_EmployeeLogOutJobs",
                newName: "IX_Company_EmployeeLogOutJobs_rlt_Employee_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Company_EmployeeLogOutJobs",
                table: "Company_EmployeeLogOutJobs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Company_EmployeeTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Token = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rlt_Employee_Id = table.Column<int>(type: "int", nullable: false),
                    RecordCreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company_EmployeeTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_EmployeeTokens_Company_Employees_rlt_Employee_Id",
                        column: x => x.rlt_Employee_Id,
                        principalTable: "Company_Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Company_EmployeeTokens_rlt_Employee_Id",
                table: "Company_EmployeeTokens",
                column: "rlt_Employee_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_EmployeeLogOutJobs_Company_Employees_rlt_Employee_Id",
                table: "Company_EmployeeLogOutJobs",
                column: "rlt_Employee_Id",
                principalTable: "Company_Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_EmployeeLogOutJobs_Company_Employees_rlt_Employee_Id",
                table: "Company_EmployeeLogOutJobs");

            migrationBuilder.DropTable(
                name: "Company_EmployeeTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Company_EmployeeLogOutJobs",
                table: "Company_EmployeeLogOutJobs");

            migrationBuilder.RenameTable(
                name: "Company_EmployeeLogOutJobs",
                newName: "Company_EmployeeLogOuts");

            migrationBuilder.RenameIndex(
                name: "IX_Company_EmployeeLogOutJobs_rlt_Employee_Id",
                table: "Company_EmployeeLogOuts",
                newName: "IX_Company_EmployeeLogOuts_rlt_Employee_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Company_EmployeeLogOuts",
                table: "Company_EmployeeLogOuts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_EmployeeLogOuts_Company_Employees_rlt_Employee_Id",
                table: "Company_EmployeeLogOuts",
                column: "rlt_Employee_Id",
                principalTable: "Company_Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
