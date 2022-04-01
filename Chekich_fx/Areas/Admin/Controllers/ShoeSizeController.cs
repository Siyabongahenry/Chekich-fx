using Chekich_fx.Data;
using Chekich_fx.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Chekich_fx.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "writepolicy")]
    [AutoValidateAntiforgeryToken]
    public class ShoeSizeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ShoeSizeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ActionResult> Create(int shoeId)
        {
            try
            {
                var shoe = await _db.Shoe
                    .AsNoTracking()
                    .Include(s=>s.Sizes)
                    .FirstOrDefaultAsync(s => s.Id == shoeId);
                if(shoe == null)
                {
                    return NotFound();
                }
                return View(shoe);
            }
            catch
            {
                return NotFound();
            }
        }

        // POST: ShoeSize/Create
        [HttpPost]
      
        public async Task<string> Create(ShoeSize shoeSize)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var size =await _db.ShoeSizes.FirstOrDefaultAsync(s=>s.Size==shoeSize.Size && s.ShoeId==shoeSize.ShoeId);
                    if (size == null)
                    {
                        _db.Add(shoeSize);
                        await _db.SaveChangesAsync();
                        return "Inserted";
                    }
                    else
                    {
                        size.Quantity = shoeSize.Quantity;
                        await _db.SaveChangesAsync();
                        return "Updated";
                    }
                }
                catch
                {
                    return "Error";
                }
            }
            else
            {
                return "Rejected";
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<string> Remove(ShoeSize shoeSize)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var size = await _db.ShoeSizes.FirstOrDefaultAsync(s => s.ShoeId == shoeSize.ShoeId && s.Size == shoeSize.Size);
                    if (size == null)
                    {
                        return "NotFound";
                    }
                    _db.Remove(size);
                    await _db.SaveChangesAsync();
                    return "Removed";
                }
                catch
                {
                    return "Error";
                }
            }
            else
            {
                return "Rejected";
            }
        }
    }
}
