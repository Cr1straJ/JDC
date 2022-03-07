using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using JDC.Common.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace JDC.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserManager<User> userManager;
        private readonly IMessageService messageService;

        public ChatHub(UserManager<User> userManager, IMessageService messageService)
        {
            this.userManager = userManager;
            this.messageService = messageService;
        }

        public async Task SelectGroup(string group)
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, group);
        }

        public async Task SendMessage(string mess, string group, int groupId)
        {
            var user = await this.userManager.GetUserAsync(this.Context.User);
            var message = new Message(groupId, user.Id, RecipientType.Deafult, mess, DateTime.Now);
            await this.messageService.Add(message);
            await this.Clients.Group(group).SendAsync("ReceiveMessage", user.UserName, user.FirstName, message.Id, message.Content, message.SendingDate.ToString("hhh:mm"));
        }
    }
}
