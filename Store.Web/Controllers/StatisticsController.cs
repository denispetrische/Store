using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.Web.Abstractions.Data;
using Store.Web.Dtos.HistoryNote;
using Store.Web.Models;

namespace Store.Web.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IHistoryNoteRepo<HistoryNote> _repo;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<StatisticsController> _logger;

        public StatisticsController(IHistoryNoteRepo<HistoryNote> repo, 
                                    IMapper mapper, 
                                    UserManager<IdentityUser> userManager, 
                                    RoleManager<IdentityRole> roleManager,
                                    ILogger<StatisticsController> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<IActionResult> StatisticsMainView()
        {
            List<HistoryNoteStatisticsViewDto> mapperNotes = null;

            try
            {
                var clientsId = _userManager.GetUsersInRoleAsync("Client").Result.Select(u => u.Id);
                var notesLastDay = _repo.GetAllHistoryNotesLastDay().Result.Where(n => clientsId.Contains(n.UserId)).ToList();
                mapperNotes = _mapper.Map<List<HistoryNote>, List<HistoryNoteStatisticsViewDto>>(notesLastDay);
                _logger.LogInformation("StatisticsMainView: successfull");
            }
            catch (Exception e)
            {
                _logger.LogError($"StatisticsMainView: error occured. Reason: {e.Message}");
            }
            
            return View(mapperNotes);
        }

        [HttpGet]
        public async Task<IActionResult> ClientDetailedInfoView([FromQuery(Name = "param1")] string? id)
        {
            if (id == null)
            {
                return RedirectToAction("StatisticsMainView");
            }

            List<HistoryNote> notes = null;

            try
            {
                notes = _repo.GetHistoryNotesForUserLastMonth(id).Result.ToList();
                _logger.LogInformation("ClientDetailedInfoView: notes successfully received");
            }
            catch (Exception e)
            {
                _logger.LogError($"ClientDetailedInfoView: cannot get notes. Reason: {e.Message}");
            }

            return View(_mapper.Map<List<HistoryNote>,List<HistoryNoteStatisticsViewDto>>(notes));
        }
    }
}
