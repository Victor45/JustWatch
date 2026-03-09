using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Application.DTO.Movies
{
    public class EditMovieDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public int Year { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Duration { get; set; }
        public string Director { get; set; } = "Jon Snow";
        public List<GenreDTO> Genres { get; set; } = new List<GenreDTO>();
    }
}
