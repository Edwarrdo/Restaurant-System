using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Food>()
                .HasMany(f => f.Products)
                .WithOne(p => p.Food)
                .HasForeignKey(f => f.FoodId);

            builder.Entity<Drink>()
                .HasMany(d => d.Ingredients)
                .WithOne(i => i.Drink)
                .HasForeignKey(d => d.DrinkId);

            base.OnModelCreating(builder);
        }
    }
}
