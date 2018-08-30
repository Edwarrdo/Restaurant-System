using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Common.Admin.BindingModels
{
    public class EmployeeCreationBindingModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string UserName { get; set; }

        public bool IsEmployee { get; set; } = true;

        [Required]
        [RegularExpression("^[0-9]{10}$")]
        public string EGN { get; set; }

        [Required]
        [RegularExpression("^0[0-9]{9}$|^\\+359[0-9]{9}$")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Salary { get; set; }

        [Required]
        public string Address { get; set; }

        //employee self description
        public string About { get; set; }

        [Required]
        public int VacationDays { get; set; }

        //work experience
        [Required]
        public string Experience { get; set; }

        //last educations
        [Required]
        public string Education { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
    }
}
