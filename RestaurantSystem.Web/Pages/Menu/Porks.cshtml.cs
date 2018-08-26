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
    public class PorksModel : PageModel
    {
        private RMSContext context;

        public PorksModel(RMSContext context)
        {
            this.context = context;
        }

        public IEnumerable<Food> Porks { get; private set; }

        public void OnGet()
        {
            this.Porks = this.context.Foods.Where(f => f.Category == "Pork").ToList();
        }
    }
}