using System;

namespace RestaurantSystem.Web.Areas.Employee.Models.ViewModels
{
    public class EmployeeDetailsViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string EGN { get; set; }

        public double Salary { get; set; }

        public string Address { get; set; }

        public string About { get; set; }

        public int VacationDays { get; set; }

        public string Experience { get; set; }

        public string Education { get; set; }

        public DateTime HireDate { get; set; }

        public string Profession { get; set; }
    }
}
