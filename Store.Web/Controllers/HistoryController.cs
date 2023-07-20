using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.Web.Abstractions.Data;
using Store.Web.Dtos.HistoryNote;
using Store.Web.Models;

namespace Store.Web.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IHistoryNoteRepo _repo;
        private readonly IMapper _mapper;

        public HistoryController(IHistoryNoteRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public IActionResult HistoryMainView()
        {
            var notes = _repo.GetHistoryNotes().Result;
            return View(_mapper.Map<List<HistoryNote>,List<HistoryNoteHistoryViewDto>>(notes));
        }
    }
}
