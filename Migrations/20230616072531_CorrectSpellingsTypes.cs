using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserListTestApp.Migrations
{
    /// <inheritdoc />
    public partial class CorrectSpellingsTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserTypes_Type_idId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Type_idId",
                table: "Users",
                newName: "TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Type_idId",
                table: "Users",
                newName: "IX_Users_TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserTypes_TypeId",
                table: "Users",
                column: "TypeId",
                principalTable: "UserTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserTypes_TypeId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Users",
                newName: "Type_idId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_TypeId",
                table: "Users",
                newName: "IX_Users_Type_idId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserTypes_Type_idId",
                table: "Users",
                column: "Type_idId",
                principalTable: "UserTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
