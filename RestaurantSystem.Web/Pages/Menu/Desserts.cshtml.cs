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
    public class DessertsModel : PageModel
    {
        private RMSContext context;

        public DessertsModel(RMSContext context)
        {
            this.context = context;
        }

        public IEnumerable<Food> Desserts { get; private set; }

        public void OnGet()
        {
            this.Desserts = this.context.Foods.Where(f => f.Category == "Dessert").ToList();
        }
    }
}