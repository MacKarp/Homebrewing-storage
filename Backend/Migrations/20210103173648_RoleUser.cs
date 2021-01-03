using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class RoleUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"Insert into AspNetRoles (Id, [Name], [NormalizedName]) values ('8615fbb8-7edf-43b3-af93-878bd7f482db', 'User', 'USER')"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"delete AspNetRoles where id = '8615fbb8-7edf-43b3-af93-878bd7f482db'");
        }
    }
}
