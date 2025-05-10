using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityProject.Migrations
{
    /// <inheritdoc />
    public partial class migTrash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInTrash",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInTrash",
                table: "Messages");
        }
    }
}
