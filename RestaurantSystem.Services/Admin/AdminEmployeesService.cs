using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RestaurantSystem.Common.Admin.BindingModels;
using RestaurantSystem.Common.Admin.ViewModels;
using RestaurantSystem.Data;
using RestaurantSystem.Models;
using RestaurantSystem.Services.Admin.Interfaces;
using RestaurantSystem.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Services.Admin
{
    public class AdminEmployeesService : BaseEFService, IAdminEmployeesService
    {
        private UserManager<User> userManager;

        public AdminEmployeesService(
            RMSContext dbContext,
            IMapper mapper, UserManager<User> userManager)
            : base(dbContext, mapper)
        {
            this.userManager = userManager;
        }

        public async Task<int> CreateAsync(EmployeeCreationBindingModel model)
        {
            var employee = this.Mapper.Map<User>(model);
            employee.UserName = employee.FirstName + employee.LastName;
            try
            {
                await this.userManager.CreateAsync(employee, model.Password);
                await userManager.AddToRoleAsync(employee, model.Role);
            }
            catch
            {
                //failure
                return 0;
            }
            //success
            return 1;
        }

        public async Task<int> FireEmployeeAsync(string id)
        {
            var employee = this.DbContext.Users.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return 0;
            }
            try
            {
                this.DbContext.Remove(employee);
                await this.DbContext.SaveChangesAsync();
            }
            catch
            {
                //failure
                return 0;
            }
            //success
            return 1;
        }

        public async Task<EmployeeDetailsViewModel> GetEmployeeDetailsModelAsync(string id)
        {
            var employee = this.DbContext.Users.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                throw new NotFoundException();
            }

            var model = this.Mapper.Map<EmployeeDetailsViewModel>(employee);
            await FindUserProfession(employee, model);

            return model;
        }

        public async Task<List<EmployeeConciseViewModel>> GetAllEmployeesModel()
        {
            var users = this.DbContext.Users
                .Where(u => u.IsEmployee == true)
                .ToArray();
            var userModels = this.Mapper.Map<List<EmployeeConciseViewModel>>(users);
            for (int i = 0; i < users.Count(); i++)
            {
                await FindUserProfession(users[i], userModels[i]);
            }

            return userModels;
        }

        private async Task FindUserProfession(User currentUser, EmployeeDetailsViewModel model)
        {
            if (await userManager.IsInRoleAsync(currentUser, "Chef"))
            {
                model.Profession = "Chef";
            }
            else if (await userManager.IsInRoleAsync(currentUser, "Bartender"))
            {
                model.Profession = "Bartender";
            }
            else
            {
                model.Profession = "Waiter";
            }
        }

        private async Task FindUserProfession(User currentUser, EmployeeConciseViewModel model)
        {
            if (await userManager.IsInRoleAsync(currentUser, "Chef"))
            {
                model.Profession = "Chef";
            }
            else if (await userManager.IsInRoleAsync(currentUser, "Bartender"))
            {
                model.Profession = "Bartender";
            }
            else
            {
                model.Profession = "Waiter";
            }
        }
    }
}
