using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Common.Order.BindingModels
{
    public class TablesBindingModel
    {
        [Required]
        public string[] Tables { get; set; }
    }
}
