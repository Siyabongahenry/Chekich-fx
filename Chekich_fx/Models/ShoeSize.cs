using System.ComponentModel.DataAnnotations;

namespace Chekich_fx.Models
{
    public class ShoeSize
    {
        public int Id { get; set; }
        [Required]
        public int Size { get; set; }
        public int ShoeId { get; set; }
        public Shoe Shoe { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
