using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantSystem.Common.Constants
{
    public class Messages
    {
        public const string NotFoundMessage = "No {0} with such id found!";

        //Admin - Employees Managing Messages
        public const string EmployeeHiredErrorMessage = "Employee {1} couldn't be hired!";
        public const string EmployeeHiredSuccessMessage = "Employee {1} successfully hired!";
        public const string EmployeeFiredSuccessMessage = "Employee successfully fired!";

        //Admin - Storage Managing Messages
        public const string AddingFailureMessage = "{0} \"{1}\" coultn't be added!";
        public const string AddingSuccessMessage = "{0} \"{1}\" successfully added!";

        //Chef and Bartender Messages
        public const string TakeOrderFailureMessage = "Could not take {0}s!";
        public const string TakeOrderSuccessMessage = "{0}s taken successfully!";
        public const string FinishOrderFailureMessage = "Could not finish {0}s order!";
        public const string FinishOrderSuccessMessage = "{0}s finished!";

        //Waiter Messages
        public const string ApproveOrderFailureMessage = "Could not approve order!";
        public const string ApproveOrderSuccessMessage = "Order approved!";
        public const string PayForOrderFailureMessage = "Some things have not been delivered yet!";
        public const string PayForOrderSuccessMessage = "Order finished!";

        //Managing Orders Messages
        public const string OrderCreationFailureMessage = "Could not create the order!";
        public const string OrderCreationSuccessMessage = "Order created!";

        //Managing Cart Messages
        public const string BanFailureMessage = "{0} could not be banned!";
        public const string BanSuccessMessage = "{0} banned successfully!";
        public const string AddToCartSuccessMessage = "{0} added to cart!";
    }
}
