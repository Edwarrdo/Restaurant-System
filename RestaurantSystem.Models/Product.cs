﻿using System.Collections;
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

        public ICollection<FoodProduct> ProductFoods { get; set; } = new List<FoodProduct>();
    }
}