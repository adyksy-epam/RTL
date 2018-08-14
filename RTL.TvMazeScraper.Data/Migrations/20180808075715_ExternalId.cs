using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RTL.TvMazeScraper.Data.Migrations
{
    public partial class ExternalId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ExtId",
                table: "Shows",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "Characters",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<long>(
                name: "ExtId",
                table: "Characters",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtId",
                table: "Shows");

            migrationBuilder.DropColumn(
                name: "ExtId",
                table: "Characters");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "Characters",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
