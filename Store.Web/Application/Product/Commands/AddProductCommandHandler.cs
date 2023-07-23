﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NuGet.Protocol.Plugins;
using Store.Web.Abstractions.Data;
using Store.Web.Constants;
using Store.Web.Controllers;
using Store.Web.Dtos.Product;
using Store.Web.Models;

namespace Store.Web.Application.Product.Commands
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand>
    {
        private readonly IMapper _mapper;
        private readonly IProductRepo<Models.Product> _repoProduct;
        private readonly ILogger<StoreController> _logger;
        private readonly AppConstants _constants;

        public AddProductCommandHandler(IMapper mapper,
                                        IProductRepo<Models.Product> repoProduct,
                                        ILogger<StoreController> logger)
        {
            _mapper = mapper;
            _repoProduct = repoProduct;
            _logger = logger;
            _constants = new AppConstants();

        }

        public async Task Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Models.Product>(request.ProductDto);
            product.ReceiptDate = DateTime.Now;
            product.ExpireDate = DateTime.Now.Add(_constants._expireTime);

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
