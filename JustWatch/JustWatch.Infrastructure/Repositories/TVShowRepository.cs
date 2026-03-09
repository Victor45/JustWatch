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
    public class TVShowRepository : ITVShowRepository
    {
        private readonly AppDbContext _context;

        public TVShowRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddNewComment(TVShowComment comment)
        {
            _context.TVShowComments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task AddNewTvShow(TVShow tvShow)
        {
            _context.TVShows.Add(tvShow);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTvShow(TVShow tvShow)
        {
            _context.TVShows.Remove(tvShow);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TVShow>> GetAllTvShows()
        {
            return await _context.TVShows.ToListAsync();
        }

        public async Task<List<TVShow>> GetRandShowsAsync(int count)
        {
            return await _context.TVShows
                .OrderBy(x => Guid.NewGuid())
                .Take(count)
                .ToListAsync();
        }

        public async Task<TVShow?> GetTvShowById(int id)
        {
            return await _context.TVShows
                .AsSplitQuery()
                .Include(tv => tv.TVShowGenres)
                .ThenInclude(tvg => tvg.Genre)
                .Include(tv => tv.TVShowActors)
                .ThenInclude(tva => tva.Actor)
                .Include(tv => tv.Comments)
                .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(tv => tv.Id == id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
