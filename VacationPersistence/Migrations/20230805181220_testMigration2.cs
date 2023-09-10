using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacationPersistence.Migrations
{
    /// <inheritdoc />
    public partial class testMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RuleID",
                table: "Units",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RuleID",
                table: "Hires",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Units_RuleID",
                table: "Units",
                column: "RuleID");

            migrationBuilder.CreateIndex(
                name: "IX_Hires_RuleID",
                table: "Hires",
                column: "RuleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Hires_Rules_RuleID",
                table: "Hires",
                column: "RuleID",
                principalTable: "Rules",
                principalColumn: "RuleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Rules_RuleID",
                table: "Units",
                column: "RuleID",
                principalTable: "Rules",
                principalColumn: "RuleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hires_Rules_RuleID",
                table: "Hires");

            migrationBuilder.DropForeignKey(
                name: "FK_Units_Rules_RuleID",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_RuleID",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Hires_RuleID",
                table: "Hires");

            migrationBuilder.DropColumn(
                name: "RuleID",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "RuleID",
                table: "Hires");
        }
    }
}
