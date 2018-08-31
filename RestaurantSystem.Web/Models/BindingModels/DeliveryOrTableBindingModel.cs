using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Web.Models.BindingModels
{
    public class DeliveryOrTableBindingModel
    {
        [Required]
        [Display(Name = "Type of Service")]
        public string DeliveryOrTable { get; set; }
    }
}
