using Chekich_fx.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chekich_fx.ViewComponents
{
    public class ChatViewComponent:ViewComponent
    {

        public IViewComponentResult Invoke(SenderReceiverInfo SRI)
        {
            return View(SRI);
        }
    }
}
