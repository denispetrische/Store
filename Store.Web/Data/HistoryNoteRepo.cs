using Store.Web.Abstractions.Data;
using Store.Web.Models;

namespace Store.Web.Data
{
    public class HistoryNoteRepo : IHistoryNoteRepo
    {
        public Task CreateHistoryNote(HistoryNote historyNote)
        {
            throw new NotImplementedException();
        }

        public Task<List<HistoryNote>> GetHistoryNotes()
        {
            throw new NotImplementedException();
        }
    }
}
