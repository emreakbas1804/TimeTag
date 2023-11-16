using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTag.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class companyFileNullableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_FileUploads_rlt_FileUpload_Id",
                table: "Companies");

            migrationBuilder.AlterColumn<int>(
                name: "rlt_FileUpload_Id",
                table: "Companies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_FileUploads_rlt_FileUpload_Id",
                table: "Companies",
                column: "rlt_FileUpload_Id",
                principalTable: "FileUploads",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_FileUploads_rlt_FileUpload_Id",
                table: "Companies");

            migrationBuilder.AlterColumn<int>(
                name: "rlt_FileUpload_Id",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_FileUploads_rlt_FileUpload_Id",
                table: "Companies",
                column: "rlt_FileUpload_Id",
                principalTable: "FileUploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
