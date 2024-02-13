using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SocialY.Data;
using SocialY.Data.Repository;
using SocialY.Models;
using SocialY.Models.Dto;

namespace SocialY.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IWebHostEnvironment _webHost;

        public PostsController(IPostRepository postRepository, IWebHostEnvironment webHost)
        {
            _postRepository = postRepository;
            _webHost = webHost;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var postList = _postRepository.GetAllPostsAsync();
            return View(await postList);
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postRepository.GetPostByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostDto postDto, IFormFile file)
        {
            if (ModelState.IsValid && file != null)
            {
                string filePath = @"images\post\";
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string postPath = Path.Combine(_webHost.WebRootPath, filePath);

                using (FileStream stream = new FileStream(Path.Combine(postPath, fileName), FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                Post post = new Post()
                {
                    Title = postDto.Title,
                    Description = postDto.Description,
                    Author = User.Identity.Name,
                    ImageUrl = filePath + fileName
                };

                await _postRepository.AddPostAsync(post);

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Posts/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postRepository.GetPostByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Author,ImageUrl")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _postRepository.UpdatePostAsync(post);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postRepository.GetPostByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post != null)
            {
                await _postRepository.DeletePostAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _postRepository.PostExists(id);
        }
    }
}
