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
    public class SalatsModel : PageModel
    {
        private RMSContext context;

        public SalatsModel(RMSContext context)
        {
            this.context = context;
        }

        public IEnumerable<Food> Salats { get; private set; }

        public void OnGet()
        {
            this.Salats = this.context.Foods.Where(f => f.Category == "Salat").ToList();
        }
    }
}