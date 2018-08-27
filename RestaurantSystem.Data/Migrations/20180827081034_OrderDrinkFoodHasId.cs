using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantSystem.Data.Migrations
{
    public partial class OrderDrinkFoodHasId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdersFoods",
                table: "OrdersFoods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdersDrinks",
                table: "OrdersDrinks");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrdersFoods",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrdersDrinks",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdersFoods",
                table: "OrdersFoods",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdersDrinks",
                table: "OrdersDrinks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersFoods_FoodId",
                table: "OrdersFoods",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersDrinks_DrinkId",
                table: "OrdersDrinks",
                column: "DrinkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdersFoods",
                table: "OrdersFoods");

            migrationBuilder.DropIndex(
                name: "IX_OrdersFoods_FoodId",
                table: "OrdersFoods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdersDrinks",
                table: "OrdersDrinks");

            migrationBuilder.DropIndex(
                name: "IX_OrdersDrinks_DrinkId",
                table: "OrdersDrinks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrdersFoods");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrdersDrinks");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdersFoods",
                table: "OrdersFoods",
                columns: new[] { "FoodId", "OrderId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdersDrinks",
                table: "OrdersDrinks",
                columns: new[] { "DrinkId", "OrderId" });
        }
    }
}
