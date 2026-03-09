using JustWatch.Application.DTO;
using JustWatch.Application.DTO.Movies;
using JustWatch.Domain.Commom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Application.Interfaces
{
    public interface IMovieService
    {
        Task<List<MovieDTO>> GetTopMoviesAsync(int count);
        Task<MovieDetailsDTO> GetMovieByIdAsync(int id);
        Task<List<MovieDTO>> GetAlMoviesAsync();
        Task<Result> AddNewCommentAsync(CommentDTO commentDTO);
        Task<Result<int>> AddNewMovieAsync(MovieDetailsDTO movieDetailsDTO);
        Task<Result> DeleteMovieAsync(int id);
        Task<Result> EditMovieAsync(EditMovieDTO editMovieDTO);
        Task<Result> EditMovieActorsAsync(int id, List<ActorInMovieDTO> actors);
    }
}
