namespace JustWatch.Web.Models
{
     public class StartViewModel
     {
          public List<MovieModel> Movies { get; set; } = new List<MovieModel>();
          public List<TVShowModel> TVShows { get; set; } = new List<TVShowModel>();
     }
}
