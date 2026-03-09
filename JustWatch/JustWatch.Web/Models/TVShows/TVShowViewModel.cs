namespace JustWatch.Web.Models.TVShows
{
    public class TVShowViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Seasons { get; set; }
        public string PosterUrl { get; set; } = string.Empty;
    }
}
