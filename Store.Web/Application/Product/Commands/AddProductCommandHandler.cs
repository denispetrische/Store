using AutoMapper;
using MediatR;
using Store.Web.Abstractions.Data;
using Store.Web.Constants;

namespace Store.Web.Application.Product.Commands
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepo<Models.Product> _repoProduct;
        private readonly ILogger<AddProductCommandHandler> _logger;

        public AddProductCommandHandler(IMapper mapper,
                                        IProductRepo<Models.Product> repoProduct,
                                        ILogger<AddProductCommandHandler> logger)
        {
            _mapper = mapper;
            _repoProduct = repoProduct;
            _logger = logger;
        }

        public async Task Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Models.Product>(request.ProductDto);
            product.ReceiptDate = DateTime.Now;
            product.ExpireDate = DateTime.Now.Add(AppConstants._expireTime);

            try
            {
                await _repoProduct.CreateProduct(product);
                _logger.LogInformation("AddProduct: product was succesfully created");

            }
            catch (Exception e)
            {
                _logger.LogError($"AddProduct: unable to create product. Reason: {e.Message}");
            }
        }
    }
}
