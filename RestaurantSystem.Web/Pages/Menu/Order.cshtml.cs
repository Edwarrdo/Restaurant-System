using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantSystem.Data;
using RestaurantSystem.Models;

namespace RestaurantSystem.Web.Pages.Menu
{
    public class OrderModel : PageModel
    {
        private RMSContext context;

        public OrderModel(RMSContext context)
        {
            this.context = context;
        }
        

        public IActionResult OnGetOrderFood(int id)
        {
            if(HttpContext.Session.GetString("meals") != null)
            {
                var meals = HttpContext.Session.GetString("meals");
                HttpContext.Session.SetString("meals", meals + "," + id.ToString());
            }
            else
            {
                HttpContext.Session.SetString("meals", id.ToString());
            }
            this.TempData["goodMessage"] = "Meal added to cart!";

            var meal = this.context.Foods.FirstOrDefault(m => m.Id == id);
            //STUPID IDEA BUT WHO CARES IT WORKS!
            var category = meal.Category;
            return RedirectToPage(category + "s");
        }

        public IActionResult OnGetOrderDrink(int id)
        {
            if (HttpContext.Session.GetString("drinks") != null)
            {
                var drinks = HttpContext.Session.GetString("drinks");
                HttpContext.Session.SetString("drinks", drinks + "," + id.ToString());
            }
            else
            {
                HttpContext.Session.SetString("drinks", id.ToString());
            }
            this.TempData["goodMessage"] = "Drink added to cart!";
            return RedirectToPage("Drinks");
        }
    }
}