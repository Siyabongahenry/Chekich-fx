using Chekich_fx.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Chekich_fx.ViewComponents
{
    public class ChatNotificationViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public ChatNotificationViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }
        public string Invoke()
        {
            string UserId = HttpContext.Request.Cookies["UserId"];
            string chatCount = "0";
            try
            {
                var chat = _db.SenderReceiverInfo.Include(c => c.Chats).FirstOrDefault(c => c.SenderId == UserId || c.ReceiverId == UserId);
                if (chat != null)
                {
                    chatCount = chat.Chats.Count.ToString();
                }
                else
                {
                    chatCount = "0";
                }
            }
            catch
            {
                chatCount = "0";
            }
            return chatCount;
        }
    }
}
