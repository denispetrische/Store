using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.Web.Abstractions.Data;
using Store.Web.Dtos.HistoryNote;
using Store.Web.Models;

namespace Store.Web.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IHistoryNoteRepo<HistoryNote> _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<HistoryController> _logger;

        public HistoryController(IHistoryNoteRepo<HistoryNote> repo, 
                                 IMapper mapper,
                                 ILogger<HistoryController> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public IActionResult HistoryMainView()
        {
           
            try
            {
                var notes = _repo.GetHistoryNotes().Result.ToList();
                _logger.LogInformation("HistoryMainView: notes was successfully received");

                return View(_mapper.Map<List<HistoryNote>, List<HistoryNoteHistoryViewDto>>(notes));
            }
            catch (Exception e)
            {
                _logger.LogError($"HistoryMainView: unable to get notes. Reason: {e.Message}");
                return BadRequest();
            }            
        }
    }
}
