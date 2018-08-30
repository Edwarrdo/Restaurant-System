using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Common.Admin.BindingModels
{
    public class MealBindingModel
    {
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

        [Required]
        public IEnumerable<string> Products { get; set; }
    }
}
