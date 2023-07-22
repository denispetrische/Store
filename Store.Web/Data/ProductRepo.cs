using Microsoft.EntityFrameworkCore;
using Store.Web.Abstractions.Data;
using Store.Web.Models;

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
            await _context.Database.ExecuteSqlRawAsync($"CreateProduct '{product.Id}', " +
                                                          $"'{product.Name}', " +
                                                          $"'{product.Description}', " +
                                                          $"'{product.IsOnTrade}', " +
                                                          $"'{product.ReceiptDate.ToString(format)}', " +
                                                          $"'{product.ExpireDate.ToString(format)}', " +
                                                          $"'{product.Amount}', " +
                                                          $"'{product.Price}', " +
                                                          $"'{product.Currency}'");
        }

        public async Task DeleteProductById(string id)
        {
            await _context.Database.ExecuteSqlRawAsync($"DeleteProductById '{id}'");
        }

        public async Task<Product> GetProductById(string id)
        {
            Product product = _context.Products.FromSqlRaw($"GetProductById '{id}'").AsEnumerable().FirstOrDefault();

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
            await _context.Database.ExecuteSqlRawAsync($"UpdateProduct '{product.Id}', " +
                                                          $"'{product.Name}', " +
                                                          $"'{product.Description}', " +
                                                          $"'{product.IsOnTrade}', " +
                                                          $"'{product.ReceiptDate.ToString(format)}', " +
                                                          $"'{product.ExpireDate.ToString(format)}', " +
                                                          $"'{product.Amount}', " +
                                                          $"'{product.Price}', " +
                                                          $"'{product.Currency}'");
        }
    }
}
