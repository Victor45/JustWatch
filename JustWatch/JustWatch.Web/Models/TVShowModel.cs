namespace JustWatch.Web.Models
{
     public class TVShowModel
     {
          public int Id { get; set; }
          public string Title { get; set; } = string.Empty;
          public int Years { get; set; }
          public string Description { get; set; } = string.Empty;
          public string PosterUrl { get; set; } = string.Empty;

          public int Seasons { get; set; }
     }
}
