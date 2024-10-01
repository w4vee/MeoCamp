using MeoCamp.Data.Models;
using MeoCamp.Data.Repositories.Interface;
using MeoCamp.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Data.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly MeoCampDBContext _context;

        public BlogRepository(MeoCampDBContext context)
        {
            _context = context;
        }
        public async Task<Blog> CreateBlog(Blog blog)
        {
            _context.Blog.Add(blog);
            await _context.SaveChangesAsync();
            return blog;
        }

        public async Task<bool> DeleteBlog(Blog blog)
        {
            _context.Blog.Remove(blog);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Blog>> GetAllBlogAsync()
        {
            return await _context.Set<Blog>().ToListAsync();
        }

        public async Task<Blog> GetBlogbyUserIdAsync(int userId)
        {
            var Blog = await _context.Blog.FirstOrDefaultAsync(x => x.CustomerId == userId);
            return Blog;
        }

        public async Task<Blog> UpdateBlog(Blog blog)
        {
            var tracker = _context.Attach(blog);
            tracker.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return blog;
        }
    }
}
