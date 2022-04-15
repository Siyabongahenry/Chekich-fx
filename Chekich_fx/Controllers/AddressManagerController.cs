using Chekich_fx.Data;
using Chekich_fx.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chekich_fx.Controllers
{
    [Authorize]
    public class AddressManagerController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AddressManagerController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: AddressManagerController
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var addressList = await _db.Address.Where(a=>a.UserId == userId).ToListAsync();
            return View(addressList);
        }

        // GET: AddressManagerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AddressManagerController/Create
        public ActionResult Create(string returnUrl)
        {
            ViewBag.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: AddressManagerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Address _address,string _returnUrl=null)
        {
            if (!ModelState.IsValid)
            {
                return View(_address);
            }
            try
            {
                _db.Add(_address);
                await _db.SaveChangesAsync();
                if(_returnUrl != null && Url.IsLocalUrl(_returnUrl))
                {
                    return Redirect(_returnUrl);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(_address);
            }
        }
        // GET: AddressManagerController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var address = await _db.Address.FirstOrDefaultAsync(a=>a.UserId == userId && a.Id==id );
            if(address == null)
            {
                return NotFound();
            }
            return View(address);
        }

        // POST: AddressManagerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Address _address)
        {
            if (!ModelState.IsValid)
            {
                return View(_address);
            }
            try
            {
                _db.Add(_address);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(_address);
            }
        }

        // POST: AddressManagerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var address = await _db.Address.FirstOrDefaultAsync(a => a.UserId == userId && a.Id == id);
                _db.Remove(address);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
