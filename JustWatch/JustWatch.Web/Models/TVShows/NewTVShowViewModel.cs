namespace JustWatch.Web.Models.TVShows
{
    public class NewTVShowViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string Years { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public int Seasons { get; set; }
        public int Duration { get; set; }
        public string Director { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public IFormFile? Poster { get; set; }
        public IFormFile? Wallpaper { get; set; }

        public string ActorsJSON { get; set; } = string.Empty;
        public string GenresJSON { get; set; } = string.Empty;
    }
}
