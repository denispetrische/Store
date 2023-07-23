using MediatR;
using Store.Web.Dtos.Product;

namespace Store.Web.Application.Product.Commands
{
    public class AddProductCommand : IRequest
    {
        public ProductCreateDto ProductDto { get; set; }
    }
}
