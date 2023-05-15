using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaZone.Data.Migrations
{
    /// <inheritdoc />
    public partial class BackToTPC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImagesTags");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectId",
                table: "Tags",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "HomeFolderId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "SubjectsTags",
                columns: table => new
                {
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TagText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectsTags", x => new { x.SubjectId, x.TagId });
                    table.ForeignKey(
                        name: "FK_SubjectsTags_Folders_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Folders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectsTags_Images_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectsTags_Tags_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_OwnerId",
                table: "Images",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_HomeFolderId",
                table: "AspNetUsers",
                column: "HomeFolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Folders_HomeFolderId",
                table: "AspNetUsers",
                column: "HomeFolderId",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_AspNetUsers_OwnerId",
                table: "Images",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Folders_HomeFolderId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_AspNetUsers_OwnerId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "SubjectsTags");

            migrationBuilder.DropIndex(
                name: "IX_Images_OwnerId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_HomeFolderId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "HomeFolderId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "ImagesTags",
                columns: table => new
                {
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZoneImageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagesTags", x => new { x.TagId, x.ZoneImageId });
                    table.ForeignKey(
                        name: "FK_ImagesTags_Images_ZoneImageId",
                        column: x => x.ZoneImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImagesTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImagesTags_ZoneImageId",
                table: "ImagesTags",
                column: "ZoneImageId");
        }
    }
}
