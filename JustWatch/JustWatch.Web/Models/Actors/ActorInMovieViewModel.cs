namespace JustWatch.Web.Models.Actors
{
    public class ActorInMovieViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public int CastOrder { get; set; }
    }
}
