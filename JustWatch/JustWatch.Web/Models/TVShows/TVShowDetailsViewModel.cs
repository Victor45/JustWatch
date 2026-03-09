using JustWatch.Web.Models.Actors;
using JustWatch.Web.Models.Commom;

namespace JustWatch.Web.Models.TVShows
{
    public class TVShowDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public string Years { get; set; } = string.Empty;
        public int Seasons { get; set; }
        public string Description { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = "/images/defaultposter.jpg";
        public string Wallpaper { get; set; } = "/images/defaultwall.jpg";
        public int Duration { get; set; }
        public string Director { get; set; } = "Jon Snow";
        public List<ActorInMovieViewModel> Actors { get; set; } = new List<ActorInMovieViewModel>();
        public List<GenreViewModel> Genres { get; set; } = new List<GenreViewModel>();
        public List<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();
    }
}
