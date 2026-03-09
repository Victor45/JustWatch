using JustWatch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Domain.Interfaces
{
    public interface ITVShowRepository
    {
        Task<List<TVShow>> GetRandShowsAsync(int count);
        Task<List<TVShow>> GetAllTvShows();
        Task<TVShow?> GetTvShowById(int id);
        Task AddNewComment(TVShowComment comment);
        Task AddNewTvShow(TVShow tvShow);
        Task DeleteTvShow(TVShow tvShow);
        Task SaveAsync();
    }
}
