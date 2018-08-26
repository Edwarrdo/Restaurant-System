using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantSystem.Web.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize(Roles = "Client")]
    public abstract class ClientController : Controller
    {
    }
}