using Chekich_fx.Data;
using Chekich_fx.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace Chekich_fx.ViewComponents
{
    public class FavViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public FavViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }
        public string Invoke()
        {
            string UserId = HttpContext.Request.Cookies["UserId"];
           
            var count = _db.Favourites.Where(f => f.UserId == UserId).Count();
                
            return count.ToString();
        }

    }
}
