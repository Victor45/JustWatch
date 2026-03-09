using JustWatch.Application.DTO;
using JustWatch.Application.DTO.Movies;
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
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repository;
        public MovieService(IMovieRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result> AddNewCommentAsync(CommentDTO commentDTO)
        {
            var comment = new MovieComment
            {
                MovieId = commentDTO.ContentId,
                UserId = commentDTO.UserId,
                Text = commentDTO.Text,
                CreatedAt = commentDTO.CreatedAt,
            };

            await _repository.AddNewComment(comment);

            return Result.Success();
        }

        public async Task<Result<int>> AddNewMovieAsync(MovieDetailsDTO movieDetailsDTO)
        {

            var newMovie = new Movie
            {
                Title = movieDetailsDTO.Title,
                Description = movieDetailsDTO.Description,
                Director = movieDetailsDTO.Director,
                Year = movieDetailsDTO.Year,
                Rating = movieDetailsDTO.Rating,
                Duration = movieDetailsDTO.Duration,
                PosterUrl = movieDetailsDTO.PosterUrl,
                Wallpaper = movieDetailsDTO.Wallpaper,
                MovieActors = movieDetailsDTO.Actors.Select(a => new MovieActor { 
                    ActorId = a.Id,
                    Role = a.Role,
                    CastOrder = a.CastOrder,
                }).ToList(),
                MovieGenres = movieDetailsDTO.Genres.Select(g => new MovieGenre { GenreId = g.Id }).ToList(),
            };

            await _repository.AddNewMovie(newMovie);
            return Result<int>.Success(newMovie.Id);
        }

        public async Task<Result> DeleteMovieAsync(int id)
        {
            var movieToDelete = await _repository.GetMovieById(id);

            if (movieToDelete == null)
            {
                return Result.Error("Movie not found");
            }

            await _repository.DeleteMovie(movieToDelete);
            return Result.Success();
        }

        public async Task<Result> EditMovieActorsAsync(int id, List<ActorInMovieDTO> actors)
        {
            var movieToBeEdited = await _repository.GetMovieById(id);

            if (movieToBeEdited == null)
            {
                return Result.Error("Movie not found");
            }

            movieToBeEdited.MovieActors.Clear();

            foreach (var actor in actors)
            {
                movieToBeEdited.MovieActors.Add(new MovieActor
                {
                    ActorId = actor.Id,
                    Role = actor.Role,
                    CastOrder = actor.CastOrder,
                });
            }

            await _repository.SaveAsync();
            return Result.Success();
        }

        public async Task<Result> EditMovieAsync(EditMovieDTO editMovieDTO)
        {
            var movieToBeEdited = await _repository.GetMovieById(editMovieDTO.Id);

            if (movieToBeEdited == null)
            {
                return Result.Error("Movie not found");
            }

            movieToBeEdited.Title = editMovieDTO.Title;
            movieToBeEdited.Year = editMovieDTO.Year;
            movieToBeEdited.Rating = editMovieDTO.Rating;
            movieToBeEdited.Description = editMovieDTO.Description;
            movieToBeEdited.Director = editMovieDTO.Director;
            movieToBeEdited.Duration = editMovieDTO.Duration;

            movieToBeEdited.MovieGenres.Clear();

            foreach (var genreId in editMovieDTO.Genres.Select(g => g.Id).Distinct())
            {
                movieToBeEdited.MovieGenres.Add(new MovieGenre { GenreId = genreId });
            } 

            await _repository.SaveAsync();
            return Result.Success();
        }

        public async Task<List<MovieDTO>> GetAlMoviesAsync()
        {
            var movies = await _repository.GetAllMovies();

            return movies.Select(x => new MovieDTO
            {
                Id = x.Id,
                Title = x.Title,
                Year = x.Year,
                PosterUrl = x.PosterUrl,
            }).ToList();
        }

        public async Task<MovieDetailsDTO> GetMovieByIdAsync(int id)
        {
            var movie = await _repository.GetMovieById(id);

            var movieDetails = new MovieDetailsDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Rating = movie.Rating,
                Year = movie.Year,
                Description = movie.Description,
                PosterUrl = movie.PosterUrl,
                Wallpaper = movie.Wallpaper,
                Duration = movie.Duration,
                Director = movie.Director,
                Actors = movie.MovieActors
                     .Select(ma => new ActorInMovieDTO
                     {
                         Id = ma.ActorId,
                         Name = ma.Actor.Name,
                         Role = ma.Role,
                         CastOrder = ma.CastOrder,
                     }).ToList(),
                Genres = movie.MovieGenres
                     .Select(mg => new GenreDTO
                     {
                         Id = mg.GenreId,
                         Name = mg.Genre.Name,
                     }).ToList(),
                Comments = movie.Comments
                     .Select(c => new CommentDTO
                     {
                         ID = c.Id,
                         Text = c.Text,
                         CreatedAt = c.CreatedAt,
                         UserId = c.UserId,
                         UserAvatar = c.User.UserAvatar,
                         UserName = $"{c.User.FirstName} {c.User.LastName}",
                     }).ToList()
            };

            return movieDetails;
        }

        public async Task<List<MovieDTO>> GetTopMoviesAsync(int count)
        {
            var movies = await _repository.GetRandMoviesAsync(count);

            return movies.Select(x => new MovieDTO
            {
                Id = x.Id,
                Title = x.Title,
                Year = x.Year,
                PosterUrl = x.PosterUrl,
            }).ToList();
        }
    }
}
