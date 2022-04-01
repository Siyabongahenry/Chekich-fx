using System;
using System.ComponentModel.DataAnnotations;

namespace Chekich_fx.Models
{
    public class Collection
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public DateTime DateTime { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
       
    }
}
