using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class FixMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_NamCategoryId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_NamCategoryId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "NamCategoryId",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "IdCategoryId",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Items_IdCategoryId",
                table: "Items",
                column: "IdCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_IdCategoryId",
                table: "Items",
                column: "IdCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_IdCategoryId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_IdCategoryId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IdCategoryId",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "NamCategoryId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Items_NamCategoryId",
                table: "Items",
                column: "NamCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_NamCategoryId",
                table: "Items",
                column: "NamCategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
