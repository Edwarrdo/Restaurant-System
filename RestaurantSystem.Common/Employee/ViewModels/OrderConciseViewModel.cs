using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Common.Employee.ViewModels
{
    public class OrderConciseViewModel
    {
        public int Id { get; set; }
        
        public string TableNumbers { get; set; }
        
        public double Price { get; set; }
        
        public DateTime TimeOfOrder { get; set; }
    }
}
