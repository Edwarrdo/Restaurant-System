using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Common.Employee.ViewModels;
using RestaurantSystem.Data;
using RestaurantSystem.Models;
using RestaurantSystem.Services.Exceptions;
using RestaurantSystem.Services.Order.Interfaces;

namespace RestaurantSystem.Web.Areas.Employee.Controllers
{
    public class OrdersController : EmployeeController
    {
        private IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        [HttpGet]
        public IActionResult OrderDetails(int id)
        {
            try
            {
                var orderModel = this.ordersService.GetOrderDetailsById(id);
                return View(orderModel);
            }
            catch(NotFoundException e)
            {
                this.TempData["badMessage"] = "No order with such id!";
                return RedirectToAction("Index", "Home", new { Area = "" });
            }
        }
    }
}