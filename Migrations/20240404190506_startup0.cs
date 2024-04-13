using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace student_permit_system.PL.Migrations
{
    /// <inheritdoc />
    public partial class startup0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contact_AspNetUsers_EmpID",
                table: "Contact");

            migrationBuilder.DropForeignKey(
                name: "FK_Contact_AspNetUsers_Id",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_EmpID",
                table: "Contact");

            migrationBuilder.DropIndex(
                name: "IX_Contact_Id",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Contact");

            migrationBuilder.AlterColumn<string>(
                name: "EmpID",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentID",
                table: "Contact",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentID",
                table: "Contact");

            migrationBuilder.AlterColumn<string>(
                name: "EmpID",
                table: "Contact",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Contact",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Contact",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_EmpID",
                table: "Contact",
                column: "EmpID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_Id",
                table: "Contact",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_AspNetUsers_EmpID",
                table: "Contact",
                column: "EmpID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contact_AspNetUsers_Id",
                table: "Contact",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
