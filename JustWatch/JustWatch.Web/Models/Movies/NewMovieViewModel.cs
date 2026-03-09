namespace JustWatch.Web.Models.Movies
{
    public class NewMovieViewModel
    {
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; } 
        public decimal Rating { get; set; }
        public int Duration { get; set; }
        public string Director { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public IFormFile? Poster { get; set; } 
        public IFormFile? Wallpaper { get; set; }

        public string ActorsJSON { get; set; } = string.Empty;
        public string GenresJSON { get; set; } = string.Empty;
    }
}
