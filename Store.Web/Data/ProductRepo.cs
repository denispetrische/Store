using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Store.Web.Abstractions.Data;
using Store.Web.Models;
using System.Xml.Linq;

namespace Store.Web.Data
{
    public class ProductRepo : IProductRepo<Product>
    {
        private readonly StoreWebContext _context;
        private string format = "yyyy-MM-dd HH:mm:ss";

        public ProductRepo(StoreWebContext context)
        {
            _context = context;
        }

        public async Task CreateProduct(Product product)
        {
            var param1 = new SqlParameter("@Id", product.Id);
            var param2 = new SqlParameter("@Name", product.Name);
            var param3 = new SqlParameter("@Description", product.Description);
            var param4 = new SqlParameter("@IsOnTrade", product.IsOnTrade);
            var param5 = new SqlParameter("@ReceiptDate", product.ReceiptDate.ToString(format));
            var param6 = new SqlParameter("@ExpireDate", product.ExpireDate.ToString(format));
            var param7 = new SqlParameter("@Amount", product.Amount);
            var param8 = new SqlParameter("@Price", product.Price);
            var param9 = new SqlParameter("@Currency", product.Currency);

            await _context.Database.ExecuteSqlRawAsync($"CreateProduct @Id, " +
                                                                     $"@Name, " +
                                                                     $"@Description, " +
                                                                     $"@IsOnTrade, " +
                                                                     $"@ReceiptDate, " +
                                                                     $"@ExpireDate, " +
                                                                     $"@Amount, " +
                                                                     $"@Price, " +
                                                                     $"@Currency",
                                                                     param1,
                                                                     param2,
                                                                     param3,
                                                                     param4,
                                                                     param5,
                                                                     param6,
                                                                     param7,
                                                                     param8,
                                                                     param9);
        }

        public async Task DeleteProductById(string id)
        {
            var param1 = new SqlParameter("@Id", id);
            await _context.Database.ExecuteSqlRawAsync($"DeleteProductById @Id", param1);
        }

        public async Task<Product> GetProductById(string id)
        {
            var param1 = new SqlParameter("@Id", id);
            Product product = _context.Products.FromSqlRaw($"GetProductById @Id", param1).AsEnumerable().FirstOrDefault();

            return product;
        }

        public async Task<IReadOnlyList<Product>> GetProducts()
        {
            var products = _context.Products.FromSqlRaw("GetProducts").ToList();

            return products;
        }

        public async Task<IReadOnlyList<Product>> GetProductsForMarket()
        {
            var products = _context.Products.FromSqlRaw("GetProductsForMarket").ToList();

            return products;
        }

        public async Task UpdateProduct(Product product)
        {
            var param1 = new SqlParameter("@Id", product.Id);
            var param2 = new SqlParameter("@Name", product.Name);
            var param3 = new SqlParameter("@Description", product.Description);
            var param4 = new SqlParameter("@IsOnTrade", product.IsOnTrade);
            var param5 = new SqlParameter("@ReceiptDate", product.ReceiptDate.ToString(format));
            var param6 = new SqlParameter("@ExpireDate", product.ExpireDate.ToString(format));
            var param7 = new SqlParameter("@Amount", product.Amount);
            var param8 = new SqlParameter("@Price", product.Price);
            var param9 = new SqlParameter("@Currency", product.Currency);

            await _context.Database.ExecuteSqlRawAsync($"UpdateProduct @Id, " +
                                                                     $"@Name, " +
                                                                     $"@Description, " +
                                                                     $"@IsOnTrade, " +
                                                                     $"@ReceiptDate, " +
                                                                     $"@ExpireDate, " +
                                                                     $"@Amount, " +
                                                                     $"@Price, " +
                                                                     $"@Currency",
                                                                     param1,
                                                                     param2,
                                                                     param3,
                                                                     param4,
                                                                     param5,
                                                                     param6,
                                                                     param7,
                                                                     param8,
                                                                     param9);
        }
    }
}
