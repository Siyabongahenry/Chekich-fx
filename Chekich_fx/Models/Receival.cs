using System;
using System.ComponentModel.DataAnnotations;
namespace Chekich_fx.Models
{
    public abstract class Receival
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
