using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantSystem.Web.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = "Chef, Waiter, Bartender")]
    public abstract class EmployeeController : Controller
    {
    }
}