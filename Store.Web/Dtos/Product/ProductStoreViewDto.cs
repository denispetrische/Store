namespace Store.Web.Dtos.Product
{
    public class ProductStoreViewDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsOnTrade { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public Int64 Amount { get; set; }
        public Int64 Price { get; set; }
        public string Currency { get; set; }
    }
}
