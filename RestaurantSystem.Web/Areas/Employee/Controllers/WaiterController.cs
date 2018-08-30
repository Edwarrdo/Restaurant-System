using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Services.Employee.Interfaces;

namespace RestaurantSystem.Web.Areas.Employee.Controllers
{
    public class WaiterController : EmployeeController
    {
        private IWaiterService waiterService;

        public WaiterController(IWaiterService waiterService)
        {
            this.waiterService = waiterService;
        }

        [HttpGet]
        public IActionResult OrdersForApproval()
        {
            var ordersModel = this.waiterService.GetOrdersForApproval();
            return View(ordersModel);
        }

        [HttpGet]
        public async Task<IActionResult> Approve(int id)
        {
            var waiterName = this.GetCurrentUserName();
            var result = await this.waiterService.WaiterApproveOrder(waiterName, id);
            if (result == 0)
            {
                this.TempData["badMessage"] = "Could not approve order!";
            }
            else
            {
                this.TempData["goodMessage"] = "Order approved!";
            }

            return RedirectToAction("OrdersForApproval", "Waiter", new { area = "Employee" });
        }

        [HttpGet]
        public IActionResult FinishedOrders()
        {
            var ordersModel = this.waiterService.GetFinishedOrders();
            return View(ordersModel);
        }

        [HttpGet]
        public async Task<IActionResult> PayForOrder(int id)
        {
            var result = await this.waiterService.PayForOrder(id);
            if (result == 0)
            {
                TempData["badMessage"] = "Some things have not been delivered yet!";
            }
            else
            {
                TempData["goodMessage"] = "Order finished!";
            }
            return RedirectToAction("FinishedOrders", "Waiter", new { area = "Employee" });
        }

        private string GetCurrentUserName()
        {
            var name = this.User.Identity.Name;
            return name;
        }
    }
}