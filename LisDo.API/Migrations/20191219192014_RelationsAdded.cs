using Microsoft.EntityFrameworkCore.Migrations;

namespace LisDo.API.Migrations
{
    public partial class RelationsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Teams",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LisdoCount",
                table: "Teams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MemberCount",
                table: "Teams",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Teams",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Teams",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Lisdos",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Lisdos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Lisdos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpvoteCount",
                table: "Lisdos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LisdoId",
                table: "LisdoItems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Item_Users",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LisdoItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item_Users", x => new { x.LisdoItemId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Item_Users_LisdoItems_LisdoItemId",
                        column: x => x.LisdoItemId,
                        principalTable: "LisdoItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Item_Users_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Team_Users",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    TeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team_Users", x => new { x.TeamId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Team_Users_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Team_Users_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lisdos_AuthorId",
                table: "Lisdos",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lisdos_TeamId",
                table: "Lisdos",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Lisdos_UserId",
                table: "Lisdos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LisdoItems_LisdoId",
                table: "LisdoItems",
                column: "LisdoId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_Users_UserId",
                table: "Item_Users",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_Users_UserId",
                table: "Team_Users",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LisdoItems_Lisdos_LisdoId",
                table: "LisdoItems",
                column: "LisdoId",
                principalTable: "Lisdos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lisdos_AspNetUsers_AuthorId",
                table: "Lisdos",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lisdos_Teams_TeamId",
                table: "Lisdos",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lisdos_AspNetUsers_UserId",
                table: "Lisdos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LisdoItems_Lisdos_LisdoId",
                table: "LisdoItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Lisdos_AspNetUsers_AuthorId",
                table: "Lisdos");

            migrationBuilder.DropForeignKey(
                name: "FK_Lisdos_Teams_TeamId",
                table: "Lisdos");

            migrationBuilder.DropForeignKey(
                name: "FK_Lisdos_AspNetUsers_UserId",
                table: "Lisdos");

            migrationBuilder.DropTable(
                name: "Item_Users");

            migrationBuilder.DropTable(
                name: "Team_Users");

            migrationBuilder.DropIndex(
                name: "IX_Lisdos_AuthorId",
                table: "Lisdos");

            migrationBuilder.DropIndex(
                name: "IX_Lisdos_TeamId",
                table: "Lisdos");

            migrationBuilder.DropIndex(
                name: "IX_Lisdos_UserId",
                table: "Lisdos");

            migrationBuilder.DropIndex(
                name: "IX_LisdoItems_LisdoId",
                table: "LisdoItems");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "LisdoCount",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "MemberCount",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Lisdos");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Lisdos");

            migrationBuilder.DropColumn(
                name: "UpvoteCount",
                table: "Lisdos");

            migrationBuilder.DropColumn(
                name: "LisdoId",
                table: "LisdoItems");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Lisdos",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
