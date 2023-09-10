using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacationPersistence.Migrations
{
    /// <inheritdoc />
    public partial class adjustForeignKeysForRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rules_Units_UnitID",
                table: "Rules");

            migrationBuilder.DropForeignKey(
                name: "FK_Rules_Vacations_VacationID",
                table: "Rules");

            migrationBuilder.DropIndex(
                name: "IX_Rules_UnitID",
                table: "Rules");

            migrationBuilder.DropIndex(
                name: "IX_Rules_VacationID",
                table: "Rules");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacations_Rules_VacationID",
                table: "Rules",
                column: "VacationID",
                principalTable: "Vacations",
                principalColumn: "VacationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Rules_UnitID",
                table: "Rules",
                column: "UnitID",
                principalTable: "Units",
                principalColumn: "UnitID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Rules_UnitID",
                table: "Rules",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_Rules_VacationID",
                table: "Rules",
                column: "VacationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rules_Units_UnitID",
                table: "Rules",
                column: "UnitID",
                principalTable: "Units",
                principalColumn: "UnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rules_Vacations_VacationID",
                table: "Rules",
                column: "VacationID",
                principalTable: "Vacations",
                principalColumn: "VacationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
