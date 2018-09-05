using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Common.Constants;

namespace RestaurantSystem.Web.Areas.Employee.Controllers
{
    [Area(WebConstants.EmployeeArea)]
    [Authorize(Roles = WebConstants.EmployeesRoles)]
    public abstract class EmployeeController : Controller
    {
    }
}