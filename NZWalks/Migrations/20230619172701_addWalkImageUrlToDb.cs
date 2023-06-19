using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalks.Migrations
{
    /// <inheritdoc />
    public partial class addWalkImageUrlToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WalkImageUrl",
                table: "walks",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WalkImageUrl",
                table: "walks");
        }
    }
}
