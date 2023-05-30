using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaZone.Data.Migrations
{
    /// <inheritdoc />
    public partial class SomeChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "HomeFolderId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_HomeFolderId",
                table: "AspNetUsers",
                column: "HomeFolderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Folders_HomeFolderId",
                table: "AspNetUsers",
                column: "HomeFolderId",
                principalTable: "Folders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Folders_HomeFolderId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_HomeFolderId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "HomeFolderId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
