using System.ComponentModel.DataAnnotations;
namespace Chekich_fx.Models
{
    public class Transport
    {
        public int Id { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}
