using Microsoft.EntityFrameworkCore;
using Store.Web.Abstractions.Data;
using Store.Web.Models;

namespace Store.Web.Data
{
    public class HistoryNoteRepo : IHistoryNoteRepo<HistoryNote>
    {
        private readonly StoreWebContext _context;
        private string format = "yyyy-MM-dd HH:mm:ss";

        public HistoryNoteRepo(StoreWebContext context)
        {
            _context = context;
        }

        public async Task CreateHistoryNote(HistoryNote historyNote)
        {
            await _context.Database.ExecuteSqlRawAsync($"CreateHistoryNote '{historyNote.Id}', " +
                                                          $"N'{historyNote.Message}', " +
                                                          $"'{historyNote.Date.ToString(format)}', " +
                                                          $"'{historyNote.UserId}'");
        }

        public async Task<IReadOnlyList<HistoryNote>> GetAllHistoryNotesLastDay()
        {
            var notes = _context.HistoryNotes.FromSqlRaw($"GetAllHistoryNotesLastDay '{DateTime.Now.AddDays(-1).ToString(format)}'").ToList();

            return notes;
        }

        public async Task<IReadOnlyList<HistoryNote>> GetHistoryNotes()
        {
            var notes = _context.HistoryNotes.FromSqlRaw("GetHistoryNotes").ToList();

            return notes;
        }

        public async Task<IReadOnlyList<HistoryNote>> GetHistoryNotesForUserLastMonth(string id)
        {
            var notes = _context.HistoryNotes.FromSqlRaw($"GetHistoryNotesForUserLastMonth '{id}', '{DateTime.Now.AddMonths(-1).ToString(format)}'").ToList();

            return notes;
        }
    }
}
