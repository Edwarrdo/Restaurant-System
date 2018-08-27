using AutoMapper;
using RestaurantSystem.Models;
using RestaurantSystem.Web.Areas.Admin.Models.BindingModels;
using RestaurantSystem.Web.Areas.Admin.Models.ViewModels;
using RestaurantSystem.Web.Areas.Employee.Models.ViewModels;
using RestaurantSystem.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Web.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<EmployeeCreationBindingModel, User>();

            this.CreateMap<User, EmployeeConciseViewModel>();

            this.CreateMap<User, Areas.Admin.Models.ViewModels.EmployeeDetailsViewModel>();

            this.CreateMap<ProductCreationBindingModel, Product>();

            this.CreateMap<IngredientCreationBindingModel, Ingredient>();

            this.CreateMap<MealBindingModel, Food>();

            this.CreateMap<DrinkBindingModel, Drink>();

            this.CreateMap<Drink, Areas.Admin.Models.ViewModels.DrinkConciseViewModel>();

            this.CreateMap<Food, Areas.Admin.Models.ViewModels.FoodConciseViewModel>();

            this.CreateMap<User, Areas.Employee.Models.ViewModels.EmployeeDetailsViewModel>();

            this.CreateMap<Food, MealConciseViewModel>();

            this.CreateMap<Drink, Models.ViewModels.DrinkConciseViewModel>();

            this.CreateMap<Order, OrderConciseViewModel>();

            this.CreateMap<Food, Areas.Employee.Models.ViewModels.FoodConciseViewModel>();

            this.CreateMap<Drink, Areas.Employee.Models.ViewModels.DrinkConciseViewModel>();


        }
    }
}
