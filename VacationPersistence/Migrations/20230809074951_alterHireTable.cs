using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacationPersistence.Migrations
{
    /// <inheritdoc />
    public partial class alterHireTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rules_Hires_HireID",
                table: "Rules");

            migrationBuilder.DropIndex(
                name: "IX_Rules_HireID",
                table: "Rules");

            migrationBuilder.DropColumn(
                name: "HireID",
                table: "Rules");

            migrationBuilder.AddColumn<bool>(
                name: "IsBasedOnHire",
                table: "Vacations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "VacationID",
                table: "Hires",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacations_Hires_VacationID",
                table: "Hires",
                column: "VacationID",
                principalTable: "Vacations",
                principalColumn: "VacationID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBasedOnHire",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "VacationID",
                table: "Hires");

            migrationBuilder.AddColumn<int>(
                name: "HireID",
                table: "Rules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rules_HireID",
                table: "Rules",
                column: "HireID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rules_Hires_HireID",
                table: "Rules",
                column: "HireID",
                principalTable: "Hires",
                principalColumn: "HireID");
        }
    }
}
