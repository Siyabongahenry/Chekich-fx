using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Chekich_fx.Models
{
    public class Shoe:Product
    {
        public ICollection<ShoeSize> Sizes { get; set; }
        public int Quantity
        {
            get
            {
                if (Sizes != null)
                {
                    return Sizes.Sum(c => c.Quantity);
                }
                else
                {
                    return 0;
                }
            }
        }
        
    }
}
