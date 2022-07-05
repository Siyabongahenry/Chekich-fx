using System.ComponentModel.DataAnnotations;

namespace Chekich_fx.Models
{
    public class Favourite
    {
        [Key]
        public int Id { get; set; }
        public int ShoeId { get; set; }
        public Shoe Shoe { get; set; }
        public string UserId { get; set; }

    }
}
