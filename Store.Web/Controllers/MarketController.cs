using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.Web.Abstractions.Data;
using Store.Web.Dtos.Product;
using Store.Web.Models;

namespace Store.Web.Controllers
{
    public class MarketController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductRepo _repo;

        public MarketController(IMapper mapper, IProductRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<IActionResult> MainView()
        {
            var products = await _repo.GetProductsForMarket();
            return View(_mapper.Map<List<Product>, List<ProductMarketViewDto>>(products));
        }
    }
}
