using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiRoulette.Migrations
{
    public partial class acttablas5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateBet",
                table: "Bets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateBet",
                table: "Bets");
        }
    }
}
