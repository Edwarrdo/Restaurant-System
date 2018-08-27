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
    public class BeefsModel : PageModel
    {
        private RMSContext context;

        public BeefsModel(RMSContext context)
        {
            this.context = context;
        }

        public IEnumerable<Food> Beefs { get; private set; }

        public void OnGet()
        {
            this.Beefs = this.context.Foods.Where(f => f.Category == "Beef").ToList();
        }
    }
}