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
    public class PastasModel : PageModel
    {
        private RMSContext context;

        public PastasModel(RMSContext context)
        {
            this.context = context;
        }

        public IEnumerable<Food> Pastas { get; private set; }

        public void OnGet()
        {
            this.Pastas = this.context.Foods.Where(f => f.Category == "Pasta").ToList();
        }
    }
}