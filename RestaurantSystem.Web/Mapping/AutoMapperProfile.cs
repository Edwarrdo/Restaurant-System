﻿using AutoMapper;
using RestaurantSystem.Common.Admin.BindingModels;
using RestaurantSystem.Common.Admin.ViewModels;
using RestaurantSystem.Models;
using RestaurantSystem.Web.Areas.Employee.Models.ViewModels;
using RestaurantSystem.Web.Models.ViewModels;

namespace RestaurantSystem.Web.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<EmployeeCreationBindingModel, User>();

            this.CreateMap<User, EmployeeConciseViewModel>();

            this.CreateMap<User, RestaurantSystem.Common.Admin.ViewModels.EmployeeDetailsViewModel>();

            this.CreateMap<ProductCreationBindingModel, Product>();

            this.CreateMap<IngredientCreationBindingModel, Ingredient>();

            this.CreateMap<MealBindingModel, Food>();

            this.CreateMap<DrinkBindingModel, Drink>();

            this.CreateMap<Drink, RestaurantSystem.Common.Admin.ViewModels.DrinkConciseViewModel>();

            this.CreateMap<Food, RestaurantSystem.Common.Admin.ViewModels.FoodConciseViewModel>();

            this.CreateMap<User, Areas.Employee.Models.ViewModels.EmployeeDetailsViewModel>();

            this.CreateMap<Food, MealConciseViewModel>();

            this.CreateMap<Drink, Models.ViewModels.DrinkConciseViewModel>();

            this.CreateMap<Order, OrderConciseViewModel>();

            this.CreateMap<Food, Areas.Employee.Models.ViewModels.FoodConciseViewModel>();

            this.CreateMap<Drink, Areas.Employee.Models.ViewModels.DrinkConciseViewModel>();


        }
    }
}
