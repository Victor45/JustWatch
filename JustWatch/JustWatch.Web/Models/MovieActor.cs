namespace JustWatch.Web.Models
{
    public class MovieActor
    {
        public int MovieId { get; set; }
        public MovieModel Movie { get; set; } = null!;

        public int ActorId { get; set; }
        public ActorModel Actor { get; set; } = null!;

        public string Role { get; set; } = string.Empty;
        public int CastOrder { get; set; }
    }
}
