﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantSystem.Web.Areas.Client.Controllers
{
    public class HomeController : ClientController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}