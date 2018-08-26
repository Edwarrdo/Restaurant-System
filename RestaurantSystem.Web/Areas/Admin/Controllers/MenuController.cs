using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantSystem.Web.Areas.Admin.Controllers
{
    public class MenuController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}