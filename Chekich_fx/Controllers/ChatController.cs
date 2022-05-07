using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Chekich_fx.Data;
using Chekich_fx.Models;
using Chekich_fx.Utility;
using Chekich_fx.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chekich_fx.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ChatController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public string _appUserId { get; set; }
        public Product _product { get; set; }
        public ChatController(ApplicationDbContext db,UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var receiver = await _userManager.GetUsersInRoleAsync(URole.Admin);
            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {

                var SRI = await _db.SenderReceiverInfo
                    .AsNoTracking()
                    .Include(sri => sri.Chats)
                    .Include(sri => sri.Sender)
                    .FirstOrDefaultAsync(sri => sri.SenderId == senderId && sri.ReceiverId == receiver[0].Id);

                if (receiver[0].Id == senderId)
                {
                    return RedirectToAction("Index", "AChat", new { area = "Admin" });
                }
                ViewBag.IsAdmin = false;
                if (SRI == null)
                {
                    SRI = new SenderReceiverInfo
                    {
                        SenderId = senderId,
                        ReceiverId = receiver[0].Id
                    };
                    _db.SenderReceiverInfo.Add(SRI);
                    await _db.SaveChangesAsync();
                }


                return View(SRI);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<bool> SaveChat(string SenderId, string ReceiverId, string Message)
        {
            var SRI = new SenderReceiverInfo();
            try
            {
                var receiver = await _userManager.GetUsersInRoleAsync(URole.Admin);

                //consider using this code for production
                //if (User.IsInRole(URole.Admin) || User.IsInRole(URole.SuperAdmin) || User.IsInRole(URole.SuperManager))
                //{
                //    SRI = await _db.SenderAndReceiverInfo.FirstOrDefaultAsync(SI => SI.SenderId == ReceiverId && SI.ReceiverId == SenderId);
                //}
                //else 
                //{
                //    SRI = await _db.SenderAndReceiverInfo.FirstOrDefaultAsync(SI => SI.SenderId == SenderId && SI.ReceiverId == ReceiverId);
                //}

                if (SenderId == receiver[0].Id)
                {
                    SRI = await _db.SenderReceiverInfo
                        .AsNoTracking()
                        .FirstOrDefaultAsync(SI => SI.SenderId == ReceiverId && SI.ReceiverId == SenderId);
                }
                else
                {
                    SRI = await _db.SenderReceiverInfo
                        .AsNoTracking()
                        .FirstOrDefaultAsync(SI => SI.SenderId == SenderId && SI.ReceiverId == ReceiverId);
                }

                Chat chat = new Chat
                {
                    SRInfoId = SRI.Id,
                    Message = Message,
                    DateTime = DateTime.Now,
                    SenderId = SenderId,
                    ChatStatus = ChatStatus.NotRead,

                };
                _db.Chat.Add(chat);
                await _db.SaveChangesAsync();


                return true;
            }
            catch
            {
                return false;
            }

          

        }

    }
}
