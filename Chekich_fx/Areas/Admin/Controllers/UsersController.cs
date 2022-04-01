using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chekich_fx.Models;
using Chekich_fx.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Chekich_fx.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "SuperAdminOnly")]
    [AutoValidateAntiforgeryToken]
    public class UsersController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.GetUsersInRoleAsync(URole.Admin);
            return View(users);
        }
        [HttpPost]
        public async Task<string> AddUserToRole(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return "NotFound";
                }
                else if (await _userManager.IsInRoleAsync(user, URole.Admin))
                {
                    return "Exist";
                }
                await _userManager.AddToRoleAsync(user, URole.Admin);
                return "Enrolled";
            }
            catch
            {
                return "Error";
            }
            
        }
        [HttpPost]
        public async Task<string> RemoveUserFromRole(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return "NotFound";
                }
                await _userManager.RemoveFromRoleAsync(user, URole.Admin);

                return "Removed";
            }
            catch
            {
                return "Error";
            }
           
        }
    }
}
