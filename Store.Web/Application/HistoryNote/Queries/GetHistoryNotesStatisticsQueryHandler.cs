using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Store.Web.Abstractions.Data;
using Store.Web.Controllers;
using Store.Web.Dtos.HistoryNote;

namespace Store.Web.Application.HistoryNote.Queries
{
    public class GetHistoryNotesStatisticsQueryHandler : IRequestHandler<GetHistoryNotesStatisticsQuery, List<HistoryNoteStatisticsViewDto>>
    {
        private readonly IHistoryNoteRepo<Models.HistoryNote> _repo;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<StatisticsController> _logger;

        public GetHistoryNotesStatisticsQueryHandler(IHistoryNoteRepo<Models.HistoryNote> repo,
                                    IMapper mapper,
                                    UserManager<IdentityUser> userManager,
                                    ILogger<StatisticsController> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<List<HistoryNoteStatisticsViewDto>> Handle(GetHistoryNotesStatisticsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var clientsId = _userManager.GetUsersInRoleAsync("Client").Result.Select(u => u.Id);
                var notesLastDay = _repo.GetAllHistoryNotesLastDay().Result.Where(n => clientsId.Contains(n.UserId)).ToList();
                var mapperNotes = _mapper.Map<List<Models.HistoryNote>, List<HistoryNoteStatisticsViewDto>>(notesLastDay);
                _logger.LogInformation("StatisticsMainView: successfull");

                return mapperNotes;
            }
            catch (Exception e)
            {
                _logger.LogError($"StatisticsMainView: error occured. Reason: {e.Message}");
                return null;
            }
        }
    }
}
