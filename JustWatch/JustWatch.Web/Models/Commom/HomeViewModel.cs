using JustWatch.Web.Models.Movies;
using JustWatch.Web.Models.TVShows;

namespace JustWatch.Web.Models.Commom
{
    public class HomeViewModel
    {
        public List<MovieViewModel> Movies { get; set; } = new List<MovieViewModel>();
        public List<TVShowViewModel> TVShows { get; set; } = new List<TVShowViewModel>();
    }
}
