using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacationPersistence.Migrations
{
    /// <inheritdoc />
    public partial class testmigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "unitTest",
                table: "Units",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "unitTest",
                table: "Units");
        }
    }
}
