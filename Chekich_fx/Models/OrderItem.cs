using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Chekich_fx.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public Product Product { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }
        public Order Order { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ShoeSizeInt { get; set; }
    }
}
