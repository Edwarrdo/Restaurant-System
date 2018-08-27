using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantSystem.Data.Migrations
{
    public partial class DrinksAreBeingPreppedNameFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DrinkIsBeingPrepped",
                table: "Orders",
                newName: "DrinksAreBeingPrepped");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DrinksAreBeingPrepped",
                table: "Orders",
                newName: "DrinkIsBeingPrepped");
        }
    }
}
