using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Web.Abstractions.Data;
using Store.Web.Constants;
using Store.Web.Dtos.Product;
using Store.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace Store.Web.Controllers
{
    [Authorize(Roles ="Manager, Admin")]
    public class StoreController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductRepo _repoProduct;
        private readonly IHistoryNoteRepo _repoHistory;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppConstants _constants;

        public StoreController(IMapper mapper, IProductRepo repoProduct, IHistoryNoteRepo repoHistory, UserManager<IdentityUser> userManager)
        {
            _mapper = mapper;
            _repoProduct = repoProduct;
            _repoHistory = repoHistory;
            _userManager = userManager;
            _constants = new AppConstants();
        }

        public async Task<IActionResult> MainView()
        {
            var products = await _repoProduct.GetProducts();
            return View(_mapper.Map<List<Product>,List<ProductStoreViewDto>>(products));
        }

        [HttpGet]
        public async Task<IActionResult> ChangeIsOnTrade([FromQuery(Name = "param1")] Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction("MainView");
            }

            var product = await _repoProduct.GetProductById(id.Value.ToString());
            if (product == null)
            {
                return RedirectToAction("MainView");
            }

            HistoryNote note;

            if (product.IsOnTrade)
            {
                product.IsOnTrade = !product.IsOnTrade;
                _repoProduct.UpdateProduct(product);

                note = new HistoryNote()
                {
                    Message = $"Продукт {product.Name} перестал продаваться. Пользователь:{User.Identity.Name}",
                    Date = DateTime.Now,
                    UserId = _userManager.GetUserId(HttpContext.User)
                };

                _repoHistory.CreateHistoryNote(note);

                return RedirectToAction("MainView");
            }

            if (DateTime.Now >= product.ExpireDate)
            {
                return RedirectToAction("MainView");
            }

            product.IsOnTrade = !product.IsOnTrade;

            note = new HistoryNote()
            {
                Message = $"Продукт {product.Name} начал продаваться. Пользователь:{User.Identity.Name}",
                Date = DateTime.Now,
                UserId = _userManager.GetUserId(HttpContext.User)
            };

            _repoHistory.CreateHistoryNote(note);
            _repoProduct.UpdateProduct(product);

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

            _repoProduct.CreateProduct(product);

            return RedirectToAction("MainView");
        }
    }
}
