using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Domain.Models
{
    public class TVShow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Seasons { get; set; }
        public decimal Rating { get; set; }
        public string Years { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PosterUrl { get; set; } = string.Empty;
        public string Wallpaper { get; set; } = "/images/defaultwall.jpg";
        public int Duration { get; set; }
        public string Director { get; set; } = "Michael Scofield";
        public ICollection<TVShowActor> TVShowActors { get; set; } = new List<TVShowActor>();
        public ICollection<TVShowGenre> TVShowGenres { get; set; } = new List<TVShowGenre>();
        public ICollection<TVShowComment> Comments { get; set; } = new List<TVShowComment>(); 
    }
}
