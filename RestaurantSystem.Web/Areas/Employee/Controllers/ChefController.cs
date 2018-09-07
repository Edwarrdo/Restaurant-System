using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantSystem.Common.Constants;
using RestaurantSystem.Services.Employee.Interfaces;

namespace RestaurantSystem.Web.Areas.Employee.Controllers
{
    [Authorize(Roles = WebConstants.ChefRole)]
    public class ChefController : EmployeeController
    {
        private IChefService chefService;

        public ChefController(IChefService chefService)
        {
            this.chefService = chefService;
        }

        [HttpGet]
        public IActionResult OrdersNotBeingCooked()
        {
            var ordersModel = this.chefService.GetMealsNotBeingCooked();
            return View(ordersModel);
        }

        [HttpGet]
        public async Task<IActionResult> ChefTakeOrder(int id)
        {
            var userName = GetCurrentUserName();
            var result = await this.chefService.TakeOrderAsync(id, userName);
            if(result == 0)
            {
                this.TempData[WebConstants.BadMessage] = string.Format(Messages.TakeOrderFailureMessage, "meal");
            }
            else
            {
                this.TempData[WebConstants.GoodMessage] = string.Format(Messages.TakeOrderSuccessMessage, "Meal");
            }

            return RedirectToAction("OrdersNotBeingCooked", "Chef", new { area = WebConstants.EmployeeArea });
        }

        [HttpGet]
        public IActionResult TakenMealsOrders()
        {
            var chefName = GetCurrentUserName();
            var ordersModel = this.chefService.MealsTakenByChef(chefName);
            return View(ordersModel);
        }

        [HttpGet]
        public async Task<IActionResult> FinishMealsOrder(int id)
        {
            var result = await this.chefService.FinishOrderAsync(id);

            if(result == 0)
            {
                this.TempData[WebConstants.BadMessage] = string.Format(Messages.FinishOrderFailureMessage, "meal");
            }
            else
            {
                this.TempData[WebConstants.GoodMessage] = string.Format(Messages.FinishOrderSuccessMessage, "Meal");
            }

            return RedirectToAction("TakenMealsOrders", "Chef", new { area = WebConstants.EmployeeArea });
        }

        private string GetCurrentUserName()
        {
            var name = this.User.Identity.Name;
            return name;
        }
    }
}