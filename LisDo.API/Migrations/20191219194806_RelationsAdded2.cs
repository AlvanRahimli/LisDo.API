using Microsoft.EntityFrameworkCore.Migrations;

namespace LisDo.API.Migrations
{
    public partial class RelationsAdded2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Clicked",
                table: "LisdoItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "LisdoItems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "LisdoItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequiredClick",
                table: "LisdoItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Clicked",
                table: "LisdoItems");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "LisdoItems");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "LisdoItems");

            migrationBuilder.DropColumn(
                name: "RequiredClick",
                table: "LisdoItems");
        }
    }
}
