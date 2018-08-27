using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantSystem.Data.Migrations
{
    public partial class OrderFinishedProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsFinished",
                table: "Orders",
                newName: "MealsAreFinished");

            migrationBuilder.AddColumn<bool>(
                name: "DrinksAreFinished",
                table: "Orders",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DrinksAreFinished",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "MealsAreFinished",
                table: "Orders",
                newName: "IsFinished");
        }
    }
}
