using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chekich_fx.Data;
using Chekich_fx.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chekich_fx.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "writepolicy")]
    [AutoValidateAntiforgeryToken]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;
        public  OrderController(ApplicationDbContext db)
        { 
            _db = db;
        }

        public IActionResult Index()
        {
           
            return View();
        }
        public IActionResult Details(int? Id)
        {
            try
            {
                if (Id != null)
                {
      

                    return View();
                }
                return View(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult UpdateCheckoutStatus(int Id,string Checkout_Status,string message="Your OrderItems is being processed.")
        {
            try
            {
               
                return RedirectToAction("Details");
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
