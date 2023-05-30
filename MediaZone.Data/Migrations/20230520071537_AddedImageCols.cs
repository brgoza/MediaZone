using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediaZone.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedImageCols : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OriginalFilename",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "SizeInBytes",
                table: "Images",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalFilename",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "SizeInBytes",
                table: "Images");
        }
    }
}
