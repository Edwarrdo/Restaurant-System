using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestaurantSystem.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsAllergen { get; set; }

        public int FoodId { get; set; }
        public Food Food { get; set; }

        public int DrinkId { get; set; }
        public Drink Drink { get; set; }
    }
}