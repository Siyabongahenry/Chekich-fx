using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult Create(PaymentType _paymentType)
        {
            
             return RedirectToActionPermanent(nameof(Pay));
            
        }
        public IActionResult Pay(Order order)
        {
            return View("Pay",order);
        }
    }
}
