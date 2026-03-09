using JustWatch.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JustWatch.Web.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var result = await _commentService.DeleteCommentAsync(id);

            if (!result.IsSuccess)
            {
                return NotFound();
            }

            return RedirectToAction("Details", "Movies", new { id = result.Data });
        }
    }
}
