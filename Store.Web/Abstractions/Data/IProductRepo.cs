using Store.Web.Models;

namespace Store.Web.Abstractions.Data
{
    public interface IProductRepo<T> where T : Product
    {
        Task<IReadOnlyList<T>> GetProducts();
        Task<IReadOnlyList<T>> GetProductsForMarket();
        Task<T> GetProductById(string id);
        Task CreateProduct(T product);
        Task UpdateProduct(T product);
        Task DeleteProductById(string id);
    }
}
