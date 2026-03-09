using JustWatch.Application.DTO;
using JustWatch.Application.Interfaces;
using JustWatch.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<List<GenreDTO>> GetAllGenreAsyc()
        {
            var genres = await _genreRepository.GetAllGenres();

            var genresDTO = genres.Select(g => new GenreDTO
            {
                Id = g.Id,
                Name = g.Name,
            }).ToList();

            return genresDTO;
        }
    }
}
