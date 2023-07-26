using Moq;
using Store.Web.Abstractions.Data;
using Store.Web.Constants;
using Store.Web.Data;
using Store.Web.Models;

namespace Store.Tests.Repositories
{
    public class ProductRepoTest
    {
        [Fact]
        public async void GetProductsTest()
        {
            //arrange
            var mock = new Mock<IProductRepo<Product>>();
            mock.Setup(repo => repo.GetProducts()).Returns(GetProducts());

            //act
            var products = await mock.Object.GetProducts();

            //assert
            Assert.Equal(products.Count, GetProducts().Result.Count);
        }

        private async Task<IReadOnlyList<Product>> GetProducts()
        {
            return new List<Product>() 
                { 
                    new Product()
                    {
                        Name = "Banana",
                        Description = "Yellow bananas from Ecuador",
                        ReceiptDate = DateTime.Now,
                        ExpireDate = DateTime.Now.Add(AppConstants._expireTime),
                        Amount = 10,
                        Price = 10,
                        Currency = "BYN"
                    },
                    new Product()
                    {
                        Name = "Computer",
                        Description = "Powerfull and new",
                        ReceiptDate = DateTime.Now,
                        ExpireDate = DateTime.Now.Add(AppConstants._expireTime),
                        Amount = 1,
                        Price = 1000,
                        Currency = "GBP"
                    },
                    new Product()
                    {
                        Name = "Mineral Water",
                        Description = "Made in Georgia",
                        ReceiptDate = DateTime.Now,
                        ExpireDate = DateTime.Now.Add(AppConstants._expireTime),
                        Amount = 256,
                        Price = 5
                    }};
        }
    }
}