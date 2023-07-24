using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Store.Web.Abstractions.Data;
using Store.Web.Models;
using System.Collections.Generic;

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
            var param1 = new SqlParameter("@Id", historyNote.Id);
            var param2 = new SqlParameter("@Message", historyNote.Message);
            var param3 = new SqlParameter("@Date", historyNote.Date);
            var param4 = new SqlParameter("@UserId", historyNote.UserId);

            await _context.Database.ExecuteSqlRawAsync($"CreateHistoryNote @Id, @Message, @Date, @UserId",
                                                        param1, param2, param3, param4);
        }

        public async Task<IReadOnlyList<HistoryNote>> GetAllHistoryNotesLastDay()
        {
            return await Task.Factory.StartNew<IReadOnlyList<HistoryNote>>(() =>
            {
                var param1 = new SqlParameter("@Date", DateTime.Now.AddDays(-1).ToString(format));
                var notes = _context.HistoryNotes.FromSqlRaw($"GetAllHistoryNotesLastDay @Date", param1).ToList();
                return notes;
            });
        }

        public async Task<IReadOnlyList<HistoryNote>> GetHistoryNotes()
        {
            return await Task.Factory.StartNew<IReadOnlyList<HistoryNote>>(() =>
            {
                var notes = _context.HistoryNotes.FromSqlRaw("GetHistoryNotes").ToList();
                return notes;
            });
        }

        public async Task<IReadOnlyList<HistoryNote>> GetHistoryNotesForUserLastMonth(string id)
        {
            return await Task.Factory.StartNew<IReadOnlyList<HistoryNote>>(() =>
            {
                var param1 = new SqlParameter("@UserId", id);
                var param2 = new SqlParameter("@Date", DateTime.Now.AddMonths(-1).ToString(format));

                var notes = _context.HistoryNotes.FromSqlRaw($"GetHistoryNotesForUserLastMonth @UserId, @Date", param1, param2).ToList();

                return notes;
            });            
        }
    }
}
