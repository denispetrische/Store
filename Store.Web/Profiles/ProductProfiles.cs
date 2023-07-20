using AutoMapper;
using Store.Web.Dtos.Product;
using Store.Web.Models;

namespace Store.Web.Profiles
{
    public class ProductProfiles : Profile
    {
        public ProductProfiles()
        {
            CreateMap<Product, ProductStoreViewDto>();
        }
    }
}
