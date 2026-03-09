using JustWatch.Application.DTO;
using JustWatch.Application.Interfaces;
using JustWatch.Web.Models;
using JustWatch.Web.Models.Actors;
using JustWatch.Web.Models.Commom;
using JustWatch.Web.Models.Movies;
using JustWatch.Web.Models.TVShows;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace JustWatch.Web.Controllers
{
    public class TVShowsController : Controller
    {
        private readonly ITVShowService _tvShowService;
        private readonly IWebHostEnvironment _env;
        public TVShowsController(ITVShowService tvShowService, IWebHostEnvironment env)
        {
            _tvShowService = tvShowService;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var tvshowsDto = await _tvShowService.GetAllTvShowsAsync();

            var tvshowsViewModel = tvshowsDto.Select(tv => new TVShowViewModel
            {
                Id = tv.Id,
                Title = tv.Title,
                Seasons = tv.Seasons,
                PosterUrl = tv.PosterUrl,
            }).ToList();

            return View(tvshowsViewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var tvshowDto = await _tvShowService.GetTvShowByIdAsync(id);

            var tvshowDetailsViewModel = new TVShowDetailsViewModel
            {
                Id = tvshowDto.Id,
                Title = tvshowDto.Title,
                Rating = tvshowDto.Rating,
                Years = tvshowDto.Years,
                Seasons = tvshowDto.Seasons,
                Description = tvshowDto.Description,
                Director = tvshowDto.Director,
                Duration = tvshowDto.Duration,
                PosterUrl = tvshowDto.PosterUrl,
                Wallpaper = tvshowDto.Wallpaper,
                Actors = tvshowDto.Actors.Select(a => new ActorInMovieViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Role = a.Role,
                    CastOrder = a.CastOrder,
                }).ToList(),
                Genres = tvshowDto.Genres.Select(g => new GenreViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                }).ToList(),
                Comments = tvshowDto.Comments.Select(c => new CommentViewModel
                {
                    ID = c.ID,
                    Text = c.Text,
                    CreatedAt = c.CreatedAt,
                    UserAvatar = c.UserAvatar,
                    UserId = c.UserId,
                    UserName = c.UserName,
                }).ToList()
            };

            return View(tvshowDetailsViewModel);
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
                UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
            };

            await _tvShowService.AddNewCommentAsync(commentDTO);

            return RedirectToAction("Details", new { id = addCommentViewModel.ContentId });
        }

        public IActionResult NewTVShow()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> NewTVShow(NewTVShowViewModel newTVShowViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(newTVShowViewModel);
            }

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var actors = string.IsNullOrEmpty(newTVShowViewModel.ActorsJSON)
                ? new List<ActorInMovieViewModel>()
                : JsonSerializer.Deserialize<List<ActorInMovieViewModel>>(newTVShowViewModel.ActorsJSON, options);

            var genres = string.IsNullOrEmpty(newTVShowViewModel.GenresJSON)
                ? new List<GenreViewModel>()
                : JsonSerializer.Deserialize<List<GenreViewModel>>(newTVShowViewModel.GenresJSON, options);

            string posterURL = "/images/defaultposter.jpg";
            string wallpaperURL = "/images/defaultwall.jpg";

            if (newTVShowViewModel.Poster != null && newTVShowViewModel.Poster.Length > 0)
            {
                var posterFileName = Path.GetFileName(newTVShowViewModel.Poster.FileName);
                posterURL = $"/images/posters/movies/{posterFileName}";
                var posterPath = Path.Combine(_env.WebRootPath, "images/posters/tvshows", posterFileName);

                using (var stream = new FileStream(posterPath, FileMode.Create))
                {
                    await newTVShowViewModel.Poster.CopyToAsync(stream);
                }
            }

            if (newTVShowViewModel.Wallpaper != null && newTVShowViewModel.Wallpaper.Length > 0)
            {
                var wallpaperFileName = Path.GetFileName(newTVShowViewModel.Wallpaper.FileName);
                wallpaperURL = $"/images/wallpapers/movies/{wallpaperFileName}";
                var wallpaperPath = Path.Combine(_env.WebRootPath, "images/wallpapers/tvshows", wallpaperFileName);

                using (var stream = new FileStream(wallpaperPath, FileMode.Create))
                {
                    await newTVShowViewModel.Wallpaper.CopyToAsync(stream);
                }
            }

            var actorsDTO = actors.Select(a => new ActorInMovieDTO
            {
                Id = a.Id,
                Name = a.Name,
                Role = a.Role,
                CastOrder = a.CastOrder,
            }).ToList();

            var genresDTO = genres.Select(g => new GenreDTO { Id = g.Id, Name = g.Name }).ToList();

            var tvShowDTO = new TVShowDetailsDTO
            {
                Title = newTVShowViewModel.Title,
                Director = newTVShowViewModel.Director,
                Years = newTVShowViewModel.Years,
                Rating = newTVShowViewModel.Rating,
                Duration = newTVShowViewModel.Duration,
                Seasons = newTVShowViewModel.Seasons,
                Description = newTVShowViewModel.Description,
                PosterUrl = posterURL,
                Wallpaper = wallpaperURL,
                Actors = actorsDTO,
                Genres = genresDTO
            };

            var result = await _tvShowService.AddNewTvShowAsyc(tvShowDTO);

            return RedirectToAction("Details", new { id = result.Data });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTvShowDetails(EditTVShowViewModel editTVShowViewModel, string editGenresJson)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Details", new {Id = editTVShowViewModel.Id});
            }

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var newGenres = string.IsNullOrEmpty(editGenresJson)
                ? new List<GenreViewModel>()
                : JsonSerializer.Deserialize<List<GenreViewModel>>(editGenresJson, options);

            var tvShowDTO = new TVShowDetailsDTO
            {
                Id = editTVShowViewModel.Id,
                Title = editTVShowViewModel.Title,
                Director = editTVShowViewModel.Director,
                Years = editTVShowViewModel.Years,
                Rating = editTVShowViewModel.Rating,
                Duration = editTVShowViewModel.Duration,
                Seasons = editTVShowViewModel.Seasons,
                Description = editTVShowViewModel.Description,
                Genres = newGenres.Select(g => new GenreDTO { Id = g.Id, Name = g.Name }).ToList(),
            };

            var result = await _tvShowService.EditTvShowAsync(tvShowDTO);

            //if (!result.IsSuccess)
            //{

            //}

            return RedirectToAction("Details", new {id =  tvShowDTO.Id});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTvShowActors(string actorsJson, int id)
        {
            if (string.IsNullOrEmpty(actorsJson))
            {
                return RedirectToAction("Details", new { id = id });
            }

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var newActors = JsonSerializer.Deserialize<List<ActorInMovieViewModel>>(actorsJson, options);

            var actorsDTO = newActors.Select(a => new ActorInMovieDTO
            {
                Id = a.Id,
                Role = a.Role,
                CastOrder = a.CastOrder,
            }).ToList();

            var result = await _tvShowService.EditTvShowActorsAsync(id, actorsDTO);

            return RedirectToAction("Details", new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTvShow(int id)
        {
            var result = await _tvShowService.DeleteTvShowAsync(id);
            if (!result.IsSuccess)
            {
                return RedirectToAction("Details", new { id = id });
            }
            return RedirectToAction("Index");
        }
    }
}
