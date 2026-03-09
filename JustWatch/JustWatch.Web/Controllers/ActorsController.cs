using JustWatch.Application.DTO;
using JustWatch.Application.Interfaces;
using JustWatch.Domain.Interfaces;
using JustWatch.Web.Models.Actors;
using JustWatch.Web.Models.Commom;
using Microsoft.AspNetCore.Mvc;

namespace JustWatch.Web.Controllers
{
    public class ActorsController : Controller
    {

        private readonly ISearchService _searchService;
        private readonly IActorService _actorService;
        public ActorsController(ISearchService searchService, IActorService actorService)
        {
            _searchService = searchService;
            _actorService = actorService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SearchActor(string query)
        {
            query = (query ?? string.Empty).Trim();
            if (query.Length < 2)
            {
                return Ok(new List<SearchContentViewModel>());
            }

            var actors = await _searchService.SearchActorAsync(query);

            var searchActorsViewModel = actors.Select(a => new SearchActorViewModel
            {
                Id = a.Id,
                Name = a.Name,
            }).ToList();

            return Ok(searchActorsViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewActor ([FromBody] NewActorViewModel newActor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newActorDTO = new NewActorDTO
            {
                Name = newActor.Name,
                BirthDate = newActor.BirthDate,
                Description = newActor.Description,
            };

            var result = await _actorService.AddActorAsync(newActorDTO);

            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok();
        }
    }
}
