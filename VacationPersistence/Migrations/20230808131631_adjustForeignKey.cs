using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacationPersistence.Migrations
{
    /// <inheritdoc />
    public partial class adjustForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeVacations_Employees_EmployeeID",
                table: "EmployeeVacations");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeVacations_Vacations_VacationID",
                table: "EmployeeVacations");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeVacations_EmployeeID",
                table: "EmployeeVacations");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeVacations_VacationID",
                table: "EmployeeVacations");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeVacations_EmployeeID",
                table: "EmployeeVacations",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vacations_EmployeeVacations_VacationID",
                table: "EmployeeVacations",
                column: "VacationID",
                principalTable: "Vacations",
                principalColumn: "VacationID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EmployeeVacations_EmployeeID",
                table: "EmployeeVacations",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeVacations_VacationID",
                table: "EmployeeVacations",
                column: "VacationID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeVacations_Employees_EmployeeID",
                table: "EmployeeVacations",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeVacations_Vacations_VacationID",
                table: "EmployeeVacations",
                column: "VacationID",
                principalTable: "Vacations",
                principalColumn: "VacationID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
