using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Common.Admin.ViewModels
{
    public class DrinkConciseViewModel
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public long Milliliters { get; set; }
    }
}
