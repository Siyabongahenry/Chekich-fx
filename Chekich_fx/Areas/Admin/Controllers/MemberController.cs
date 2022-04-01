using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chekich_fx.Areas.Admin.Models;
using Chekich_fx.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Chekich_fx.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "writepolicy")]
    [AutoValidateAntiforgeryToken]
    public class MemberController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public MemberController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string email)
        {
            ApplicationUserRole UserRole = new ApplicationUserRole();
            try
            {
                if (!String.IsNullOrEmpty(email))
                {
                    UserRole.User = await _userManager.FindByEmailAsync(email);
                    UserRole.Role = null;
                }

                return View(UserRole);
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(ApplicationUserRole AppURole)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _userManager.AddToRoleAsync(AppURole.User, AppURole.Role);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
