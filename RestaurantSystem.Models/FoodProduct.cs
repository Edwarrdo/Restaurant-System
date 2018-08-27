namespace RestaurantSystem.Models
{
    public class FoodProduct
    {
        public int Id { get; set; }

        public int FoodId { get; set; }
        public Food Food { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}