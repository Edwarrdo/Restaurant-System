namespace RestaurantSystem.Models
{
    public class DrinkIngredient
    {
        public int Id { get; set; }

        public int DrinkId { get; set; }
        public Drink Drink { get; set; }

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}