using JustWatch.Application.DTO;
using JustWatch.Application.Interfaces;
using JustWatch.Domain.Commom;
using JustWatch.Domain.Interfaces;
using JustWatch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Application.Services
{
    public class TVShowService : ITVShowService
    {
        private readonly ITVShowRepository _repository;
        public TVShowService(ITVShowRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> AddNewCommentAsync(CommentDTO commentDTO)
        {
            var comment = new TVShowComment
            {
                TVShowId = commentDTO.ContentId,
                Text = commentDTO.Text,
                CreatedAt = commentDTO.CreatedAt,
                UserId = commentDTO.UserId,
            };

            await _repository.AddNewComment(comment);

            return Result.Success();
        }

        public async Task<Result<int>> AddNewTvShowAsyc(TVShowDetailsDTO tvShowDetailsDTO)
        {
            var newTvShow = new TVShow
            {
                Title = tvShowDetailsDTO.Title,
                Director = tvShowDetailsDTO.Director,
                Years = tvShowDetailsDTO.Years,
                Rating = tvShowDetailsDTO.Rating,
                Duration = tvShowDetailsDTO.Duration,
                Seasons = tvShowDetailsDTO.Seasons,
                Description = tvShowDetailsDTO.Description,
                PosterUrl = tvShowDetailsDTO.PosterUrl,
                Wallpaper = tvShowDetailsDTO.Wallpaper,
                TVShowActors = tvShowDetailsDTO.Actors.Select(a => new TVShowActor
                {
                    ActorId = a.Id,
                    Role = a.Role,
                    CastOrder = a.CastOrder,
                }).ToList(),
                TVShowGenres = tvShowDetailsDTO.Genres.Select(g => new TVShowGenre { GenreId = g.Id, }).ToList()
            };

            await _repository.AddNewTvShow(newTvShow);
            return Result<int>.Success(newTvShow.Id);
        }

        public async Task<Result> DeleteTvShowAsync(int id)
        {
            var tvShowToBeDeleted = await _repository.GetTvShowById(id);
            if (tvShowToBeDeleted == null)
            {
                return Result.Error("TVShow not found.");
            }
            await _repository.DeleteTvShow(tvShowToBeDeleted);
            return Result.Success();
        }

        public async Task<Result> EditTvShowActorsAsync(int id, List<ActorInMovieDTO> actorInMovieDTOs)
        {
            var showToBeEdited = await _repository.GetTvShowById(id);

            if(showToBeEdited == null)
            {
                return Result.Error("TVShow not found");
            }

            showToBeEdited.TVShowActors.Clear();

            foreach(var actor in actorInMovieDTOs)
            {
                showToBeEdited.TVShowActors.Add(new TVShowActor
                {
                    ActorId = actor.Id,
                    Role = actor.Role,
                    CastOrder = actor.CastOrder,
                });
            }

            await _repository.SaveAsync();
            return Result.Success();
        }

        public async Task<Result> EditTvShowAsync(TVShowDetailsDTO tvShowDetailsDTO)
        {
            var showToBeEdited = await _repository.GetTvShowById(tvShowDetailsDTO.Id);

            if (showToBeEdited == null)
            {
                return Result.Error("TVShow not found.");
            }

            showToBeEdited.TVShowGenres.Clear();

            showToBeEdited.Title = tvShowDetailsDTO.Title;
            showToBeEdited.Director = tvShowDetailsDTO.Director;
            showToBeEdited.Years = tvShowDetailsDTO.Years;  
            showToBeEdited.Rating = tvShowDetailsDTO.Rating;
            showToBeEdited.Duration = tvShowDetailsDTO.Duration;
            showToBeEdited.Seasons = tvShowDetailsDTO.Seasons;
            showToBeEdited.Description = tvShowDetailsDTO.Description;

            foreach(var genre in tvShowDetailsDTO.Genres)
            {
                showToBeEdited.TVShowGenres.Add(new TVShowGenre { GenreId = genre.Id, });
            }

            await _repository.SaveAsync();
            return Result.Success();
        }

        public async Task<List<TVShowDTO>> GetAllTvShowsAsync()
        {
            var tvshows = await _repository.GetAllTvShows();

            return tvshows.Select(t => new TVShowDTO
            {
                Id = t.Id,
                Title = t.Title,
                Seasons = t.Seasons,
                PosterUrl = t.PosterUrl,
            }).ToList();
        }

        public async Task<List<TVShowDTO>> GetTopShowsAsync(int count)
        {
            var tvshows = await _repository.GetRandShowsAsync(count);

            return tvshows.Select(x => new TVShowDTO
            {
                Id = x.Id,
                Title = x.Title,
                Seasons = x.Seasons,
                PosterUrl = x.PosterUrl,
            }).ToList();
        }

        public async Task<TVShowDetailsDTO> GetTvShowByIdAsync(int id)
        {
            var tvshow = await _repository.GetTvShowById(id);

            var tvshowDetailsDTO = new TVShowDetailsDTO
            {
                Id = tvshow.Id,
                Title = tvshow.Title,
                Seasons = tvshow.Seasons,
                Rating = tvshow.Rating,
                Years = tvshow.Years,
                Description = tvshow.Description,
                PosterUrl = tvshow.PosterUrl,
                Wallpaper = tvshow.Wallpaper,
                Director = tvshow.Director,
                Duration = tvshow.Duration,
                Actors = tvshow.TVShowActors.Select(tva => new ActorInMovieDTO
                {
                    Id = tva.ActorId,
                    Name = tva.Actor.Name,
                    Role = tva.Role,
                    CastOrder = tva.CastOrder,
                }).ToList(),
                Genres = tvshow.TVShowGenres.Select(tvg => new GenreDTO
                {
                    Id = tvg.GenreId,
                    Name = tvg.Genre.Name,
                }).ToList(),
                Comments = tvshow.Comments.Select(c => new CommentDTO
                {
                    ID = c.Id,
                    Text = c.Text,
                    CreatedAt = c.CreatedAt,
                    UserId = c.UserId,
                    UserName = $"{c.User.FirstName} {c.User.LastName}",
                    UserAvatar = c.User.UserAvatar,
                }).ToList()
            };

            return tvshowDetailsDTO;
        }
    }
}
