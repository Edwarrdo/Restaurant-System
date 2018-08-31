using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantSystem.Models
{
    public class Food
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public long DishWeight { get; set; }

        [Required]
        public string Category { get; set; }

        public bool IsBanned { get; set; } = false;

        public ICollection<FoodProduct> FoodProducts { get; set; } = new List<FoodProduct>();
    }
}
