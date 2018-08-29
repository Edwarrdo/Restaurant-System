using RestaurantSystem.Common.Admin.BindingModels;
using RestaurantSystem.Common.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Services.Admin.Interfaces
{
    public interface IAdminEmployeesService
    {
        Task<int> CreateAsync(EmployeeCreationBindingModel model);

        Task<int> FireEmployeeAsync(string id);

        Task<EmployeeDetailsViewModel> GetEmployeeDetailsModelAsync(string id);

        Task<List<EmployeeConciseViewModel>> GetAllEmployeesModel();
    }
}
