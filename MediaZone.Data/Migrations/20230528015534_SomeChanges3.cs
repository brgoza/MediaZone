using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaZone.Data.Migrations
{
    /// <inheritdoc />
    public partial class SomeChanges3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_HomeFolderId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_HomeFolderId",
                table: "AspNetUsers",
                column: "HomeFolderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_HomeFolderId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_HomeFolderId",
                table: "AspNetUsers",
                column: "HomeFolderId",
                unique: true);
        }
    }
}
