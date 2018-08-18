using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantSystem.Models
{
    public class Drink
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Milliliters { get; set; }

        public string Description { get; set; }

        public ICollection<DrinkIngredient> DrinkIngredients { get; set; } = new List<DrinkIngredient>();
    }
}
