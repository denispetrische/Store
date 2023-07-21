namespace Store.Web.Models
{
    public class HistoryNote
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
    }
}
