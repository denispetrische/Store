using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Store.Web.Abstractions.Data;
using Store.Web.Dtos.HistoryNote;

namespace Store.Web.Application.HistoryNote.Queries
{
    public class ClientDetailedInfoQueryHandler : IRequestHandler<ClientDetailedInfoQuery, List<HistoryNoteStatisticsViewDto>>
    {
        private readonly IHistoryNoteRepo<Models.HistoryNote> _repo;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<ClientDetailedInfoQueryHandler> _logger;

        public ClientDetailedInfoQueryHandler(IHistoryNoteRepo<Models.HistoryNote> repo,
                                    IMapper mapper,
                                    UserManager<IdentityUser> userManager,
                                    ILogger<ClientDetailedInfoQueryHandler> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<List<HistoryNoteStatisticsViewDto>> Handle(ClientDetailedInfoQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                _logger.LogWarning("Id = null");
                return null;
            }

            try
            {
                var notes = _repo.GetHistoryNotesForUserLastMonth(request.Id).Result.ToList();
                _logger.LogInformation("ClientDetailedInfoView: notes successfully received");

                return _mapper.Map<List<Models.HistoryNote>, List<HistoryNoteStatisticsViewDto>>(notes);
            }
            catch (Exception e)
            {
                _logger.LogError($"ClientDetailedInfoView: cannot get notes. Reason: {e.Message}");
                return null;
            }
        }
    }
}
