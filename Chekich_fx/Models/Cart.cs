using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Chekich_fx.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal TotalPrice
        {
            get
            {
                if (CartItems == null)
                {
                    return 0;
                }
                else
                {
                    return CartItems.Sum(c => c.SubTotalPrice);
                }     
            }
        }
        public int Items
        {
            get
            {
                if(CartItems ==null)
                {
                    return 0;
                }
                else
                {
                    return CartItems.Sum(c => c.Quantity);
                }       
            }
        }
    }
}
