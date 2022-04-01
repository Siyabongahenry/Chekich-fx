using Chekich_fx.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Chekich_fx.Models
{
    public abstract class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "DESCRIPTION")]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required]
        [Display(Name ="Discount Price")]
        [DisplayFormat(DataFormatString ="{0:C}",ApplyFormatInEditMode =true)]
        public decimal DiscountPrice { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal FinalPrice
        {
            get { return Price - DiscountPrice; }
        }
        [Display(Name ="Image")]
        [RegularExpression("[^\\s]+(.*?)(\\.(jpe?g|png|JPE?G|PNG))$", ErrorMessage = "This file is not accepted")]
        public string ImageFileName { get; set; }
        [Required]
        public Category Category{ get; set; }
        [Display(Name="Discount Percentage")]
       public string DiscountPercentage
        {
            get
            {
                if (DiscountPrice > 0 && Price > 0)
                {
                    return ((int)((DiscountPrice / Price) * 100)).ToString()+"% off";
                }
                else
                {
                    return "0";
                }
            }
        }
       public string ApplicationUserId { get; set; }
       public ApplicationUser ApplicationUser { get; set; }
       
    }
}
