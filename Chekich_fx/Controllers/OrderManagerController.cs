﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Chekich_fx.Data;
using Chekich_fx.Enums;
using Chekich_fx.Models;
using Chekich_fx.Utility;
using Chekich_fx.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chekich_fx.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class OrderManagerController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public OrderManagerController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {   
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var address = await _db.Address.FirstOrDefaultAsync(a=>a.UserId==userId);
            if (address == null)
            {
                return RedirectToAction("AddressManager","Index");
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Confirm(ReceivalType _receivalType)
        { 
            var transportCost = 0;
            if (_receivalType == ReceivalType.Delivery)
            {
                transportCost = 0;
            }
            else
            {
                transportCost = 0;
            }
            string userId = HttpContext.Request.Cookies["UserId"];

            var cart = await _db.Cart
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            CartReceivalViewModel CRVM = new CartReceivalViewModel()
            {
                Cart = cart,
                ReceivalCost = transportCost,
                ReceivalType = _receivalType
            };

            return View(CRVM);
        }
        [HttpPost]
        public async Task<IActionResult> Submit(ReceivalType _receivalType)
        {
            var transportCost = 0;
            if (_receivalType == ReceivalType.Delivery)
            {
                transportCost = 0;
            }
            else
            {
                transportCost = 0;
            }
            
            string userId = HttpContext.Request.Cookies["UserId"];

            var cart = await _db.Cart
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Order order = new Order()
            {
                DateTime = DateTime.Now,
                ReceivalCost = transportCost,
                UserId = userId
            };
            order.OrderItems = new List<OrderItem>();
            foreach (var item in cart.CartItems)
            {
                order.OrderItems.Add(new OrderItem()
                {
                    Price = item.SubTotalPrice,
                    Quantity = item.Quantity,
                    ShoeSizeInt = item.ShoeSizeInt,
                    Order = order,
                    Product = item.Product
                });
                cart.CartItems.Remove(item);

            }
            _db.Add(order);
            await _db.SaveChangesAsync();
            if (_receivalType == ReceivalType.Delivery)
            {
                return RedirectToAction(nameof(Delivery));
            }
            return RedirectToAction(nameof(Collection));
        }
      
        public async Task<IActionResult> Delivery()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var addresses = await _db.Address
                   .AsNoTracking()
                   .Where(a => a.AddressType == AddressType.Delivery && a.UserId == userId)
                   .ToListAsync();
            return View(addresses);
        }
        [HttpPost]
        public async Task<IActionResult> Delivery(int _addressId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _db.Order.FirstOrDefaultAsync(c => c.UserId == userId);
            Delivery delivery = new Delivery()
            {
                OrderId = order.Id,
                AddressId = _addressId,
                DateTime = DateTime.Now.AddDays(5)
            };
            _db.Add(delivery);
            await _db.SaveChangesAsync();
            return View();
        }
        public async Task<IActionResult> Collection()
        {
            var addresses = await _db.Address
                   .AsNoTracking()
                   .Where(a => a.AddressType == AddressType.Collection)
                   .ToListAsync();
            return View(addresses);
        }
        [HttpPost]
        public async Task<IActionResult> Collection(int _addressId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var order = await _db.Order.FirstOrDefaultAsync(c => c.UserId == userId);
            Collection collection = new Collection()
            {
                OrderId = order.Id,
                AddressId = _addressId,
                DateTime = DateTime.Now.AddDays(5)
            };
            _db.Add(collection);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index","Payment");
        }
        
        [HttpGet]
        public async Task<IActionResult> Status()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _db.Order
                .Include(o=>o.OrderItems)
                .ThenInclude(oi=>oi.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o=>o.DateTime)
                .ToListAsync();
            return View(orders);
        }
        [HttpGet]
        public async Task<string> GetAddress(AddressType addressType)
        {
            var ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var addresses = new List<Address>();
            if (addressType == AddressType.Collection)
            {
                addresses = await _db
                    .Address
                    .AsNoTracking()
                    .Where(a => a.AddressType == AddressType.Collection && a.Available == true)
                    .ToListAsync();
            }
            if (addressType == AddressType.Delivery)
            {
                
                addresses = await _db.Address
                    .AsNoTracking()
                    .Where(a => a.AddressType == AddressType.Delivery && a.UserId == ApplicationUserId)
                    .ToListAsync();
            }
            if(addresses.Count == 0)
            {
                return "null";
            }
            return JsonSerializer.Serialize(addresses);
        }
      
       
    }
}
