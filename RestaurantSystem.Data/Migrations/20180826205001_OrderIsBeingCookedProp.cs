using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantSystem.Data.Migrations
{
    public partial class OrderIsBeingCookedProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChefId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBeingCooked",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ChefId",
                table: "Orders",
                column: "ChefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_ChefId",
                table: "Orders",
                column: "ChefId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_ChefId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ChefId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ChefId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsBeingCooked",
                table: "Orders");
        }
    }
}
