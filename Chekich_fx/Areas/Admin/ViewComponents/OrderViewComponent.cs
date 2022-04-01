using Chekich_fx.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chekich_fx.Areas.Admin.ViewComponents
{
    public class OrderViewComponent:ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public OrderViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }
        public string Invoke()
        {
            string UserId = HttpContext.Request.Cookies["UserId"];
            var Count = 0;
            try
            {
                Count = _db.Order.ToList().Count();
            }
            catch
            {
                ModelState.AddModelError("", "Having trouble with connections. " +
                    "Try again, and if the problem persists " +
                     "see your system administrator.");
            }
            return Count.ToString();
        }
    }
}
