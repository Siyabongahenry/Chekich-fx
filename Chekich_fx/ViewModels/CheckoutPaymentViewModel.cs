using Chekich_fx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chekich_fx.ViewModels
{
    public class CheckoutPaymentViewModel
    {
        public IList<Order> Checkout { get; set; }
        public IList<Payment> Payment { get; set; }
    }
}
