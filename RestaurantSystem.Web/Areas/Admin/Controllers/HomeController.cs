using Microsoft.AspNetCore.Mvc;

namespace RestaurantSystem.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}