using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chekich_fx.Models
{
    public class CartViewModel
    {
        public List<CartItem> CartList { get; set; }
        [DataType(DataType.Currency)]
        public decimal TotalPrice
        {
            get
            {
                return CartList.Sum(c => c.SubTotalPrice);
            }
        }
        public int Items
        {
            get
            {
                return CartList.Sum(c => c.Quantity);
            }
        }
    }
}
