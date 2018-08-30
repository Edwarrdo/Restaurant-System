using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Common.Order.ViewModels
{
    public class DrinkConciseViewModel
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public long Milliliters { get; set; }

        public string Category { get; set; }
    }
}
