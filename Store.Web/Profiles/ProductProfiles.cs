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
            CreateMap<ProductCreateDto, Product>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency));
        }
    }
}
