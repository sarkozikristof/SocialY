using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialY.Data.Repository.IRepositry;
using SocialY.Models;

namespace SocialY.Controllers
{
    public class RatingController : Controller
    {
        private readonly IPostRepository _postRepository;


        public RatingController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        //GET 
        [Authorize]
        public IActionResult RatePost(int postId)
        {
            var post = _postRepository.GetPostByIdAsync(postId).Result;
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        //POST
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RatePost(int postId, int creativityRating, int uniquenessRating)
        {
            if (ModelState.IsValid)
            {
                var post = _postRepository.GetPostByIdAsync(postId).Result;
                if (post == null)
                {
                    return NotFound();
                }

                var existingRating = post.Ratings.FirstOrDefault(r => r.UserId == User.Identity.Name);
                if (existingRating != null)
                {
                    existingRating.CreativityRating = creativityRating;
                    existingRating.UniqunessRating = uniquenessRating;
                }
                else
                {
                    var newRating = new UserRating
                    {
                        UserId = User.Identity.Name,
                        PostId = post.Id,
                        CreativityRating = creativityRating,
                        UniqunessRating = uniquenessRating
                    };

                    post.Ratings.Add(newRating);
                }

                await _postRepository.UpdatePostAsync(post);

                return RedirectToAction("Index", "Posts");
            }

            return View();
        }
    }
}
