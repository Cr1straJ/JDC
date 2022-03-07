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
            var user = await this.userManager.Users.Include(i=>i.Chats).ThenInclude(i=>i.Messages).SingleAsync(i=> User.Identity.Name == i.Email);
            var groups = user.Chats;
            return View(groups.ToList());
        }
    }
}
