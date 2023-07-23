using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.Web.Application.Product.Commands;
using Store.Web.Application.Product.Queries;

namespace Store.Web.Controllers
{
    public class MarketController : Controller
    {
        private readonly IMediator _mediator;

        public MarketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> MainView()
        {
            var products = await _mediator.Send(new GetProductsForMarketQuery());
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> BuyProduct([FromQuery(Name = "param1")] Guid? id)
        {
            await _mediator.Send(new BuyProductCommand() { Id = id, User = User});

            return RedirectToAction("MainView");
        }
    }
}
