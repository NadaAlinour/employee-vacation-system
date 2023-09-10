using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacationPersistence.Migrations
{
    /// <inheritdoc />
    public partial class migrationTest8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeVacations",
                table: "EmployeeVacations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeVacations",
                table: "EmployeeVacations",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeVacations_EmployeeID",
                table: "EmployeeVacations",
                column: "EmployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeVacations",
                table: "EmployeeVacations");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeVacations_EmployeeID",
                table: "EmployeeVacations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeVacations",
                table: "EmployeeVacations",
                columns: new[] { "EmployeeID", "VacationID" });
        }
    }
}
