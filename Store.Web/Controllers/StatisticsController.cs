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
        private readonly IHistoryNoteRepo _repo;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public StatisticsController(IHistoryNoteRepo repo, IMapper mapper, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _repo = repo;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> StatisticsMainView()
        {
            var clientsId = _userManager.GetUsersInRoleAsync("Client").Result.Select(u => u.Id);
            var notesLastDay = _repo.GetAllHistoryNotesLastDay().Result.Where(n => clientsId.Contains(n.UserId)).ToList();
            var mapperNotes = _mapper.Map<List<HistoryNote>, List<HistoryNoteStatisticsViewDto>>(notesLastDay);
            return View(mapperNotes);
        }
    }
}
