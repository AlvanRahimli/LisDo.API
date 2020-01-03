using Microsoft.EntityFrameworkCore.Migrations;

namespace LisDo.API.Migrations
{
    public partial class Beta3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Lisdos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Lisdos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Lisdos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Lisdos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Lisdos");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Lisdos");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Lisdos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Lisdos");
        }
    }
}
