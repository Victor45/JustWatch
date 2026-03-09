using JustWatch.Domain.Enums;
using JustWatch.Domain.Interfaces;
using JustWatch.Domain.Models;
using JustWatch.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Infrastructure.Repositories
{
    public class SearchRepository : ISearchRepository
    {
        private readonly AppDbContext _context;
        public SearchRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<List<Actor>> SearchActor(string q)
        {
            var actors = _context.Actors
                .Where(a => EF.Functions.Like(a.Name, $"%{q}%"))
                .ToListAsync();

            return actors;
        }

        public async Task<List<SearchContent>> SearchContent(string q)
        {
            var movies = _context.Movies
                .Where(m => EF.Functions.Like(m.Title, $"%{q}%"))
                .Select(m => new SearchContent
                {
                    Id = m.Id,
                    Title = m.Title,
                    PosterURL = m.PosterUrl,
                    Info = m.Year,
                    Type = "Movie"
                });

            var tvshows = _context.TVShows
                .Where(tv => EF.Functions.Like(tv.Title, $"%{q}%"))
                .Select(tv => new SearchContent
                {
                    Id = tv.Id,
                    Title = tv.Title,
                    PosterURL = tv.PosterUrl,
                    Info = tv.Seasons,
                    Type = "TVShow"
                });

            var items = await movies.Concat(tvshows)
                .ToListAsync();

            return items;
        }
    }
}
