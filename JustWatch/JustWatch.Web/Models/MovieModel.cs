namespace JustWatch.Web.Models
{
     public class MovieModel
     {
          public int Id { get; set; }
          public string Title { get; set; } = string.Empty;
          public int ReleaseYear { get; set; }
          public string Description { get; set; } = string.Empty;
          public string PosterUrl { get; set; } = string.Empty;
     }
}
