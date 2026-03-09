using JustWatch.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Domain.Models
{
    public class SearchContent
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string PosterURL { get; set; } = string.Empty;
        public int Info { get; set; }
        public string Type { get; set; }
    }
}
