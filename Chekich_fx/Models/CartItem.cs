using Chekich_fx.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chekich_fx.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int ShoeSizeInt { get; set; }
        public Shoe Product { get; set; }
        [Required]
        public int Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal SubTotalPrice 
        {
            get
            {
                if (Product != null && Quantity >0)
                {
                    if (Product.DiscountPrice > 0)
                    {
                        return (Quantity * Product.FinalPrice);
                    }
                    else
                    {
                        return (Quantity * Product.Price);

                    }
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
