using Microsoft.EntityFrameworkCore.Migrations;

namespace RTL.TvMazeScraper.Data.Migrations
{
    public partial class RelationFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "CharacterShow");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "CharacterShow",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
