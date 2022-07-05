using Chekich_fx.Data;
using Chekich_fx.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Chekich_fx.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class FavouriteController : Controller
    {
        private readonly ApplicationDbContext _db;
        public FavouriteController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var favouriteList = await _db.Favourites.Where(f => f.UserId ==GetUserId() ).Include(f=>f.Shoe).ToListAsync();
            return View(favouriteList);
        }
        [HttpPost]
        public async Task<string> Add(int ShoeId)
        {
            var userId = GetUserId();
            var shoe = await _db.Shoe.AsNoTracking().FirstOrDefaultAsync(s => s.Id == ShoeId);

            if (shoe == null) return "failed";

            var findFav = await _db.Favourites.FirstOrDefaultAsync(f => f.UserId == userId && f.ShoeId == ShoeId);

            if (findFav != null) return "failed";

            Favourite fav = new Favourite()
            {
                UserId = userId,
                ShoeId = ShoeId
            };

            _db.Add(fav);
            await _db.SaveChangesAsync();
            return "success";
        }
        [HttpPost]
        public async Task<string> Remove(int favId)
        {
            var fav = await _db.Favourites.FirstOrDefaultAsync(f=>f.Id == favId);
            if(fav == null)  return "failed";
            _db.Remove(fav);
            await _db.SaveChangesAsync();
            return "success";
        }
        public string GetUserId()
        {

            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddMonths(12));
            bool IsFound = HttpContext.Request.Cookies.ContainsKey("UserId");
            if (!IsFound)
            {
                Guid Id = Guid.NewGuid();
                HttpContext.Response.Cookies.Append("UserId", Id.ToString(), cookieOptions);
                return Id.ToString();
            }
            else
            {
                string Id = Request.Cookies["UserId"];
                return Id;
            }
        }
    }
}
