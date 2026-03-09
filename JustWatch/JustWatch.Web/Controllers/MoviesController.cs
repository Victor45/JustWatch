using JustWatch.Application.DTO;
using JustWatch.Application.DTO.Movies;
using JustWatch.Application.Interfaces;
using JustWatch.Domain.Interfaces;
using JustWatch.Web.Models;
using JustWatch.Web.Models.Actors;
using JustWatch.Web.Models.Commom;
using JustWatch.Web.Models.Movies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace JustWatch.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService, IWebHostEnvironment env)
        {
            _movieService = movieService;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var moviesDto = await _movieService.GetAlMoviesAsync();

            var moviesViewModel = moviesDto.Select(m => new MovieViewModel
            {
                Id = m.Id,
                Title = m.Title,
                PosterUrl = m.PosterUrl,
                Year = m.Year,
            }).ToList();

            return View(moviesViewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);

            var movieDetailsViewModel = new MovieDetailsViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Rating = movie.Rating,
                Year = movie.Year,
                Description = movie.Description,
                PosterUrl = movie.PosterUrl,
                Wallpaper = movie.Wallpaper,
                Duration = movie.Duration,
                Director = movie.Director,
                Actors = movie.Actors.Select(a => new ActorInMovieViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Role = a.Role,
                    CastOrder = a.CastOrder,
                }).ToList(),
                Genres = movie.Genres.Select(g => new GenreViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                }).ToList(),
                Comments = movie.Comments.Select(c => new CommentViewModel
                {
                    ID = c.ID,
                    Text = c.Text,
                    CreatedAt = c.CreatedAt,
                    UserId = c.UserId,
                    UserAvatar = c.UserAvatar,
                    UserName = c.UserName,
                }).ToList()
            };

            return View(movieDetailsViewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(AddCommentViewModel addCommentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", new { id = addCommentViewModel.ContentId });
            }

            var commentDTO = new CommentDTO
            {
                ContentId = addCommentViewModel.ContentId,
                Text = addCommentViewModel.Text,
                CreatedAt = DateTime.UtcNow,
                UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!),
            };

            await _movieService.AddNewCommentAsync(commentDTO);

            return RedirectToAction("Details", new { id = addCommentViewModel.ContentId });
        }


        public IActionResult NewMovie()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewMovie(NewMovieViewModel newMovieViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(newMovieViewModel);
            }

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true  };

            var actors = string.IsNullOrEmpty(newMovieViewModel.ActorsJSON) 
                ? new List<ActorInMovieViewModel>()
                : JsonSerializer.Deserialize<List<ActorInMovieViewModel>>(newMovieViewModel.ActorsJSON, options);

            var genres = string.IsNullOrEmpty(newMovieViewModel.GenresJSON)
                ? new List<GenreViewModel>()
                : JsonSerializer.Deserialize<List<GenreViewModel>>(newMovieViewModel.GenresJSON, options);

            string posterURL = "/images/defaultposter.jpg";
            string wallpaperURL = "/images/defaultwall.jpg";

            if (newMovieViewModel.Poster != null && newMovieViewModel.Poster.Length > 0)
            {
                var posterFileName = Path.GetFileName(newMovieViewModel.Poster.FileName);
                posterURL = $"/images/posters/movies/{posterFileName}";
                var posterPath = Path.Combine(_env.WebRootPath, "images/posters/movies", posterFileName);

                using (var stream = new FileStream(posterPath, FileMode.Create))
                {
                    await newMovieViewModel.Poster.CopyToAsync(stream);
                }
            }

            if (newMovieViewModel.Wallpaper != null && newMovieViewModel.Wallpaper.Length > 0)
            {
                var wallpaperFileName = Path.GetFileName(newMovieViewModel.Wallpaper.FileName);
                wallpaperURL = $"/images/wallpapers/movies/{wallpaperFileName}";
                var wallpaperPath = Path.Combine(_env.WebRootPath, "images/wallpapers/movies", wallpaperFileName);

                using (var stream = new FileStream(wallpaperPath, FileMode.Create))
                {
                    await newMovieViewModel.Wallpaper.CopyToAsync(stream);
                }
            }


            var actorsDTO = actors.Select(a => new ActorInMovieDTO
            {
                Id = a.Id,
                Name = a.Name,
                Role = a.Role,
                CastOrder = a.CastOrder,
            }).ToList();

            var genresDTO = genres.Select(g => new GenreDTO
            {
                Id = g.Id,
                Name = g.Name,
            }).ToList();

            var movieDTO = new MovieDetailsDTO
            {
                Title = newMovieViewModel.Title,
                Description = newMovieViewModel.Description,
                Director = newMovieViewModel.Director,
                Year = newMovieViewModel.Year,
                Rating = newMovieViewModel.Rating,
                Duration = newMovieViewModel.Duration,
                PosterUrl = posterURL,
                Wallpaper = wallpaperURL,
                Actors = actorsDTO,
                Genres = genresDTO,
            };

            var result = await _movieService.AddNewMovieAsync(movieDTO);

            return RedirectToAction("Details", new {id = result.Data});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            await _movieService.DeleteMovieAsync(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMovieDetails(EditMovieViewModel editMovieViewModel, string editGenresJson)
        {
            if (!ModelState.IsValid)
            {
                return View(editMovieViewModel);
            }

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            List<GenreViewModel> newGenres = new List<GenreViewModel>();

            if (!string.IsNullOrEmpty(editGenresJson))
            {
                newGenres = JsonSerializer.Deserialize<List<GenreViewModel>>(editGenresJson, options);
            }
            var editedMovie = new EditMovieDTO
            {
                Id = editMovieViewModel.Id,
                Title = editMovieViewModel.Title,
                Year = editMovieViewModel.Year,
                Rating = editMovieViewModel.Rating,
                Description = editMovieViewModel.Description,
                Director = editMovieViewModel.Director,
                Duration = editMovieViewModel.Duration,
                Genres = newGenres.Select(g => new GenreDTO
                {
                    Id = g.Id,
                    Name = g.Name,
                }).ToList()
            };

            var result = await _movieService.EditMovieAsync(editedMovie);

            return RedirectToAction("Details", new { id = editedMovie.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMovieActors(int id, string actorsJson)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            List<ActorInMovieDTO> newActors = new List<ActorInMovieDTO>();

            if (!string.IsNullOrEmpty(actorsJson))
            {
                newActors = JsonSerializer.Deserialize<List<ActorInMovieDTO>>(actorsJson, options);
            }

            var newActorsDto = newActors.Select(a => new ActorInMovieDTO
            {
                Id = a.Id,
                Name = a.Name,
                Role = a.Role,
                CastOrder = a.CastOrder,
            }).ToList();

            var result = await _movieService.EditMovieActorsAsync(id, newActorsDto);

            if (!result.IsSuccess)
            {
                return NotFound(result.ErrorMessage);
            }

            return RedirectToAction("Details", new {id = id});
        }
    }
}
