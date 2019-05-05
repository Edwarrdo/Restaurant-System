using AutoMapper;
using RestaurantSystem.Common.Admin.BindingModels;
using RestaurantSystem.Common.Admin.ViewModels;
using RestaurantSystem.Common.Employee.ViewModels;
using RestaurantSystem.Common.Order.ViewModels;
using RestaurantSystem.Models;

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

            this.CreateMap<User, RestaurantSystem.Common.Employee.ViewModels.EmployeeDetailsViewModel>();

            this.CreateMap<Food, MealConciseViewModel>();

            this.CreateMap<Drink, RestaurantSystem.Common.Order.ViewModels.DrinkConciseViewModel>();

            this.CreateMap<Order, OrderConciseViewModel>();

            this.CreateMap<Food, RestaurantSystem.Common.Employee.ViewModels.FoodConciseViewModel>();

            this.CreateMap<Drink, RestaurantSystem.Common.Employee.ViewModels.DrinkConciseViewModel>();
        }
    }
}
