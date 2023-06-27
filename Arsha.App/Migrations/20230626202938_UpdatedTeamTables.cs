using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arsha.App.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTeamTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phote",
                table: "Teams",
                newName: "Photo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Teams",
                newName: "Phote");
        }
    }
}
