﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Web.Areas.Employee.Models.ViewModels
{
    public class DrinkConciseViewModel
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public long Milliliters { get; set; }
    }
}
