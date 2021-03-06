﻿namespace RestaurantSystem.Models
{
    public class OrderDrink
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
        
        public int DrinkId { get; set; }
        public Drink Drink { get; set; }
    }
}