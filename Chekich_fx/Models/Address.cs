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
        [Display(Name ="Street Name*")]
        public string StreetName { get; set; }
        [Required]
        [Display(Name = "House Number*")]
        public string HouseNumber { get; set; }
        [Required]
        [Display(Name = "Surbub*")]
        public string Surbub { get; set; }
        [Required]
        [Display(Name ="City/Town*")]
        public string TownOrCity { get; set; }
        [Required]
        [Display(Name = "Code*")]
        public string Code { get; set; }
        [Required]
        [Display(Name = "Province*")]
        public string Province { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        public AddressType AddressType { get; set; }
        [Required]
        public bool Available { get; set; }
       
    }
}
