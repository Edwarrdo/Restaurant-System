using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestaurantSystem.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsAllergen { get; set; }

        public ICollection<DrinkIngredient> IngredientDrinks { get; set; } = new List<DrinkIngredient>();
    }
}