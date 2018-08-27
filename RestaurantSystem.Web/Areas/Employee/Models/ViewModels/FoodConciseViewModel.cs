using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Web.Areas.Employee.Models.ViewModels
{
    public class FoodConciseViewModel
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public long DishWeight { get; set; }
    }
}
