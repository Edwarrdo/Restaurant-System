using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantSystem.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Drinks_DrinkId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Foods_FoodId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_DrinkId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_FoodId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DrinkId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "FoodId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "FoodsProducts",
                columns: table => new
                {
                    FoodId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodsProducts", x => new { x.FoodId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_FoodsProducts_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodsProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    IsAllergen = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrinksIngredients",
                columns: table => new
                {
                    DrinkId = table.Column<int>(nullable: false),
                    IngredientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinksIngredients", x => new { x.DrinkId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_DrinksIngredients_Drinks_DrinkId",
                        column: x => x.DrinkId,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrinksIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrinksIngredients_IngredientId",
                table: "DrinksIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodsProducts_ProductId",
                table: "FoodsProducts",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrinksIngredients");

            migrationBuilder.DropTable(
                name: "FoodsProducts");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.AddColumn<int>(
                name: "DrinkId",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FoodId",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_DrinkId",
                table: "Products",
                column: "DrinkId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_FoodId",
                table: "Products",
                column: "FoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Drinks_DrinkId",
                table: "Products",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Foods_FoodId",
                table: "Products",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
