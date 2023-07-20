using Store.Web.Models;

namespace Store.Web.Abstractions.Data
{
    public interface IHistoryNoteRepo
    {
        Task<List<HistoryNote>> GetHistoryNotes();
        Task CreateHistoryNote(HistoryNote historyNote);
        Task<List<HistoryNote>> GetHistoryNotesForUserLastMonth(string id);
        Task<List<HistoryNote>> GetAllHistoryNotesLastDay();
    }
}
