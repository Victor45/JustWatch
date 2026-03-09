using JustWatch.Web.Models.Commom;

namespace JustWatch.Web.Models.TVShows
{
    public class EditTVShowViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Years { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public int Duration { get; set; }
        public int Seasons { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public IFormFile? Poster { get; set; }
        public IFormFile? Wallpaper { get; set; }
        public List<GenreViewModel> Genres { get; set; } = new List<GenreViewModel>();
    }
}
