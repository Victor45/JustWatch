using JustWatch.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Application.Interfaces
{
    public interface IGenreService
    {
        Task<List<GenreDTO>> GetAllGenreAsyc();
    }
}
