using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Data;
using RestaurantSystem.Web.Areas.Employee.Models.ViewModels;

namespace RestaurantSystem.Web.Areas.Employee.Controllers
{
    public class OrdersController : EmployeeController
    {
        private RMSContext context;
        private IMapper mapper;

        public OrdersController(RMSContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult ApproveOrders()
        {
            var orders = this.context.Orders.Where(o => o.Approved == false);
            var ordersModel = this.mapper.Map<IEnumerable<OrderConciseViewModel>>(orders);
            return View(ordersModel);
        }

        public IActionResult OrderDetails(int id)
        {
            var order = this.context.Orders.FirstOrDefault(o => o.Id == id);
            var orderModel = this.mapper.Map<OrderViewModel>(order);
            return View(ordersModel);
        }
    }
}