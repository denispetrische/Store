using AutoMapper;
using MediatR;
using Store.Web.Abstractions.Data;
using Store.Web.Controllers;
using Store.Web.Dtos.HistoryNote;
using Store.Web.Dtos.Product;

namespace Store.Web.Application.HistoryNote.Queries
{
    public class GetHistoryNotesQueryHandler : IRequestHandler<GetHistoryNotesQuery, List<HistoryNoteHistoryViewDto>>
    {
        private readonly IHistoryNoteRepo<Models.HistoryNote> _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<HistoryController> _logger;

        public GetHistoryNotesQueryHandler(IHistoryNoteRepo<Models.HistoryNote> repo,
                                           IMapper mapper,
                                           ILogger<HistoryController> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<HistoryNoteHistoryViewDto>> Handle(GetHistoryNotesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var notes = _repo.GetHistoryNotes().Result.ToList();
                _logger.LogInformation("HistoryMainView: notes was successfully received");

                return _mapper.Map<List<Models.HistoryNote>, List<HistoryNoteHistoryViewDto>>(notes);
            }
            catch (Exception e)
            {
                _logger.LogError($"HistoryMainView: unable to get notes. Reason: {e.Message}");
                return null;
            }
        }
    }
}
