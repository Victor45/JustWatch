using System.Runtime.ExceptionServices;

namespace JustWatch.Web.Models
{
    public class ActorModel
    {
        public int ActorId { get; set; }
        public string Name { get; set; } =string.Empty;
        public string BirthYear { get; set; } = string.Empty;
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
        public string ActorPhoto { get; set; } = "/images/defaultavatar.jpg";
    }
}
