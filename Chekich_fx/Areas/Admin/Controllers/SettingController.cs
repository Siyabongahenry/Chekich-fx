using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Chekich_fx.Data;
using Chekich_fx.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chekich_fx.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "writepolicy")]
    [AutoValidateAntiforgeryToken]
    public class SettingController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public SettingController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ColAddressBook()
        {
            List<Address> listAddress = await _db.Address
                .AsNoTracking()
                .Where(a => a.AddressType == AddressType.Collection)
                .ToListAsync();
            return View(listAddress);
        }
        public IActionResult CreateColAddress()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddColAddress(Address _address)
        {
            _address.ApplicationUser = await _userManager.GetUserAsync(User);
            _address.UserId = _address.ApplicationUser.Id;
            _db.Add(_address);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(ColAddressBook));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAddressStatus(int addressId,bool available=false)
        {
            var address = _db.Address.FirstOrDefault(a => a.Id == addressId);
            address.Available = available;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(ColAddressBook));
        }

    }
}
