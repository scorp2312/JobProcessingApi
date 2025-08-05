using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobCreator.Migrations
{
    /// <inheritdoc />
    public partial class AddedFieldResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "Jobs",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Result",
                table: "Jobs");
        }
    }
}
