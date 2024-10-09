using MeoCamp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeoCamp.Data.Repositories.Interface
{
    public interface IBlogRepository
    {
        public Task<List<Blog>> GetAllBlogAsync();
        public Task<List<Blog>> GetBlogbyUserIdAsync(int userId);
        public Task<Blog> GetBlogbyIdAsync(int Id);
        public Task<Blog> CreateBlog(Blog blog);
        public Task<Blog> UpdateBlog(Blog blog);
        public Task<bool> DeleteBlog(Blog blog); 
    }
}
