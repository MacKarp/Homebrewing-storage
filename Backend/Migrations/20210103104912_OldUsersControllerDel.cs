using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class OldUsersControllerDel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expires_User_UserId",
                table: "Expires");

            migrationBuilder.DropForeignKey(
                name: "FK_Storages_User_UserId",
                table: "Storages");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Storages_UserId",
                table: "Storages");

            migrationBuilder.DropIndex(
                name: "IX_Expires_UserId",
                table: "Expires");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Storages");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Expires");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Storages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Expires",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Storages_Id",
                table: "Storages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Expires_Id",
                table: "Expires",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expires_AspNetUsers_Id",
                table: "Expires",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Storages_AspNetUsers_Id",
                table: "Storages",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expires_AspNetUsers_Id",
                table: "Expires");

            migrationBuilder.DropForeignKey(
                name: "FK_Storages_AspNetUsers_Id",
                table: "Storages");

            migrationBuilder.DropIndex(
                name: "IX_Storages_Id",
                table: "Storages");

            migrationBuilder.DropIndex(
                name: "IX_Expires_Id",
                table: "Expires");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Storages");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Expires");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Storages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Expires",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Storages_UserId",
                table: "Storages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Expires_UserId",
                table: "Expires",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expires_User_UserId",
                table: "Expires",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Storages_User_UserId",
                table: "Storages",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
