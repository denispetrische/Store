using Store.Web.Models;

namespace Store.Web.Abstractions.Data
{
    public interface IHistoryNoteRepo<T> where T : HistoryNote
    {
        Task<IReadOnlyList<T>> GetHistoryNotes();
        Task CreateHistoryNote(T historyNote);
        Task<IReadOnlyList<T>> GetHistoryNotesForUserLastMonth(string id);
        Task<IReadOnlyList<T>> GetAllHistoryNotesLastDay();
    }
}
