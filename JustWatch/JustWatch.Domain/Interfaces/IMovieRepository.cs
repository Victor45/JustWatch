using JustWatch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Domain.Interfaces
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetRandMoviesAsync(int count);
        Task<Movie?> GetMovieById(int id);
        Task<List<Movie>> GetAllMovies();
        Task AddNewComment(MovieComment movieComment);
        Task AddNewMovie(Movie movie); 
        Task DeleteMovie(Movie movie);
        Task SaveAsync();
    }
}
