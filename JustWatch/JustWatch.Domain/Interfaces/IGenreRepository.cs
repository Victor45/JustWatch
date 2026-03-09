using JustWatch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Domain.Interfaces
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetAllGenres();  
    }
}
