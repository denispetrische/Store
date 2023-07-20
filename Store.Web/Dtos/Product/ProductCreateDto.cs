using System.ComponentModel.DataAnnotations;

namespace Store.Web.Dtos.Product
{
    public class ProductCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Int64 Amount { get; set; }
        [Required]
        public Int64 Price { get; set; }
        [Required]
        public string Currency { get; set; }
    }
}
