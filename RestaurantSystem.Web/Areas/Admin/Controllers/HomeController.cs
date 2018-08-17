using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Data;
using RestaurantSystem.Web.Areas.Admin.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantSystem.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        private RMSContext context;
        private IMapper mapper;

        public HomeController(RMSContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var users = this.context.Users
                .Where(u => u.IsEmployee == true)
                .ToArray();
            var userModels = this.mapper.Map<IEnumerable<EmployeeConciseViewModel>>(users);

            return View(userModels);
        }
    }
}