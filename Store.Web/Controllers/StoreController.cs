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
        private readonly ILogger<StoreController> _logger;

        public StoreController(IMapper mapper, 
                               IProductRepo repoProduct, 
                               IHistoryNoteRepo repoHistory, 
                               UserManager<IdentityUser> userManager,
                               ILogger<StoreController> logger)
        {
            _mapper = mapper;
            _repoProduct = repoProduct;
            _repoHistory = repoHistory;
            _userManager = userManager;
            _constants = new AppConstants();
            _logger = logger;
        }

        public async Task<IActionResult> MainView()
        {
            List<Product> products = null;

            try
            {
                products = _repoProduct.GetProducts().Result.OrderByDescending(p => p.ReceiptDate).ToList();

                _logger.LogInformation($"Products successfully gotten");

            }
            catch (Exception e)
            {
                _logger.LogError($"Can't get products from products repo. Reason: {e.Message}");
            }

            return View(_mapper.Map<List<Product>,List<ProductStoreViewDto>>(products));
        }

        [HttpGet]
        public async Task<IActionResult> ChangeIsOnTrade([FromQuery(Name = "param1")] Guid? id)
        {
            if (id == null)
            {
                _logger.LogInformation("ChangeIsOnTrade: id == null");
                return RedirectToAction("MainView");
            }

            Product product = null;

            try
            {
                product = await _repoProduct.GetProductById(id.Value.ToString());
                _logger.LogInformation("ChangeIsOnTrade: product was received");
            }
            catch (Exception e)
            {
                _logger.LogError($"ChangeIsOnTrade: product was not received. Reason: {e.Message}");
            }
            
            if (product == null)
            {
                return RedirectToAction("MainView");
            }

            HistoryNote note;

            if (product.IsOnTrade)
            {
                product.IsOnTrade = !product.IsOnTrade;

                try
                {
                    _repoProduct.UpdateProduct(product);
                    _logger.LogInformation($"ChangeIsOnTrade: product was successfully updated");
                }
                catch (Exception e)
                {
                    _logger.LogError($"ChangeIsOnTrade: unable to update product, Reason: {e.Message}");
                }

                note = new HistoryNote()
                {
                    Message = $"Продукт {product.Name} перестал продаваться. Пользователь:{User.Identity.Name}",
                    Date = DateTime.Now,
                    UserId = _userManager.GetUserId(HttpContext.User)
                };

                try
                {
                    _repoHistory.CreateHistoryNote(note);
                    _logger.LogInformation($"ChangeIsOnTrade: note was succesfully created");
                }
                catch (Exception e)
                {
                    _logger.LogError($"ChangeIsOnTrade: unable to create note, Reason: {e.Message}");
                }                

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

            try
            {
                _repoHistory.CreateHistoryNote(note);
                _repoProduct.UpdateProduct(product);
                _logger.LogInformation($"ChangeIsOnTrade: product was successfulyy updated and note was succesfully created");
            }
            catch (Exception e)
            {
                _logger.LogError($"ChangeIsOnTrade: unable to create note or update product, Reason: {e.Message}");
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
            var product = _mapper.Map<Product>(productCreateDto);
            product.ReceiptDate = DateTime.Now;
            product.ExpireDate = DateTime.Now.Add(_constants._expireTime);

            try
            {
                _repoProduct.CreateProduct(product);
                _logger.LogInformation("AddProduct: product was succesfully created");

            }
            catch (Exception e)
            {
                _logger.LogError($"AddProduct: unable to create product. Reason: {e.Message}");
            }

            return RedirectToAction("MainView");
        }
    }
}
