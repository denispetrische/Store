using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.Web.Abstractions.Data;
using Store.Web.Dtos.Product;
using Store.Web.Models;

namespace Store.Web.Controllers
{
    public class MarketController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductRepo _productRepo;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHistoryNoteRepo _historyNoteRepo;

        public MarketController(IMapper mapper, IProductRepo repo, UserManager<IdentityUser> userManager, IHistoryNoteRepo historyNoteRepo)
        {
            _mapper = mapper;
            _productRepo = repo;
            _userManager = userManager;
            _historyNoteRepo = historyNoteRepo;
        }

        public async Task<IActionResult> MainView()
        {
            var products = await _productRepo.GetProductsForMarket();
            return View(_mapper.Map<List<Product>, List<ProductMarketViewDto>>(products));
        }

        [HttpGet]
        public async Task<IActionResult> BuyProduct([FromQuery(Name = "param1")] Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction("MainView");
            }

            var product = _productRepo.GetProductById(id.Value.ToString()).Result;

            if (product == null)
            {
                return RedirectToAction("MainView");
            }

            if (product.Amount > 0)
            {
                product.Amount = product.Amount - 1;

                HistoryNote note = new HistoryNote()
                {
                    Message = $"Продукт {product.Name} был куплен. Пользователь:{User.Identity.Name}",
                    Date = DateTime.Now,
                    UserId = _userManager.GetUserId(HttpContext.User)
                };

                _historyNoteRepo.CreateHistoryNote(note);
                _productRepo.UpdateProduct(product);
            }

            return RedirectToAction("MainView");
        }
    }
}
