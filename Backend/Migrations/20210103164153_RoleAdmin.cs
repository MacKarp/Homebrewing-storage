using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class RoleAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"Insert into AspNetRoles (Id, [Name], [NormalizedName]) values ('660e79fc - 5232 - 4610 - 908b - 202925d7c3a3', 'Admin', 'Admin')"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"delete AspNetRoles where id = '660e79fc - 5232 - 4610 - 908b - 202925d7c3a3'");
        }
    }
}
