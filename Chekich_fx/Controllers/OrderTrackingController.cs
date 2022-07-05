using Chekich_fx.Data;
using Chekich_fx.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Chekich_fx.Controllers
{
    public class OrderTrackingController : Controller
    {
        private readonly ApplicationDbContext _db;
        public OrderTrackingController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _db.Order
                .AsNoTracking()
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.DateTime)
                .ToListAsync();
            return View(orders);
        }
        [HttpGet]
        public async Task<string> Delivery(int _orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orderExist = await _db.Order.AnyAsync(o => o.UserId == userId && o.Id == _orderId);
            if (!orderExist) return "OrderNotFound";

            var delivery = await _db.Deliveries
                    .AsNoTracking()
                    .Include(d => d.Address)
                    .FirstOrDefaultAsync(d => d.OrderId == _orderId);
                
            if (delivery == null) return "DeliveryNotFound";
            
             return JsonSerializer.Serialize(delivery);   
           
        }
        [HttpGet]
        public async Task<string> Collection(int _orderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orderExist = await _db.Order.AnyAsync(o => o.UserId == userId && o.Id == _orderId);
            if (orderExist)
            {
                var collection = await _db.Collections
                    .AsNoTracking()
                    .Include(c => c.Address)
                    .FirstOrDefaultAsync(c => c.OrderId == _orderId);
                if (collection != null)
                {
                    return JsonSerializer.Serialize(collection);
                }
                return "CollectionNotFound";
            }
            return "OrderNotFound";
        }
        [HttpGet]
        public async Task<string> Payment(int _orderId,PaymentType _paymentType)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orderExist = await _db.Order.AnyAsync(o => o.UserId == userId && o.Id == _orderId);
            if (orderExist)
            {
                if(_paymentType == PaymentType.Cash)
                {
                    var cPayment = await _db.CashPayments.FirstOrDefaultAsync(cp => cp.OrderId == _orderId);
                    return JsonSerializer.Serialize(cPayment);
                }
                var oPayment = await _db.OnlinePayments.FirstOrDefaultAsync(op => op.OrderId == _orderId);
                return JsonSerializer.Serialize(oPayment);
            }
            return "OrderNotFound";
        }
    }
}
