namespace Store.Web.Dtos.Product
{
    public class ProductMarketViewDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Int64 Amount { get; set; }
        public Int64 Price { get; set; }
        public string Currency { get; set; }
    }
}
