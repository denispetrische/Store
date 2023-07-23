using MediatR;
using Store.Web.Dtos.HistoryNote;

namespace Store.Web.Application.HistoryNote.Queries
{
    public class GetHistoryNotesQuery : IRequest<List<HistoryNoteHistoryViewDto>>
    {

    }
}
