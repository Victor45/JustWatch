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
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;
        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task DeleteMovieComment(MovieComment movieComment)
        {
            _context.MovieComments.Remove(movieComment);
            await _context.SaveChangesAsync();
        }

        public async Task<MovieComment?> GetMovieCommentById(int id)
        {
            return await _context.MovieComments.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
