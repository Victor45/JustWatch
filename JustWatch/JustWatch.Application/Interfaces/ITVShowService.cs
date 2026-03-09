using JustWatch.Application.DTO;
using JustWatch.Domain.Commom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Application.Interfaces
{
    public interface ITVShowService
    {
        Task<List<TVShowDTO>> GetTopShowsAsync(int count);
        Task<List<TVShowDTO>> GetAllTvShowsAsync();
        Task<TVShowDetailsDTO> GetTvShowByIdAsync(int id);
        Task<Result> AddNewCommentAsync(CommentDTO commentDTO);
        Task<Result<int>> AddNewTvShowAsyc(TVShowDetailsDTO tvShowDetailsDTO);
        Task<Result> DeleteTvShowAsync(int id);
        Task<Result> EditTvShowAsync(TVShowDetailsDTO tvShowDetailsDTO);
        Task<Result> EditTvShowActorsAsync(int id, List<ActorInMovieDTO> actorInMovieDTOs);
    }
}
