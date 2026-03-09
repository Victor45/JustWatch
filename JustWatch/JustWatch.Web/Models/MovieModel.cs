namespace JustWatch.Web.Models
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public double Rating { get; set; }
        public string Genres { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public string Description { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string Wallpaper {  get; set; } = string.Empty;
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
        public List<CommentModel> Comments { get; set; } = new List<CommentModel>();
    }
}
