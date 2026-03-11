using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditDriverAndPassengerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Users_Id",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_Users_Id",
                table: "Passengers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Passengers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Drivers",
                newName: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Users_UserId",
                table: "Drivers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_Users_UserId",
                table: "Passengers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Users_UserId",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_Users_UserId",
                table: "Passengers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Passengers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Drivers",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Users_Id",
                table: "Drivers",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_Users_Id",
                table: "Passengers",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
