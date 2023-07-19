using Store.Web.Abstractions.Data;
using Store.Web.Models;

namespace Store.Web.Data
{
    public class ProductRepo : IProductRepo
    {
        public Task CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetProducts()
        {
            throw new NotImplementedException();
        }

        public Task UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
