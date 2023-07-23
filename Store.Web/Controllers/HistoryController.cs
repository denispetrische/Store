using MediatR;
using Microsoft.AspNetCore.Mvc;
using Store.Web.Application.HistoryNote.Queries;

namespace Store.Web.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IMediator _mediator;

        public HistoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> HistoryMainView()
        {
            var notes = await _mediator.Send(new GetHistoryNotesQuery());
            return View(notes);
        }
    }
}
