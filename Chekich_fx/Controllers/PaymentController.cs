using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Chekich_fx.Data;
using Chekich_fx.Enums;
using Chekich_fx.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chekich_fx.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public PaymentController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(PaymentType _paymentType)
        {
            if(_paymentType == PaymentType.Online)
            {
                return RedirectToActionPermanent(nameof(Online));
            }
            return RedirectToActionPermanent(nameof(Cash));
        }
        
        public async Task<IActionResult> Online()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _db.Order.FirstOrDefaultAsync(o => o.UserId == userId);

            OnlinePayment payment = new OnlinePayment
            {
                Amount = order.TotalPrice,
                OrderId = order.Id,
                DateTime = DateTime.Now
            };
            return View(payment);
        }
        public async Task<IActionResult> Online(string referenceId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _db.Order.FirstOrDefaultAsync(o => o.UserId == userId);

            OnlinePayment payment = new OnlinePayment
            {
                Amount = order.TotalPrice,
                OrderId = order.Id,
                DateTime = DateTime.Now
            };
            return View(payment);
        }

        public async Task<IActionResult> Cash()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _db.Order.FirstOrDefaultAsync(o => o.UserId == userId);

            CashPayment payment = new CashPayment
            {
                Amount = order.TotalPrice,
                OrderId = order.Id,
                DateTime = DateTime.Now
            };
            await _db.AddAsync(payment);
            return RedirectToAction(nameof(Complete));
        }
        public IActionResult Complete()
        {
            return View();
        }

    }
}
