using Microsoft.EntityFrameworkCore;
using Store.Web.Abstractions.Data;
using Store.Web.Models;

namespace Store.Web.Data
{
    public class ProductRepo : IProductRepo
    {
        private readonly StoreWebContext _context;

        public ProductRepo(StoreWebContext context)
        {
            _context = context;
        }

        public Task CreateProduct(Product product)
        {
            string format = "yyyy-MM-dd HH:mm:ss";
            _context.Database.ExecuteSqlRaw($"CreateProduct '{product.Id}', " +
                                                          $"'{product.Name}', " +
                                                          $"'{product.Description}', " +
                                                          $"'{product.IsOnTrade}', " +
                                                          $"'{product.ReceiptDate.ToString(format)}', " +
                                                          $"'{product.ExpireDate.ToString(format)}', " +
                                                          $"'{product.Amount}', " +
                                                          $"'{product.Price}', " +
                                                          $"'{product.Currency}'");

            return Task.CompletedTask;
        }

        public Task DeleteProductById(string id)
        {
            _context.Database.ExecuteSqlRawAsync($"DeleteProductById {id}");

            return Task.CompletedTask;
        }

        public Task<Product> GetProductById(string id)
        {
            Product product = _context.Products.FromSqlRaw($"GetProductById {id}").FirstOrDefault();

            return Task.FromResult(product);
        }

        public Task<List<Product>> GetProducts()
        {
            var products = _context.Products.FromSqlRaw("GetProducts").ToList();

            return Task.FromResult(products);
        }

        public Task UpdateProduct(Product product)
        {
            _context.Database.ExecuteSqlRaw($"UpdateProduct {product.Id}, " +
                                                          $"{product.Name}, " +
                                                          $"{product.Description}, " +
                                                          $"{product.IsOnTrade}, " +
                                                          $"{product.ReceiptDate}, " +
                                                          $"{product.ExpireDate}, " +
                                                          $"{product.Amount}, " +
                                                          $"{product.Price}, " +
                                                          $"{product.Currency}");

            return Task.CompletedTask;
        }
    }
}
