using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Domain.Models
{
    public class Actor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
        public ICollection<TVShowActor> TVShowActors { get; set; } = new List<TVShowActor>();
    }
}
