using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Domain.Models
{
    public class TVShowGenre
    {
        public int TVShowId { get; set; }
        public TVShow TVShow { get; set; } = null!;
        
        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;
    }
}
