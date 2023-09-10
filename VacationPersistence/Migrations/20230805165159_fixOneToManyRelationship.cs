using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacationPersistence.Migrations
{
    /// <inheritdoc />
    public partial class fixOneToManyRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacations_Rules_RuleID",
                table: "Vacations");

            migrationBuilder.DropTable(
                name: "VacationRules");

            migrationBuilder.DropIndex(
                name: "IX_Vacations_RuleID",
                table: "Vacations");

            migrationBuilder.DropColumn(
                name: "RuleID",
                table: "Vacations");

            migrationBuilder.AddColumn<int>(
                name: "VacationID",
                table: "Rules",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rules_VacationID",
                table: "Rules",
                column: "VacationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rules_Vacations_VacationID",
                table: "Rules",
                column: "VacationID",
                principalTable: "Vacations",
                principalColumn: "VacationID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rules_Vacations_VacationID",
                table: "Rules");

            migrationBuilder.DropIndex(
                name: "IX_Rules_VacationID",
                table: "Rules");

            migrationBuilder.DropColumn(
                name: "VacationID",
                table: "Rules");

            migrationBuilder.AddColumn<int>(
                name: "RuleID",
                table: "Vacations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "VacationRules",
                columns: table => new
                {
                    VacationID = table.Column<int>(type: "int", nullable: false),
                    RuleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationRules", x => new { x.VacationID, x.RuleID });
                    table.ForeignKey(
                        name: "FK_VacationRules_Rules_RuleID",
                        column: x => x.RuleID,
                        principalTable: "Rules",
                        principalColumn: "RuleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VacationRules_Vacations_VacationID",
                        column: x => x.VacationID,
                        principalTable: "Vacations",
                        principalColumn: "VacationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vacations_RuleID",
                table: "Vacations",
                column: "RuleID");

            migrationBuilder.CreateIndex(
                name: "IX_VacationRules_RuleID",
                table: "VacationRules",
                column: "RuleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacations_Rules_RuleID",
                table: "Vacations",
                column: "RuleID",
                principalTable: "Rules",
                principalColumn: "RuleID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
