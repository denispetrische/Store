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
        private readonly ILogger<MarketController> _logger;

        public MarketController(IMapper mapper, 
                                IProductRepo repo, 
                                UserManager<IdentityUser> userManager, 
                                IHistoryNoteRepo historyNoteRepo,
                                ILogger<MarketController> logger)
        {
            _mapper = mapper;
            _productRepo = repo;
            _userManager = userManager;
            _historyNoteRepo = historyNoteRepo;
            _logger = logger;
        }

        public async Task<IActionResult> MainView()
        {
            List<Product> products = null;

            try
            {
                products = _productRepo.GetProductsForMarket().Result.ToList();
                _logger.LogInformation("MainView: products successfully received");
            }
            catch (Exception e)
            {
                _logger.LogError($"MainView: cannot get products. Reason: {e.Message}");
            }
            
            return View(_mapper.Map<List<Product>, List<ProductMarketViewDto>>(products));
        }

        [HttpGet]
        public async Task<IActionResult> BuyProduct([FromQuery(Name = "param1")] Guid? id)
        {
            if (id == null)
            {
                return RedirectToAction("MainView");
            }

            Product product = null;

            try
            {
                product = _productRepo.GetProductById(id.Value.ToString()).Result;
                _logger.LogInformation("MainView: product successfully received");
            }
            catch (Exception e)
            {
                _logger.LogError($"MainView: cannot get product. Reason: {e.Message}");
            }

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

                try
                {
                    _historyNoteRepo.CreateHistoryNote(note);
                    _productRepo.UpdateProduct(product);
                    _logger.LogInformation("MainView: product is successfully bought");
                }
                catch (Exception e)
                {
                    _logger.LogError($"MainView: cannot buy product or note that. Reason: {e.Message}");
                }
            }

            return RedirectToAction("MainView");
        }
    }
}
