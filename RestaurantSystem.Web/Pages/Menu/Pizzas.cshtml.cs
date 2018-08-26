using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantSystem.Data;
using RestaurantSystem.Models;

namespace RestaurantSystem.Web.Pages.Menu
{
    public class PizzasModel : PageModel
    {
        private RMSContext context;

        public PizzasModel(RMSContext context)
        {
            this.context = context;
        }

        public IEnumerable<Food> Pizzas { get; private set; }

        public void OnGet()
        {
            this.Pizzas = this.context.Foods.Where(f => f.Category == "Pizza").ToList();
        }
    }
}