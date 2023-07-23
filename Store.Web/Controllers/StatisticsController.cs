using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.Web.Abstractions.Data;
using Store.Web.Dtos.HistoryNote;
using Store.Web.Models;
using MediatR;
using Store.Web.Application.HistoryNote.Queries;

namespace Store.Web.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IMediator _mediator;

        public StatisticsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> StatisticsMainView()
        {
            var notesStetictics = await _mediator.Send(new GetHistoryNotesStatisticsQuery());
            return View(notesStetictics);
        }

        [HttpGet]
        public async Task<IActionResult> ClientDetailedInfoView([FromQuery(Name = "param1")] string? id)
        {
            var notes = await _mediator.Send(new ClientDetailedInfoQuery() { Id = id});
            return View(notes);
        }
    }
}
