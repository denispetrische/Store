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
        private readonly ILogger<StoreController> _logger;

        public HistoryController(IHistoryNoteRepo repo, 
                                 IMapper mapper,
                                 ILogger<StoreController> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public IActionResult HistoryMainView()
        {
            List<HistoryNote> notes = null;
            
            try
            {
                notes = _repo.GetHistoryNotes().Result;
                _logger.LogInformation("HistoryMainView: notes was successfully received");
            }
            catch (Exception e)
            {
                _logger.LogError($"HistoryMainView: unable to get notes. Reason: {e.Message}");
            }
            
            return View(_mapper.Map<List<HistoryNote>,List<HistoryNoteHistoryViewDto>>(notes));
        }
    }
}
