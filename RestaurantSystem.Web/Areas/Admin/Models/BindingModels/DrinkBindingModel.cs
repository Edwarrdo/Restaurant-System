using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Web.Areas.Admin.Models.BindingModels
{
    public class DrinkBindingModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public long Milliliters { get; set; }

        [Required]
        public IEnumerable<string> Ingredients { get; set; }
    }
}
