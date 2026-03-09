using JustWatch.Application.Interfaces;
using JustWatch.Web.Models.Commom;
using Microsoft.AspNetCore.Mvc;

namespace JustWatch.Web.Controllers
{
    public class GenresController : Controller
    {
        private readonly IGenreService _genreService;
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _genreService.GetAllGenreAsyc();

            var genresViewModel = genres.Select(genre => new GenreViewModel
            {
                Id = genre.Id,
                Name = genre.Name,
            }).ToList();

            return Ok(genresViewModel);
        }
    }
}
