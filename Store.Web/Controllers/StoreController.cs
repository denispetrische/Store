using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Web.Abstractions.Data;
using Store.Web.Constants;
using Store.Web.Dtos.Product;
using Store.Web.Models;

namespace Store.Web.Controllers
{
    [Authorize(Roles ="Manager, Admin")]
    public class StoreController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductRepo _repo;
        private readonly AppConstants _constants;

        public StoreController(IMapper mapper, IProductRepo repo)
        {
            _mapper = mapper;
            _repo = repo;
            _constants = new AppConstants();
        }

        public async Task<IActionResult> MainView()
        {
            var products = await _repo.GetProducts();
            return View(_mapper.Map<List<Product>,List<ProductStoreViewDto>>(products));
        }

        [HttpGet]
        public async Task<IActionResult> ChangeIsOnTrade([FromQuery(Name = "param1")] Guid? id)
        {
            if (id != null)
            {
                var product = await _repo.GetProductById(id.Value.ToString());
                Console.WriteLine("Here2");
                product.IsOnTrade = !product.IsOnTrade;
                Console.WriteLine("Here3");
                _repo.UpdateProduct(product);
            }

            return RedirectToAction("MainView");
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] ProductCreateDto productCreateDto)
        {
            Console.WriteLine("Here!!!");
            var product = _mapper.Map<Product>(productCreateDto);
            product.ReceiptDate = DateTime.Now;
            product.ExpireDate = DateTime.Now.Add(_constants._expireTime);

            _repo.CreateProduct(product);

            return RedirectToAction("MainView");
        }
    }
}
