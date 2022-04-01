using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Chekich_fx.Data;
using Chekich_fx.Extensions;
using Chekich_fx.Models;
using Chekich_fx.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

namespace Chekich_fx.Controllers
{
    [Authorize]
    //[AutoValidateAntiforgeryToken]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        private readonly IEmailSender _emailSender;
        public ProfileController(ApplicationDbContext db,UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _db = db;
            _userManager = userManager;
            _emailSender = emailSender;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ApplicationUser applicationUser = await _userManager.GetUserAsync(User);
                return View(applicationUser);
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<string> ReplaceFirstName(string newFirstName)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var applicationUser = await _db.ApplicationUser.FirstOrDefaultAsync(u => u.Id == userId);
                applicationUser.FirstName = newFirstName;
                await _db.SaveChangesAsync();
                return "true";
            }
            catch
            {
                return "false";
            }
            

        }
        [HttpPost]
        public async Task<string> ReplaceLastName(string newLastName)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var applicationUser = await _db.ApplicationUser.FirstOrDefaultAsync(u => u.Id == userId);
                applicationUser.LastName = newLastName;
                await _db.SaveChangesAsync();
                return "true";
            }
            catch
            {
                return "false";
            }

        }
        [HttpPost]
        public async Task<string> ReplacePhoneNumber(string newNumber)
        {
            try
            {
                if (newNumber.IsValidNumber())
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var user = await _db.ApplicationUser.FirstOrDefaultAsync(u => u.Id == userId);
                    user.PhoneNumber = newNumber;
                    return "true";
                }
                else
                {
                    return "false";
                }
               
            }
            catch
            {
                return "false";
            }
           
        }
        [HttpPost]
        public async Task<string> ReplaceEmail(string newEmail)
        {
            try
            {
                ApplicationUser user = await _userManager.GetUserAsync(User);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmailChange",
                    pageHandler: null,
                    values: new { area = "Identity", userId = user.Id,email = newEmail, code = code, returnUrl = "/Profile" },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(newEmail, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return "true";
            }
            catch
            {
                return "false";
            } 
        }
        public async Task<IActionResult> AddressBook()
        {
            try
            {
                List<Address> addressList = await _db.Address
                    .AsNoTracking()
                    .Where(a => a.UserId == User.GetLoggedInUserId<string>())
                    .ToListAsync();
                return View(addressList);
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Remove(int? Id)
        {
            if (Id !=null)
            {
                try
                {
                    var address = _db.Address.FirstOrDefault(a => a.Id == Id);
                    if (address != null)
                    {
                        _db.Address.Remove(address);
                    }
                    await _db.SaveChangesAsync();
                    return RedirectToAction("AddressBook");
                }
                catch
                {
                    return NotFound();
                }
            }
            return NotFound();
        }
        public IActionResult InsertAddress(string returnUrl=null)
        {
            if(returnUrl != null)
            {
                ViewBag.ReturnUrl = returnUrl;
            }
            Address address;
            if(User.IsInRole(URole.Admin) || User.IsInRole(URole.SuperAdmin))
            {

                address = new Address
                {
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    AddressType = AddressType.Collection,
                    Available =true
                };
            }
            else
            {
                address = new Address
                {
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    AddressType = AddressType.Delivery,
                    Available =true
                };
            }
            return View(address);
        }
        [HttpPost]
        public async Task<IActionResult> InsertAddress(Address _address)
        {   
            try
            {
                _db.Address.Add(_address);
                await _db.SaveChangesAsync();
                //if (returnUrl != null)
                //{
                //    if (Url.IsLocalUrl(returnUrl))
                //    {
                //        return RedirectToAction(returnUrl);
                //    }
                //}
                return RedirectToAction(nameof(AddressBook));
            }
            catch
            {
                return NotFound();
            }
        }
       
    }
}
