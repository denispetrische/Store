using MediatR;
using Store.Web.Dtos.Product;

namespace Store.Web.Application.Product.Queries
{
    public class GetProductsForMarketQuery : IRequest<List<ProductMarketViewDto>>
    {
    }
}
