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
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;
        public SearchService(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        public async Task<List<ActorSearchDTO>> SearchActorAsync(string q)
        {
            var actors = await _searchRepository.SearchActor(q);

            return actors.Select(actor => new ActorSearchDTO
            {
                Id = actor.Id,
                Name = actor.Name,
            }).ToList();
        }

        public async Task<List<SearchContentDTO>> SearchContentAsync(string q)
        {
            var items = await _searchRepository.SearchContent(q);

            return items.Select(item => new SearchContentDTO
            {
                Id = item.Id,
                Title = item.Title,
                Info = item.Info,
                PosterURL = item.PosterURL,
                Type = item.Type,
            }).ToList();
        }
    }
}
