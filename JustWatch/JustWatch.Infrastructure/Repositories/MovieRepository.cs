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
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddNewComment(MovieComment movieComment)
        {
            _context.MovieComments.Add(movieComment);
            await _context.SaveChangesAsync();
        }

        public async Task AddNewMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovie(Movie movie)
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Movie>> GetAllMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie?> GetMovieById(int id)
        {
            return await _context.Movies
                .AsSplitQuery()
                .Include(m => m.MovieGenres)
                .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieActors)
                .ThenInclude(ma => ma.Actor)
                .Include(m => m.Comments)
                .ThenInclude(mc => mc.User)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Movie>> GetRandMoviesAsync(int count)
        {
            return await _context.Movies
                .OrderBy(m => Guid.NewGuid())
                .Take(count)
                .ToListAsync();
        }
    }
}
