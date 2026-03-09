using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Application.DTO
{
    public class TVShowDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Seasons { get; set; }
        public string PosterUrl { get; set; } = string.Empty;
    }
}
