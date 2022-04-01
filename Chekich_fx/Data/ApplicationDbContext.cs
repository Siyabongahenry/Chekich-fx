using System;
using System.Collections.Generic;
using System.Text;
using Chekich_fx.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chekich_fx.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Shoe> Shoe{ get; set; }
        public DbSet<ShoeSize> ShoeSizes { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<SenderReceiverInfo> SenderReceiverInfo { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OnlinePayment> OnlinePayments { get; set; }
        public DbSet<CashPayment> CashPayments { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Collection> Collections { get; set; }
      
    }
}
