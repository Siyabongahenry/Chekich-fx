
using Chekich_fx.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace Chekich_fx.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal ReceivalCost { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal TotalPrice
        {
            get
            {
                if (OrderItems != null)
                {
                    return OrderItems.Sum(o => o.Price) + ReceivalCost;
                }
                else
                {
                    return 0;
                }
            }
        }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        public string Status { get; set; }
        public PaymentType? PaymentType { get; set; }
        public ReceivalType? ReceivalType { get; set; }
       
    }
}
