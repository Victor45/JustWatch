using JustWatch.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Domain.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserAvatar { get; set; } = "/images/defaultavatar.jpg";
        public UserRole Role { get; set; } = UserRole.User;
        public ICollection<MovieComment> MovieComments { get; set; } = new List<MovieComment>();
        public ICollection<TVShowComment> TVShowComments { get; set; } = new List<TVShowComment>(); 
    }
}
