using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTag.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TitleToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_Employees_FileUploads_rlt_FileUpload_Id",
                table: "Company_Employees");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Companies");

            migrationBuilder.RenameColumn(
                name: "LoginTag",
                table: "Company_Employees",
                newName: "Title");

            migrationBuilder.AlterColumn<int>(
                name: "rlt_FileUpload_Id",
                table: "Company_Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "Company_Employees",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Employees_FileUploads_rlt_FileUpload_Id",
                table: "Company_Employees",
                column: "rlt_FileUpload_Id",
                principalTable: "FileUploads",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_Employees_FileUploads_rlt_FileUpload_Id",
                table: "Company_Employees");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Company_Employees",
                newName: "LoginTag");

            migrationBuilder.AlterColumn<int>(
                name: "rlt_FileUpload_Id",
                table: "Company_Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IsActive",
                table: "Company_Employees",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Employees_FileUploads_rlt_FileUpload_Id",
                table: "Company_Employees",
                column: "rlt_FileUpload_Id",
                principalTable: "FileUploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
