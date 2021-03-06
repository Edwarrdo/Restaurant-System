﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Models;
using System;

namespace RestaurantSystem.Data
{
    public class RMSContext : IdentityDbContext<User>
    {
        public RMSContext(DbContextOptions<RMSContext> options)
            : base(options)
        {

        }

        public DbSet<Drink> Drinks { get; set; }

        public DbSet<Food> Foods { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<FoodProduct> FoodsProducts { get; set; }

        public DbSet<DrinkIngredient> DrinksIngredients { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDrink> OrdersDrinks { get; set; }

        public DbSet<OrderFood> OrdersFoods { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>()
                .HasOne(o => o.Waiter)
                .WithMany(w => w.Orders)
                .HasForeignKey(o => o.WaiterId);
            
            base.OnModelCreating(builder);
        }
    }
}
