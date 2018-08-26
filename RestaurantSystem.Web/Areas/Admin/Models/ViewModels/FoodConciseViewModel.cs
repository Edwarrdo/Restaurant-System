using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Web.Areas.Admin.Models.ViewModels
{
    public class FoodConciseViewModel
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public long DishWeight { get; set; }

        public string Category { get; set; }
    }
}
