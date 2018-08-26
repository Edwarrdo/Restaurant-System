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
    public class RisottosModel : PageModel
    {
        private RMSContext context;

        public RisottosModel(RMSContext context)
        {
            this.context = context;
        }

        public IEnumerable<Food> Risottos { get; private set; }

        public void OnGet()
        {
            this.Risottos = this.context.Foods.Where(f => f.Category == "Risotto").ToList();
        }
    }
}