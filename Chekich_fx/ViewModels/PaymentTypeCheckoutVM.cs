using Chekich_fx.Enums;
using Chekich_fx.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chekich_fx.ViewModels
{
    public class PaymentTypeCheckoutVM
    {
        public PaymentType PaymentType { get; set; }
        public Order Order { get; set; }
    }
}
