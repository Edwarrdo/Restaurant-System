using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Web.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendEmployee(string firstName, string lastName, string email, string phoneNumber)
        {
            await Clients.All.SendAsync("ReceiveEmployee", firstName, lastName, email, phoneNumber);
        }
        public async Task SendMessage(string user, string message, string something)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message, something);
        }
    }
}
