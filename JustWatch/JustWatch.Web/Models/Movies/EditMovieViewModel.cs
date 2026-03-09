using JustWatch.Web.Models.Commom;
using Microsoft.AspNetCore.Routing.Constraints;

namespace JustWatch.Web.Models.Movies
{
    public class EditMovieViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal Rating { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public IFormFile? Poster { get; set; }
        public IFormFile? Wallpaper { get; set; }
        public List<GenreViewModel> Genres { get; set; } = new List<GenreViewModel>();
    }
}
