using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JDC.BusinessLogic.Interfaces;
using JDC.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JDC.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChatService chatService;
        private readonly UserManager<User> userManager;

        public ChatController(IChatService chatService, UserManager<User> userManager)
        {
            this.chatService = chatService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.Users
                .Include(i => i.Chats)
                .ThenInclude(i => i.Messages)
                .SingleAsync(i => User.Identity.Name == i.Email);

            return View(user.Chats.ToList());
        }

        [HttpPost]
        public async Task Send(Message message)
        {
            await chatService.Add(new Chat
            {
                Messages = new List<Message>
                {
                    message,
                },
            });
        }
    }
}
