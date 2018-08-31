using AutoMapper;
using RestaurantSystem.Data;
using RestaurantSystem.Models;
using RestaurantSystem.Services.Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Services.Storage
{
    public class StorageService : BaseEFService, IStorageService
    {
        public StorageService(RMSContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {
        }

        public Food GetMealById(int id)
        {
            return this.DbContext.Foods.FirstOrDefault(f => f.Id == id);
        }

        public IEnumerable<Drink> GetAllDrinks()
        {
            return this.DbContext.Drinks.Where(d => d.IsBanned == false);
        }

        public IEnumerable<Food> GetAllMealsByCategory(string category)
        {
            return this.DbContext.Foods.Where(d => d.IsBanned == false && d.Category == category);
        }
        public async Task<int> BanMealById(int id)
        {
            try
            {
                var meal = this.DbContext.Foods.FirstOrDefault(m => m.Id == id);
                meal.IsBanned = true;
                await this.DbContext.SaveChangesAsync();
            }
            catch
            {
                return 0;
            }
            return 1;
        }

        public async Task<int> BanDrinkById(int id)
        {
            try
            {
                var drink = this.DbContext.Drinks.FirstOrDefault(m => m.Id == id);
                drink.IsBanned = true;
                await this.DbContext.SaveChangesAsync();
            }
            catch
            {
                return 0;
            }
            return 1;
        }
    }
}
