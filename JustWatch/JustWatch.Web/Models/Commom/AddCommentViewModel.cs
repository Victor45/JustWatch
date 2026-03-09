namespace JustWatch.Web.Models.Commom
{
    public class AddCommentViewModel
    {
        public int ContentId { get; set; } 
        public int UserId { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

    }
}
