using Chekich_fx.Enums;
using Chekich_fx.Models;
using System.ComponentModel.DataAnnotations;

namespace Chekich_fx.ViewModels
{
    public class CartReceivalViewModel
    {
        public Cart Cart { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal ReceivalCost { get; set; }
        public ReceivalType ReceivalType { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal TotalCost { get
            {
                return Cart.TotalPrice + ReceivalCost;
            } 
        }
    }
}
