﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Common.Order.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<DrinkConciseViewModel> Drinks { get; set; }

        public IEnumerable<MealConciseViewModel> Meals { get; set; }

        public double Price { get; set; }
    }
}
