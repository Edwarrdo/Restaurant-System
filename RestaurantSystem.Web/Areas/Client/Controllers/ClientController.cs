using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Common.Constants;

namespace RestaurantSystem.Web.Areas.Client.Controllers
{
    [Area(WebConstants.ClientArea)]
    [Authorize(Roles = WebConstants.ClientRole)]
    public abstract class ClientController : Controller
    {
    }
}