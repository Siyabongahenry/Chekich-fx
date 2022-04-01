using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Chekich_fx.Models
{
    public enum AddressType{Collection,Delivery}
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [Required]
        [Display(Name ="Address")]
        public string Location { get; set; }
        [Required]
        [Display(Name ="City/Town")]
        public string TownOrCity { get; set; }
        [Required]
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public AddressType AddressType { get; set; }
        [Required]
        public bool Available { get; set; }
       
    }
}
