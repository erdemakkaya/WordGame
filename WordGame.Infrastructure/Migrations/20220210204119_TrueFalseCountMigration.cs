using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordGame.Infrastructure.Migrations
{
    public partial class TrueFalseCountMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FalseCount",
                table: "Words",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TrueCount",
                table: "Words",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
