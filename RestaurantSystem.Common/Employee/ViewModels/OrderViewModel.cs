using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Common.Employee.ViewModels
{
    public class OrderViewModel
    {
        public string TableNumbers { get; set; }

        [NotMapped]
        public IEnumerable<DrinkConciseViewModel> Drinks { get; set; }

        [NotMapped]
        public IEnumerable<FoodConciseViewModel> Meals { get; set; }

        public double Price { get; set; }
        
        public DateTime TimeOfOrder { get; set; }
    }
}
