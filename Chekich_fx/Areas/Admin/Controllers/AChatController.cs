using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Chekich_fx.Data;
using Chekich_fx.Models;
using Chekich_fx.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chekich_fx.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "writepolicy")]
    public class AChatController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public AChatController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.IsAdmin = true;
            try
            {
                var SRI = await _db
                    .SenderReceiverInfo
                    .AsNoTracking()
                    .Include(sri => sri.Chats)
                    .Include(sri => sri.Sender)
                    .ToListAsync();

                return View(SRI);
                //The admin is registered as a Receiver in the database
            }
            catch 
            {
                return NotFound();
            }
            
        }
    }
}
