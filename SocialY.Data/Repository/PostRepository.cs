using Microsoft.EntityFrameworkCore;
using SocialY.Data.Repository.IRepositry;
using SocialY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialY.Data.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _context.Post.Include(p => p.Ratings).ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(int? id)
        {
            return await _context.Post.Include(p => p.Ratings).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddPostAsync(Post post)
        {
            _context.Post.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePostAsync(Post post)
        {
            _context.Entry(post).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int? id)
        {
            var post = await _context.Post.FindAsync(id);
            if (post != null)
            {
                _context.Post.Remove(post);
                await _context.SaveChangesAsync();
            }
        }

        public bool PostExists(int id)
        {
            return _context.Post.Any(m => m.Id == id);
        }
    }
}
