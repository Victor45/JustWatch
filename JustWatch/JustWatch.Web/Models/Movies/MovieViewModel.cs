namespace JustWatch.Web.Models.Movies
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; }
        public string PosterUrl { get; set; } = string.Empty;
    }
}
