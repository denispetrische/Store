using Store.Web.Models;

namespace Store.Web.Abstractions.Data
{
    public interface IProductRepo
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProductById(string id);
        Task CreateProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProductById(string id);
    }
}
