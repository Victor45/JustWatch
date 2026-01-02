using System.Diagnostics;
using JustWatch.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace JustWatch.Web.Controllers
{
     public class HomeController : Controller
     {
          private readonly ILogger<HomeController> _logger;

          public HomeController(ILogger<HomeController> logger)
          {
               _logger = logger;
          }

          public IActionResult Index()
          {
               return View();
          }

          public IActionResult Start()
          {
               var movies = new List<MovieModel>
               {
                    new() { Id = 1, Title = "Dune: Part Two", ReleaseYear = 2024, PosterUrl="/images/dune2.jpg" },
                    new() { Id = 2, Title = "Oppenheimer", ReleaseYear = 2023, PosterUrl="/images/oppenheimer.jpg" },
                    new() { Id = 3, Title = "Interstellar", ReleaseYear = 2014, PosterUrl="/images/interstellar.jpg" },
                    new() { Id = 4, Title = "The Batman", ReleaseYear = 2022, PosterUrl="/images/batman.jpg" },
                    new() { Id = 5, Title = "Maze Runner", ReleaseYear = 2014, PosterUrl="/images/mazerunner.jpg" },
                    new() { Id = 6, Title = "Caddo Lake", ReleaseYear = 2022, PosterUrl="/images/caddolake.jpg" },
               };

               var tvshows = new List<TVShowModel>
               {
                    new TVShowModel {Id = 1, Title = "Prison Break", Seasons = 5, PosterUrl="/images/pbreakposter.jpg" },
                    new TVShowModel {Id = 2, Title = "Lucifer", Seasons = 5, PosterUrl="/images/luciferposter.jpg" },
                    new TVShowModel {Id = 3, Title = "Supernatural", Seasons = 15, PosterUrl="/images/spnposter.jpg"},
                    new TVShowModel {Id = 4, Title = "The Walking Dead", Seasons = 5, PosterUrl="/images/twdposter.jpg" },
                    new TVShowModel {Id = 5, Title = "From", Seasons = 4, PosterUrl="/images/fromposter.jpg" },
                    new TVShowModel {Id = 6, Title = "Stranger Things", Seasons = 5, PosterUrl="/images/strangerposter.jpg"}
               };

               var moviesandtvshows = new StartViewModel
               {
                    Movies = movies,
                    TVShows = tvshows,
               };
               return View(moviesandtvshows);
          }

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
