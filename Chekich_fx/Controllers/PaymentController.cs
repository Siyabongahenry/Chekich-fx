using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Chekich_fx.Data;
using Chekich_fx.Enums;
using Chekich_fx.Models;
using Chekich_fx.Utility;
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
            var order = await _db.Order
                .AsNoTracking()
                .OrderBy(a => a.DateTime)
                .LastOrDefaultAsync(o => o.UserId == userId);
            if (order != null)
            {
                bool hasPayment = await _db.OnlinePayments.AnyAsync(o => o.OrderId == order.Id);
                if (hasPayment)
                {
                    return RedirectToAction("Order", "Status");
                }

                OnlinePayment payment = new OnlinePayment
                {
                    Amount = order.TotalPrice,
                    OrderId = order.Id,
                    DateTime = DateTime.Now
                };
                return View(payment);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Online(string referenceId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _db.Order
                .OrderBy(a => a.DateTime)
                .LastOrDefaultAsync(o => o.UserId == userId);

            OnlinePayment payment = new OnlinePayment
            {
                Amount = order.TotalPrice,
                OrderId = order.Id,
                DateTime = DateTime.Now
            };
           
            _db.Add(payment);

            order.Status = OStatus.Pending;

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Complete));
        }

        public async Task<IActionResult> Cash()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _db.Order
                .OrderBy(a => a.DateTime)
                .LastOrDefaultAsync(o => o.UserId == userId);
            if (order != null)
            {
                var hasPayment = await _db.CashPayments.AnyAsync(c => c.OrderId == order.Id);
                if (hasPayment)
                {
                    return RedirectToAction("OrderManager", "Status");
                }
                CashPayment payment = new CashPayment
                {
                    Amount = order.TotalPrice,
                    OrderId = order.Id,
                    DateTime = DateTime.Now
                };
                _db.Add(payment);

                order.Status = OStatus.Pending;
               
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Complete));
            }
            else
            {
                return NotFound();
            }
        }
        public IActionResult Complete()
        {
            return View();
        }
    }
}
