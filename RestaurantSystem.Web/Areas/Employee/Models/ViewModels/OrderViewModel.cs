using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Web.Areas.Employee.Models.ViewModels
{
    public class OrderViewModel
    {
        public string TableNumbers { get; set; }

        [AutoMapper.IgnoreMap]
        public IEnumerable<DrinkConciseViewModel> Drinks { get; set; }

        [AutoMapper.IgnoreMap]
        public IEnumerable<FoodConciseViewModel> Meals { get; set; }

        public double Price { get; set; }
        
        public DateTime TimeOfOrder { get; set; }
    }
}
