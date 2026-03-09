using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Domain.Models
{
    public class TVShowActor
    {
        public int TVShowId { get; set; }
        public TVShow TVShow { get; set; } = null!;

        public int ActorId { get; set; }
        public Actor Actor { get; set; } = null!;

        public string Role { get; set; } = string.Empty;
        public int CastOrder { get; set; }
    }
}
