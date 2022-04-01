
using System;
using System.ComponentModel.DataAnnotations;

namespace Chekich_fx.Models
{
    public abstract class Payment
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime DateTime { get; set; }  
    }
}
