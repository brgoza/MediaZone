﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaZone.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamedParentsAndChildrenCols : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Folders_ParentFolderId",
                table: "Folders");

            migrationBuilder.RenameColumn(
                name: "ParentFolderId",
                table: "Folders",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_Folders_ParentFolderId",
                table: "Folders",
                newName: "IX_Folders_ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Folders_ParentId",
                table: "Folders",
                column: "ParentId",
                principalTable: "Folders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Folders_ParentId",
                table: "Folders");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "Folders",
                newName: "ParentFolderId");

            migrationBuilder.RenameIndex(
                name: "IX_Folders_ParentId",
                table: "Folders",
                newName: "IX_Folders_ParentFolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Folders_ParentFolderId",
                table: "Folders",
                column: "ParentFolderId",
                principalTable: "Folders",
                principalColumn: "Id");
        }
    }
}
