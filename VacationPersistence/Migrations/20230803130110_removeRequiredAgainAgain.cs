using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacationPersistence.Migrations
{
    /// <inheritdoc />
    public partial class removeRequiredAgainAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rules_Hires_HireID",
                table: "Rules");

            migrationBuilder.AlterColumn<int>(
                name: "HireID",
                table: "Rules",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Rules_Hires_HireID",
                table: "Rules",
                column: "HireID",
                principalTable: "Hires",
                principalColumn: "HireID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rules_Hires_HireID",
                table: "Rules");

            migrationBuilder.AlterColumn<int>(
                name: "HireID",
                table: "Rules",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rules_Hires_HireID",
                table: "Rules",
                column: "HireID",
                principalTable: "Hires",
                principalColumn: "HireID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
