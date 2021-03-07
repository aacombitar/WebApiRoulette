using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiRoulette.Migrations
{
    public partial class acttablas3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Roulettes",
                newName: "IsOpen");

            migrationBuilder.AddColumn<int>(
                name: "ValuePayout",
                table: "Bets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Winner",
                table: "Bets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValuePayout",
                table: "Bets");

            migrationBuilder.DropColumn(
                name: "Winner",
                table: "Bets");

            migrationBuilder.RenameColumn(
                name: "IsOpen",
                table: "Roulettes",
                newName: "State");
        }
    }
}
