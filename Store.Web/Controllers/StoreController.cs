using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Web.Dtos.Product;
using MediatR;
using Store.Web.Application.Product.Queries;
using Store.Web.Application.Product.Commands;

namespace Store.Web.Controllers
{
    [Authorize(Roles ="Manager, Admin")]
    public class StoreController : Controller
    {
        private readonly IMediator _mediator;

        public StoreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> MainView()
        {
            var products = await _mediator.Send(new GetProductsQuery());
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> ChangeIsOnTrade([FromQuery(Name = "param1")] Guid? id)
        {
            await _mediator.Send(new StoreChangeIsOnTradeCommand() { Id = id, User = User });

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
            await _mediator.Send(new AddProductCommand() { ProductDto = productCreateDto });
            return RedirectToAction("MainView");
        }
    }
}
