namespace JustWatch.Web.Models
{
    public class CommentModel
    {
        public string Comment { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
