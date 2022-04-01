using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chekich_fx.Hubs
{
    public class ChatHub:Hub
    {
        public async Task SendMessage(string _receiverId,string _senderId,string _message,int _screenId)
        {
            await Clients.User(_receiverId).SendAsync("ReceiveMessage",_senderId, _message,_screenId);
        }
        public async Task UserTypingAlert(string _receiverId,int _typingAlertId)
        {
            await Clients.User(_receiverId).SendAsync("ReceiveTypingAlert",_typingAlertId);
        }
        
    }
}
