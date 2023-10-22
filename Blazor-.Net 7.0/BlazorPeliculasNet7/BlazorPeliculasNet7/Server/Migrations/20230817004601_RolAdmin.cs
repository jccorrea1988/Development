using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorPeliculasNet7.Server.Migrations
{
    /// <inheritdoc />
    public partial class RolAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO AspNetRoles(Id, Name, NormalizedName)
                                    VALUES ('226ec3f3-abdc-45ed-be84-00cd1aed67a6', 'admin', 'ADMIN')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE AspNetRoles WHERE Id = '226ec3f3-abdc-45ed-be84-00cd1aed67a6'");
        }
    }
}
