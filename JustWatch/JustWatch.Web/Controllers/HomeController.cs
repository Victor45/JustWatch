using System.ComponentModel;
using System.Diagnostics;
using JustWatch.Application.Interfaces;
using JustWatch.Web.Models;
using JustWatch.Web.Models.Commom;
using JustWatch.Web.Models.Movies;
using JustWatch.Web.Models.TVShows;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace JustWatch.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;
        private readonly ITVShowService _tvShowService;
        private readonly ISearchService _searchService;

        public HomeController(ILogger<HomeController> logger, IMovieService movieService, ITVShowService tvShowService, ISearchService searchService)
        {
            _logger = logger;
            _movieService = movieService;
            _tvShowService = tvShowService;
            _searchService = searchService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Start()
        {
            var moviesDto = await _movieService.GetTopMoviesAsync(6);
            var tvshowsDto = await _tvShowService.GetTopShowsAsync(6);

            var moviesVm = moviesDto.Select(movie => new MovieViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                PosterUrl = movie.PosterUrl,
            }).ToList();

            var tvshowsVm = tvshowsDto.Select(tvshow => new TVShowViewModel
            {
                Id = tvshow.Id,
                Title = tvshow.Title,
                Seasons = tvshow.Seasons,
                PosterUrl = tvshow.PosterUrl,
            }).ToList();

            var moviesandtvshows = new HomeViewModel
            {
                Movies = moviesVm,
                TVShows = tvshowsVm,
            };

            return View(moviesandtvshows);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string? q, bool isFromButton = false)
        {
            q = (q ?? string.Empty).Trim();
            if(q.Length < 2)
            {
                return isFromButton ? View(new List<SearchContentViewModel>()) : Ok(new List<SearchContentViewModel>());
            }

            var items = await _searchService.SearchContentAsync(q);

            var itemsViewModel = items.Select(x => new SearchContentViewModel
            {
                Id = x.Id,
                Title = x.Title,
                PosterURL = x.PosterURL,
                Info = x.Info,
                Type = x.Type
            }).ToList();    

            return isFromButton ? View("SearchResult", itemsViewModel) : Ok(itemsViewModel);
        }

        //[HttpGet]
        //public async Task<IActionResult> SearchResult(string? q)
        //{

        //    q = (q ?? string.Empty).Trim();
        //    if (q.Length < 2)
        //    {
        //        return View(new List<SearchContentViewModel>());
        //    }

        //    var items = await _searchService.SearchContentAsync(q);

        //    var itemsViewModel = items.Select(x => new SearchContentViewModel
        //    {
        //        Id = x.Id,
        //        Title = x.Title,
        //        PosterURL = x.PosterURL,
        //        Info = x.Info,
        //        Type = x.Type
        //    }).ToList();

        //    return View(itemsViewModel);
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
