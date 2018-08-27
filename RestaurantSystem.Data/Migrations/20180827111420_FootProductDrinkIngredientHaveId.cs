using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantSystem.Data.Migrations
{
    public partial class FootProductDrinkIngredientHaveId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodsProducts",
                table: "FoodsProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrinksIngredients",
                table: "DrinksIngredients");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FoodsProducts",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DrinksIngredients",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodsProducts",
                table: "FoodsProducts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrinksIngredients",
                table: "DrinksIngredients",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FoodsProducts_FoodId",
                table: "FoodsProducts",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_DrinksIngredients_DrinkId",
                table: "DrinksIngredients",
                column: "DrinkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodsProducts",
                table: "FoodsProducts");

            migrationBuilder.DropIndex(
                name: "IX_FoodsProducts_FoodId",
                table: "FoodsProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrinksIngredients",
                table: "DrinksIngredients");

            migrationBuilder.DropIndex(
                name: "IX_DrinksIngredients_DrinkId",
                table: "DrinksIngredients");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FoodsProducts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DrinksIngredients");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodsProducts",
                table: "FoodsProducts",
                columns: new[] { "FoodId", "ProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrinksIngredients",
                table: "DrinksIngredients",
                columns: new[] { "DrinkId", "IngredientId" });
        }
    }
}
