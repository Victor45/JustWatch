using JustWatch.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Application.DTO
{
    public class MovieDetailsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Rating { get; set; }
        public int Year { get; set; }
        public string Description { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = "/images/defaultposter.jpg";
        public string Wallpaper { get; set; } = "/images/defaultwall.jpg";
        public int Duration { get; set; }
        public string Director { get; set; } = "Jon Snow";
        public List<ActorInMovieDTO> Actors { get; set; } = new List<ActorInMovieDTO>();
        public List<GenreDTO> Genres { get; set; } = new List<GenreDTO>();
        public List<CommentDTO> Comments { get; set; } = new List<CommentDTO>();
    }
}
