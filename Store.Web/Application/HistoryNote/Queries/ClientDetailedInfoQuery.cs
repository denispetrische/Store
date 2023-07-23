using MediatR;
using Store.Web.Dtos.HistoryNote;

namespace Store.Web.Application.HistoryNote.Queries
{
    public class ClientDetailedInfoQuery : IRequest<List<HistoryNoteStatisticsViewDto>>
    {
        public string? Id { get; set; }
    }
}
