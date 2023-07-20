using Microsoft.EntityFrameworkCore;
using Store.Web.Abstractions.Data;
using Store.Web.Models;

namespace Store.Web.Data
{
    public class HistoryNoteRepo : IHistoryNoteRepo
    {
        private readonly StoreWebContext _context;
        private string format = "yyyy-MM-dd HH:mm:ss";

        public HistoryNoteRepo(StoreWebContext context)
        {
            _context = context;
        }

        public Task CreateHistoryNote(HistoryNote historyNote)
        {
            _context.Database.ExecuteSqlRaw($"CreateHistoryNote '{historyNote.Id}', " +
                                                          $"'{historyNote.Message}', " +
                                                          $"'{historyNote.Date.ToString(format)}', " +
                                                          $"'{historyNote.UserId}'");

            return Task.CompletedTask;
        }

        public Task<List<HistoryNote>> GetAllHistoryNotesLastDay()
        {
            var notes = _context.HistoryNotes.FromSqlRaw($"GetAllHistoryNotesLastDay '{DateTime.Now.AddDays(-1).ToString(format)}'").ToList();

            return Task.FromResult(notes);
        }

        public Task<List<HistoryNote>> GetHistoryNotes()
        {
            var notes = _context.HistoryNotes.FromSqlRaw("GetHistoryNotes").ToList();

            return Task.FromResult(notes);
        }

        public Task<List<HistoryNote>> GetHistoryNotesForUserLastMonth(string id)
        {
            var notes = _context.HistoryNotes.FromSqlRaw($"GetHistoryNotesForUserLastMonth '{id}', '{DateTime.Now.AddMonths(-1).ToString(format)}'").ToList();

            return Task.FromResult(notes);
        }
    }
}
