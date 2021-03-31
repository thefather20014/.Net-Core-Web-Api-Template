using Microsoft.EntityFrameworkCore.Migrations;

namespace codeFirst.Migrations
{
    public partial class Part2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Developer_Country_CountryId",
                table: "Developer");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "Developer",
                newName: "CountryID");

            migrationBuilder.RenameIndex(
                name: "IX_Developer_CountryId",
                table: "Developer",
                newName: "IX_Developer_CountryID");

            migrationBuilder.AlterColumn<int>(
                name: "CountryID",
                table: "Developer",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Developer_Country_CountryID",
                table: "Developer",
                column: "CountryID",
                principalTable: "Country",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Developer_Country_CountryID",
                table: "Developer");

            migrationBuilder.RenameColumn(
                name: "CountryID",
                table: "Developer",
                newName: "CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Developer_CountryID",
                table: "Developer",
                newName: "IX_Developer_CountryId");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Developer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Developer_Country_CountryId",
                table: "Developer",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
