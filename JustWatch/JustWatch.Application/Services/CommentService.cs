using JustWatch.Application.Interfaces;
using JustWatch.Domain.Commom;
using JustWatch.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<Result<int>> DeleteCommentAsync(int id)
        {
            var comment = await _commentRepository.GetMovieCommentById(id);

            if (comment == null)
            {
                return Result<int>.Error("Comment not found");
            }

            int movieId = comment.MovieId;

            await _commentRepository.DeleteMovieComment(comment);
            return Result<int>.Success(movieId);
        }
    }
}
