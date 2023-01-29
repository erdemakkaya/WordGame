using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordGame.Infrastructure.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Words",
                type: "jsonb",
                nullable: true);
        }
    }
}
