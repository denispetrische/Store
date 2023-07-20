using Store.Web.Abstractions.Data;
using Store.Web.Models;

namespace Store.Web.Data
{
    public class HistoryNoteRepo : IHistoryNoteRepo
    {
        private readonly StoreWebContext _context;

        public HistoryNoteRepo(StoreWebContext context)
        {
            _context = context;
        }

        public Task CreateHistoryNote(HistoryNote historyNote)
        {
            throw new NotImplementedException();
        }

        public Task<List<HistoryNote>> GetAllHistoryNotesLastDay()
        {
            throw new NotImplementedException();
        }

        public Task<List<HistoryNote>> GetHistoryNotes()
        {
            throw new NotImplementedException();
        }

        public Task<List<HistoryNote>> GetHistoryNotesForUserLastMonth(string id)
        {
            throw new NotImplementedException();
        }
    }
}
