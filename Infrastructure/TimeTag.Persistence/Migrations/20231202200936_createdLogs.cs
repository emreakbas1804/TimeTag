using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTag.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class createdLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Company_EmployeeLoginJobs");

            migrationBuilder.DropTable(
                name: "Company_EmployeeLogOutJobs");

            migrationBuilder.CreateTable(
                name: "Company_EmployeeLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProcessTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IpAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Token = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rlt_Employee_Id = table.Column<int>(type: "int", nullable: false),
                    RecordCreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company_EmployeeLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_EmployeeLogs_Company_Employees_rlt_Employee_Id",
                        column: x => x.rlt_Employee_Id,
                        principalTable: "Company_Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Company_EmployeeLogs_rlt_Employee_Id",
                table: "Company_EmployeeLogs",
                column: "rlt_Employee_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Company_EmployeeLogs");

            migrationBuilder.CreateTable(
                name: "Company_EmployeeLoginJobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    rlt_Employee_Id = table.Column<int>(type: "int", nullable: false),
                    IpAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LoginTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RecordCreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Token = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company_EmployeeLoginJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_EmployeeLoginJobs_Company_Employees_rlt_Employee_Id",
                        column: x => x.rlt_Employee_Id,
                        principalTable: "Company_Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Company_EmployeeLogOutJobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    rlt_Employee_Id = table.Column<int>(type: "int", nullable: false),
                    IpAddress = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LogoutTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RecordCreateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Token = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company_EmployeeLogOutJobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_EmployeeLogOutJobs_Company_Employees_rlt_Employee_Id",
                        column: x => x.rlt_Employee_Id,
                        principalTable: "Company_Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Company_EmployeeLoginJobs_rlt_Employee_Id",
                table: "Company_EmployeeLoginJobs",
                column: "rlt_Employee_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Company_EmployeeLogOutJobs_rlt_Employee_Id",
                table: "Company_EmployeeLogOutJobs",
                column: "rlt_Employee_Id");
        }
    }
}
