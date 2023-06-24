using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserListTestApp.Migrations
{
    /// <inheritdoc />
    public partial class CorrectSpellings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Last_visti_date",
                table: "Users",
                newName: "Last_visit_date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Last_visit_date",
                table: "Users",
                newName: "Last_visti_date");
        }
    }
}
