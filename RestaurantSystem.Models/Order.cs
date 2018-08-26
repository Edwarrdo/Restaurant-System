﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantSystem.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string TableNumbers { get; set; }

        public string WaiterId { get; set; }
        [Required]
        public User Waiter { get; set; }

        public IEnumerable<OrderDrink> OrderDrinks { get; set; }

        public IEnumerable<OrderFood> OrderFoods { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public bool IsFinished { get; set; }

        [Required]
        public DateTime TimeOfOrder { get; set; }

        public DateTime? TimeOfPayment { get; set; }
    }
}