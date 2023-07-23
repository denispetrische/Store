using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Store.Web.Abstractions.Data;
using Store.Web.Dtos.Product;

namespace Store.Web.Application.Product.Queries
{
    public class GetProductsForMarketQueryHandler : IRequestHandler<GetProductsForMarketQuery, List<ProductMarketViewDto>>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepo<Models.Product> _productRepo;
        private readonly ILogger<GetProductsForMarketQueryHandler> _logger;

        public GetProductsForMarketQueryHandler(IMapper mapper,
                                                IProductRepo<Models.Product> repo,
                                                UserManager<IdentityUser> userManager,
                                                IHistoryNoteRepo<Models.HistoryNote> historyNoteRepo,
                                                ILogger<GetProductsForMarketQueryHandler> logger)
        {
            _mapper = mapper;
            _productRepo = repo;
            _logger = logger;
        }

        public async Task<List<ProductMarketViewDto>> Handle(GetProductsForMarketQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = _productRepo.GetProductsForMarket().Result.ToList();
                _logger.LogInformation("MainView: products successfully received");

                return _mapper.Map<List<Models.Product>, List<ProductMarketViewDto>>(products);
            }
            catch (Exception e)
            {
                _logger.LogError($"MainView: cannot get products. Reason: {e.Message}");
                return null;
            }
        }
    }
}
