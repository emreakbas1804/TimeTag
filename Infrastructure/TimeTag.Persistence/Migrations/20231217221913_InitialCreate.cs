using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTag.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppDomains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppDomains", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileUploads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileUploads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LangCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Licances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdded = table.Column<bool>(type: "bit", nullable: false),
                    RecordCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSystemRole = table.Column<bool>(type: "bit", nullable: false),
                    RecordCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Localizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rlt_Language_Id = table.Column<int>(type: "int", nullable: false),
                    RecordCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Localizations_Languages_rlt_Language_Id",
                        column: x => x.rlt_Language_Id,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirm = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFirstLogin = table.Column<bool>(type: "bit", nullable: false),
                    rlt_Role_Id = table.Column<int>(type: "int", nullable: false),
                    RecordCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_rlt_Role_Id",
                        column: x => x.rlt_Role_Id,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebSite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rlt_User_Id = table.Column<int>(type: "int", nullable: false),
                    rlt_FileUpload_Id = table.Column<int>(type: "int", nullable: true),
                    rlt_Licance_Id = table.Column<int>(type: "int", nullable: false),
                    RecordCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_FileUploads_rlt_FileUpload_Id",
                        column: x => x.rlt_FileUpload_Id,
                        principalTable: "FileUploads",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Companies_Licances_rlt_Licance_Id",
                        column: x => x.rlt_Licance_Id,
                        principalTable: "Licances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Companies_Users_rlt_User_Id",
                        column: x => x.rlt_User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecurityCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    rlt_User_Id = table.Column<int>(type: "int", nullable: true),
                    RecordCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecurityCodes_Users_rlt_User_Id",
                        column: x => x.rlt_User_Id,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User_LoginLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSuccessLogin = table.Column<bool>(type: "bit", nullable: false),
                    ReferanceUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rlt_User_Id = table.Column<int>(type: "int", nullable: false),
                    RecordCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_LoginLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_LoginLogs_Users_rlt_User_Id",
                        column: x => x.rlt_User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_Tokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    rlt_User_Id = table.Column<int>(type: "int", nullable: false),
                    RecordCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Tokens_Users_rlt_User_Id",
                        column: x => x.rlt_User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Company_Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartJobTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinishJobTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rlt_Company_Id = table.Column<int>(type: "int", nullable: false),
                    RecordCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_Departments_Companies_rlt_Company_Id",
                        column: x => x.rlt_Company_Id,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Department_Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameSurname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartedJobTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    rlt_FileUpload_Id = table.Column<int>(type: "int", nullable: true),
                    rlt_Department_Id = table.Column<int>(type: "int", nullable: false),
                    rlt_User_Id = table.Column<int>(type: "int", nullable: true),
                    RecordCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Department_Employees_Company_Departments_rlt_Department_Id",
                        column: x => x.rlt_Department_Id,
                        principalTable: "Company_Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Department_Employees_FileUploads_rlt_FileUpload_Id",
                        column: x => x.rlt_FileUpload_Id,
                        principalTable: "FileUploads",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Department_Employees_Users_rlt_User_Id",
                        column: x => x.rlt_User_Id,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Employee_Banks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rlt_Employee_Id = table.Column<int>(type: "int", nullable: false),
                    RecordCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee_Banks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Banks_Department_Employees_rlt_Employee_Id",
                        column: x => x.rlt_Employee_Id,
                        principalTable: "Department_Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee_Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rlt_Employee_Id = table.Column<int>(type: "int", nullable: false),
                    RecordCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Logs_Department_Employees_rlt_Employee_Id",
                        column: x => x.rlt_Employee_Id,
                        principalTable: "Department_Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee_Tokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rlt_Employee_Id = table.Column<int>(type: "int", nullable: true),
                    rlt_Licance_Id = table.Column<int>(type: "int", nullable: false),
                    RecordCreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee_Tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Tokens_Department_Employees_rlt_Employee_Id",
                        column: x => x.rlt_Employee_Id,
                        principalTable: "Department_Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employee_Tokens_Licances_rlt_Licance_Id",
                        column: x => x.rlt_Licance_Id,
                        principalTable: "Licances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_rlt_FileUpload_Id",
                table: "Companies",
                column: "rlt_FileUpload_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_rlt_Licance_Id",
                table: "Companies",
                column: "rlt_Licance_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_rlt_User_Id",
                table: "Companies",
                column: "rlt_User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Departments_rlt_Company_Id",
                table: "Company_Departments",
                column: "rlt_Company_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Department_Employees_rlt_Department_Id",
                table: "Department_Employees",
                column: "rlt_Department_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Department_Employees_rlt_FileUpload_Id",
                table: "Department_Employees",
                column: "rlt_FileUpload_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Department_Employees_rlt_User_Id",
                table: "Department_Employees",
                column: "rlt_User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Banks_rlt_Employee_Id",
                table: "Employee_Banks",
                column: "rlt_Employee_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Logs_rlt_Employee_Id",
                table: "Employee_Logs",
                column: "rlt_Employee_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Tokens_rlt_Employee_Id",
                table: "Employee_Tokens",
                column: "rlt_Employee_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Tokens_rlt_Licance_Id",
                table: "Employee_Tokens",
                column: "rlt_Licance_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Localizations_rlt_Language_Id",
                table: "Localizations",
                column: "rlt_Language_Id");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityCodes_rlt_User_Id",
                table: "SecurityCodes",
                column: "rlt_User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_LoginLogs_rlt_User_Id",
                table: "User_LoginLogs",
                column: "rlt_User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Tokens_rlt_User_Id",
                table: "User_Tokens",
                column: "rlt_User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_rlt_Role_Id",
                table: "Users",
                column: "rlt_Role_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppDomains");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Employee_Banks");

            migrationBuilder.DropTable(
                name: "Employee_Logs");

            migrationBuilder.DropTable(
                name: "Employee_Tokens");

            migrationBuilder.DropTable(
                name: "Localizations");

            migrationBuilder.DropTable(
                name: "SecurityCodes");

            migrationBuilder.DropTable(
                name: "User_LoginLogs");

            migrationBuilder.DropTable(
                name: "User_Tokens");

            migrationBuilder.DropTable(
                name: "Department_Employees");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Company_Departments");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "FileUploads");

            migrationBuilder.DropTable(
                name: "Licances");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
