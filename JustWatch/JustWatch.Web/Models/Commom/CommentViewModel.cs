namespace JustWatch.Web.Models.Commom
{
    public class CommentViewModel
    {
        public int ID { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserAvatar { get; set; } = string.Empty;
    }
}
