using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RestaurantSystem.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        public bool IsEmployee { get; set; } = false;

        [RegularExpression("^[0-9]{10}$")]
        public string EGN { get; set; }

        [RegularExpression("^0[0-9]{9}$|^\\+359[0-9]{9}$")]
        public override string PhoneNumber { get; set; }

        public double Salary { get; set; }

        public string Address { get; set; }

        //employee self description
        public string About { get; set; }

        public int VacationDays { get; set; }

        //work experience
        public string Experience { get; set; }

        //last educations
        public string Education { get; set; }

        public DateTime HireDate { get; set; }
    }
}
