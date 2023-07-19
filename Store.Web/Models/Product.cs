using System.ComponentModel.DataAnnotations;

namespace Store.Web.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsOnTrade { get; set; } = false;
        public DateTime ReceiptDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public Int64 Amount { get; set; }
        public Int64 Price { get; set; }
        public string Currency { get; set; } = "USD";


    }
}
