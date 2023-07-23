using AutoMapper;
using MediatR;
using Store.Web.Abstractions.Data;
using Store.Web.Dtos.Product;
using Store.Web.Models;

namespace Store.Web.Application.Product.Queries
{
    public class GetProductsCommandHandler : IRequestHandler<GetProductsQuery, List<ProductStoreViewDto>>
    {
        private readonly IProductRepo<Models.Product> _productRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<GetProductsCommandHandler> _logger;

        public GetProductsCommandHandler(IProductRepo<Store.Web.Models.Product> productRepo, IMapper mapper, ILogger<GetProductsCommandHandler> logger)
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<ProductStoreViewDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _productRepo.GetProducts();
                var mappedProducts = _mapper.Map<List<Models.Product>, List<ProductStoreViewDto>>(products.ToList());
                _logger.LogInformation($"Products successfully gotten");

                return mappedProducts;

            }
            catch (Exception e)
            {
                _logger.LogError($"Can't get products from products repo. Reason: {e.Message}");
                return null;
            }
        }
    }
}
