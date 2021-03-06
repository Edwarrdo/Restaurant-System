﻿namespace RestaurantSystem.Common.Constants
{
    public class WebConstants
    {
        public const string AdminArea = "Admin";
        public const string ClientArea = "Client";
        public const string EmployeeArea = "Employee";

        public const string AdminRole = "Administrator";
        public const string ClientRole = "Client";
        public const string WaiterRole = "Waiter";
        public const string BartenderRole = "Bartender";
        public const string ChefRole = "Chef";
        public const string EmployeesRoles = "Waiter, Chef, Bartender";

        public const string BadMessage = "badMessage";
        public const string GoodMessage = "goodMessage";

        public static string[] MealsCategories = new string[] { "Salat", "Appetizer", "Pizza", "Pasta", "Risotto", "Chicken", "Pork", "Beef", "Soup", "Dessert" };

        public const string MealsSessionKey = "meals";
        public const string DrinksSessionKey = "drinks";
    }
}
